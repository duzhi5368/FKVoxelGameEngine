/* 
 * WRANING: These codes below is far away from bugs with the god and his animal protecting
 *                  _oo0oo_                   ┏┓　　　┏┓
 *                 o8888888o                ┏┛┻━━━┛┻┓
 *                 88" . "88                ┃　　　　　　　┃ 　
 *                 (| -_- |)                ┃　　　━　　　┃
 *                 0\  =  /0                ┃　┳┛　┗┳　┃
 *               ___/`---'\___              ┃　　　　　　　┃
 *             .' \\|     |# '.             ┃　　　┻　　　┃
 *            / \\|||  :  |||# \            ┃　　　　　　　┃
 *           / _||||| -:- |||||- \          ┗━┓　　　┏━┛
 *          |   | \\\  -  #/ |   |          　　┃　　　┃神兽保佑
 *          | \_|  ''\---/''  |_/ |         　　┃　　　┃永无BUG
 *          \  .-\__  '-'  ___/-. /         　　┃　　　┗━━━┓
 *        ___'. .'  /--.--\  `. .'___       　　┃　　　　　　　┣┓
 *     ."" '<  `.___\_<|>_/___.' >' "".     　　┃　　　　　　　┏┛
 *    | | :  `- \`.;`\ _ /`;.`/ - ` : | |   　　┗┓┓┏━┳┓┏┛
 *    \  \ `_.   \_ __\ /__ _/   .-` /  /   　　　┃┫┫　┃┫┫
 *=====`-.____`.___ \_____/___.-`___.-'=====　　　┗┻┛　┗┻┛ 
 *                  `=---='　　　
 *          佛祖保佑       永无BUG
 */
// =============================================================================== 
// Author              :    Frankie.W
// Create Time         :    2017/7/31 19:34:07
// Update Time         :    2017/7/31 19:34:07
// Class Version       :    v1.0.0.0
// Class Description   :    
// ===============================================================================
using FKVoxelEngine.Base;
using FKVoxelEngine.Framework;
using FKVoxelEngine.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
// ===============================================================================
namespace FKVoxelEngine.RenderObj
{
    public class FKBaseChunkCache : DrawableGameComponent, IFKBaseChunkCacheService
    {
        #region ==== 成员变量 ====

        public static byte CacheRange = FKEngine.GetInstance.Config.Cache.CacheRange;
        public static byte ViewRange = FKEngine.GetInstance.Config.Cache.ViewRange;

        public static BoundingBox BoundingBox { get; set; }
        public BoundingBox ViewRangeBoundingBox { get; set; }
        public BoundingBox CacheRangeBoundingBox { get; set; }
        public FKTerrainGenerator Generator { get; set; }
        public bool CacheThreadStarted { get; set; }
        public int ChunksDrawn { get; set; }
        public Dictionary<ENUM_FKBaseChunkState, int> StateStatistics { get; set; }

        private Effect _BlockEffect;
        private Texture2D _BlockTextureAtlas;
        private Texture2D _CrackTextureAtlas;
        private IFKVertexBuilderService _VertexBuilder;
        private IFKBaseChunkStorageService _ChunkStorage;
        private IFKCameraService _Camera;
        private IFKFoggerService _Fogger;
        private IFKAssetManagerService _AssetManager;
        private IFKTimeRulerService _TimeRuler;

        private static readonly FKLogger Logger = FKLogManager.CreateLogger("FKBaseChunkCache");

        #endregion ==== 成员变量 ====

        #region ==== 成员变量 ====

        public FKBaseChunkCache(Game game)
            : base(game)
        {
            Game.Services.AddService(typeof(IFKBaseChunkCacheService), this);

            if (ViewRange > CacheRange)
                throw new FKChunkCacheException();

            CacheThreadStarted = false;
            StateStatistics = new Dictionary<ENUM_FKBaseChunkState, int>
            {
                {ENUM_FKBaseChunkState.eAwaitingGenerate, 0},
                {ENUM_FKBaseChunkState.eGenerating, 0},
                {ENUM_FKBaseChunkState.eAwaitingLighting, 0},
                {ENUM_FKBaseChunkState.eLighting, 0},
                {ENUM_FKBaseChunkState.eAwaitingBuild, 0},
                {ENUM_FKBaseChunkState.eBuilding, 0},
                {ENUM_FKBaseChunkState.eReady, 0},
                {ENUM_FKBaseChunkState.eAwaitingRelighting, 0},
                {ENUM_FKBaseChunkState.eAwaitingRebuilding, 0},
                {ENUM_FKBaseChunkState.eAwaitingRemoval, 0},
            };
        }

        public override void Initialize()
        {
            Logger.Trace("Init()");

            _VertexBuilder = (IFKVertexBuilderService)Game.Services.GetService(typeof(IFKVertexBuilderService));
            _ChunkStorage = (IFKBaseChunkStorageService)Game.Services.GetService(typeof(IFKBaseChunkStorageService));
            _Camera = (IFKCameraService)Game.Services.GetService(typeof(IFKCameraService));
            _Fogger = (IFKFoggerService)Game.Services.GetService(typeof(IFKFoggerService));
            _AssetManager = (IFKAssetManagerService)Game.Services.GetService(typeof(IFKAssetManagerService));
            _TimeRuler = (IFKTimeRulerService)Game.Services.GetService(typeof(IFKTimeRulerService));

            Generator = new FKTerrainGenerator_Flat(new FKTerrainBiomeGenerator_RainForest());

            base.Initialize();
        }

        protected override void LoadContent()
        {
            _BlockEffect = _AssetManager.BlockEffect;
            _BlockTextureAtlas = _AssetManager.BlockTextureAtlas;
            _CrackTextureAtlas = _AssetManager.CrackTextureAtlas;
        }

        public override void Update(GameTime gametime)
        {
            UpdateBoundingBoxes();

            if (CacheThreadStarted)
                return;

            var cacheThread = new Thread(CacheThreadMain) { IsBackground = true };
            cacheThread.Start();

            CacheThreadStarted = true;
        }

        public override void Draw(GameTime gameTime)
        {
            var viewFrustrum = new BoundingFrustum(_Camera.View * _Camera.Projection);

            Game.GraphicsDevice.DepthStencilState = DepthStencilState.Default;
            Game.GraphicsDevice.BlendState = BlendState.Opaque;

            _BlockEffect.Parameters["World"].SetValue(Matrix.Identity);
            _BlockEffect.Parameters["View"].SetValue(_Camera.View);
            _BlockEffect.Parameters["Projection"].SetValue(_Camera.Projection);
            _BlockEffect.Parameters["CameraPosition"].SetValue(_Camera.Position);
            _BlockEffect.Parameters["BlockTextureAtlas"].SetValue(_BlockTextureAtlas);
            _BlockEffect.Parameters["SunColor"].SetValue(FKWorld.SunColor);
            _BlockEffect.Parameters["NightColor"].SetValue(FKWorld.NightColor);
            _BlockEffect.Parameters["HorizonColor"].SetValue(FKWorld.HorizonColor);
            _BlockEffect.Parameters["MorningTint"].SetValue(FKWorld.MorningTint);
            _BlockEffect.Parameters["EveningTint"].SetValue(FKWorld.EveningTint);
            _BlockEffect.Parameters["TimeOfDay"].SetValue(FKGameTime.GetGameTimeOfDay());
            _BlockEffect.Parameters["FogNear"].SetValue(_Fogger.FogVector.X);
            _BlockEffect.Parameters["FogFar"].SetValue(_Fogger.FogVector.Y);

            ChunksDrawn = 0;
            foreach(EffectPass pass in _BlockEffect.CurrentTechnique.Passes)
            {
                pass.Apply();
                foreach(FKBaseChunk chunk in _ChunkStorage.Values)
                {
                    if (chunk.IndexBuffer == null || chunk.VertexBuffer == null)
                        continue;
                    if (chunk.VertexBuffer.VertexCount == 0)
                        continue;
                    if (chunk.IndexBuffer.IndexCount == 0)
                        continue;
                    if (!IsChunkInViewRange(chunk))
                        continue;
                    if (!chunk.BoundingBox.Intersects(viewFrustrum))
                        continue;


                    Game.GraphicsDevice.SetVertexBuffer(chunk.VertexBuffer);
                    Game.GraphicsDevice.Indices = chunk.IndexBuffer;
                    Game.GraphicsDevice.DrawIndexedPrimitives(PrimitiveType.TriangleList, 0, /*0, chunk.VertexBuffer.VertexCount,*/ 0, chunk.IndexBuffer.IndexCount / 3);

                    ChunksDrawn++;
                }
            }

            StateStatistics[ENUM_FKBaseChunkState.eAwaitingGenerate]    = _ChunkStorage.Values.Count(chunk => chunk.ChunkState == ENUM_FKBaseChunkState.eAwaitingGenerate);
            StateStatistics[ENUM_FKBaseChunkState.eGenerating]          = _ChunkStorage.Values.Count(chunk => chunk.ChunkState == ENUM_FKBaseChunkState.eGenerating);
            StateStatistics[ENUM_FKBaseChunkState.eAwaitingLighting]    = _ChunkStorage.Values.Count(chunk => chunk.ChunkState == ENUM_FKBaseChunkState.eAwaitingLighting);
            StateStatistics[ENUM_FKBaseChunkState.eLighting]            = _ChunkStorage.Values.Count(chunk => chunk.ChunkState == ENUM_FKBaseChunkState.eLighting);
            StateStatistics[ENUM_FKBaseChunkState.eAwaitingRelighting]  = _ChunkStorage.Values.Count(chunk => chunk.ChunkState == ENUM_FKBaseChunkState.eAwaitingRelighting);
            StateStatistics[ENUM_FKBaseChunkState.eAwaitingBuild]       = _ChunkStorage.Values.Count(chunk => chunk.ChunkState == ENUM_FKBaseChunkState.eAwaitingBuild);
            StateStatistics[ENUM_FKBaseChunkState.eBuilding]            = _ChunkStorage.Values.Count(chunk => chunk.ChunkState == ENUM_FKBaseChunkState.eBuilding);
            StateStatistics[ENUM_FKBaseChunkState.eAwaitingRebuilding]     = _ChunkStorage.Values.Count(chunk => chunk.ChunkState == ENUM_FKBaseChunkState.eAwaitingRebuilding);
            StateStatistics[ENUM_FKBaseChunkState.eReady]               = _ChunkStorage.Values.Count(chunk => chunk.ChunkState == ENUM_FKBaseChunkState.eReady);
            StateStatistics[ENUM_FKBaseChunkState.eAwaitingRemoval]     = _ChunkStorage.Values.Count(chunk => chunk.ChunkState == ENUM_FKBaseChunkState.eAwaitingRemoval);

            base.Draw(gameTime);
        }

        #endregion ==== 继承实现 ====

        #region ==== 对外接口 ====
        public FKBaseChunk GetChunkByRelativePosition(int x, int z)
        {
            return _ChunkStorage.ContainsKey(x, z) ? null : this._ChunkStorage[x, z];
        }

        public FKBaseChunk GetChunkByWorldPosition(int x, int z)
        {
            if (x < 0)
                x -= FKBaseChunk.WidthInBlocks;

            if (z < 0)
                z -= FKBaseChunk.LengthInBlocks;

            return (!(_ChunkStorage.ContainsKey(x / FKBaseChunk.WidthInBlocks, z / FKBaseChunk.LengthInBlocks))) ?
               null : _ChunkStorage[x / FKBaseChunk.WidthInBlocks, z / FKBaseChunk.LengthInBlocks];
        }

        public FKBaseChunk GetNeighborChunk(FKBaseChunk origin, FKBaseChunk.ENUM_Edges edge)
        {
            switch (edge)
            {
                case FKBaseChunk.ENUM_Edges.XDecreasing:
                    return this.GetChunkByRelativePosition(origin.RelativePosition.X - 1, origin.RelativePosition.Z);
                case FKBaseChunk.ENUM_Edges.XIncreasing:
                    return this.GetChunkByRelativePosition(origin.RelativePosition.X + 1, origin.RelativePosition.Z);
                case FKBaseChunk.ENUM_Edges.ZDecreasing:
                    return this.GetChunkByRelativePosition(origin.RelativePosition.X, origin.RelativePosition.Z - 1);
                case FKBaseChunk.ENUM_Edges.ZIncreasing:
                    return this.GetChunkByRelativePosition(origin.RelativePosition.X, origin.RelativePosition.Z + 1);
            }
            return null;
        }

        public bool IsChunkInCacheRange(FKBaseChunk chunk)
        {
            return CacheRangeBoundingBox.Contains(chunk.BoundingBox) == ContainmentType.Contains;
        }   

        public bool IsChunkInViewRange(FKBaseChunk chunk)
        {
            return ViewRangeBoundingBox.Contains(chunk.BoundingBox) == ContainmentType.Contains;
        }

        public static bool IsInBounds(int x, int y, int z)
        {
            if (x < BoundingBox.Min.X || x >= BoundingBox.Max.X
                || y < BoundingBox.Min.Y || y >= BoundingBox.Max.Y
                || z < BoundingBox.Min.Z || z >= BoundingBox.Max.Z)
                return false;

            return true;
        }

        #endregion ==== 对外接口 ====

        #region ==== 核心函数 ====

        private void UpdateBoundingBoxes()
        {
            this.ViewRangeBoundingBox = new BoundingBox(
                        new Vector3(_Camera.CurrentChunk.WorldPosition.X - (ViewRange * FKBaseChunk.WidthInBlocks), 0,
                            _Camera.CurrentChunk.WorldPosition.Z - (ViewRange * FKBaseChunk.LengthInBlocks)),
                        new Vector3(_Camera.CurrentChunk.WorldPosition.X + ((ViewRange + 1) * FKBaseChunk.WidthInBlocks),
                            FKBaseChunk.HeightInBlocks, _Camera.CurrentChunk.WorldPosition.Z + ((ViewRange + 1) * FKBaseChunk.LengthInBlocks))
                );

            this.CacheRangeBoundingBox = new BoundingBox(
                        new Vector3(_Camera.CurrentChunk.WorldPosition.X - (CacheRange * FKBaseChunk.WidthInBlocks), 0,
                            _Camera.CurrentChunk.WorldPosition.Z - (CacheRange * FKBaseChunk.LengthInBlocks)),
                        new Vector3(_Camera.CurrentChunk.WorldPosition.X + ((CacheRange + 1) * FKBaseChunk.WidthInBlocks),
                            FKBaseChunk.HeightInBlocks,
                            _Camera.CurrentChunk.WorldPosition.Z + ((CacheRange + 1) * FKBaseChunk.LengthInBlocks))
                );
        }

        private void CacheThreadMain()
        {
            Logger.Info("Chunk processing thread running...");
            Thread.CurrentThread.Name = "FKChunkProcessingThread";
            while (true)
            {
                if (_Camera.CurrentChunk == null)
                    continue;

                Process();
            }
        }

        private void Process()
        {
            foreach(var chunk in _ChunkStorage.Values) {
                if (IsChunkInViewRange(chunk))
                {
                    ProcessChunkInViewRange(chunk);
                }
                else
                {
                    if (IsChunkInCacheRange(chunk))
                    {
                        ProcessChunkInCacheRange(chunk);
                    }
                    else
                    {
                        chunk.ChunkState = ENUM_FKBaseChunkState.eAwaitingRemoval;
                        _ChunkStorage.Remove(chunk.RelativePosition.X, chunk.RelativePosition.Z);
                        chunk.Dispose();
                    }
                }
            }

            if (FKEngine.GetInstance.Config.World.IsInfinitive)
                RecacheChunks();
        }

        private void RecacheChunks()
        {
            _Camera.CurrentChunk = GetChunkByWorldPosition((int)_Camera.Position.X, (int)_Camera.Position.Z);
            if (_Camera.CurrentChunk == null)
                return;

            for(int z = -CacheRange; z <= CacheRange; ++z)
            {
                for(int x = -CacheRange; x <= CacheRange; ++x)
                {
                    if (_ChunkStorage.ContainsKey(_Camera.CurrentChunk.RelativePosition.X + x, _Camera.CurrentChunk.RelativePosition.Z + z))
                        continue;

                    var chunk = new FKBaseChunk(new FKVector2Int(_Camera.CurrentChunk.RelativePosition.X + x, _Camera.CurrentChunk.RelativePosition.Z + z));
                    _ChunkStorage[chunk.RelativePosition.X, chunk.RelativePosition.Z] = chunk;
                }
            }

            var southWestEdge = new FKVector2Int(_Camera.CurrentChunk.RelativePosition.X - ViewRange, _Camera.CurrentChunk.RelativePosition.Z - ViewRange);
            var northEastEdge = new FKVector2Int(_Camera.CurrentChunk.RelativePosition.X + ViewRange, _Camera.CurrentChunk.RelativePosition.Z + ViewRange);
            BoundingBox = new BoundingBox(
                new Vector3(southWestEdge.X * FKBaseChunk.WidthInBlocks, 0, southWestEdge.Z * FKBaseChunk.LengthInBlocks),
                new Vector3((northEastEdge.X + 1) * FKBaseChunk.WidthInBlocks, FKBaseChunk.HeightInBlocks, (northEastEdge.Z + 1) * FKBaseChunk.LengthInBlocks)
                );
        }

        private void ProcessChunkInViewRange(FKBaseChunk chunk)
        {
            if (chunk.ChunkState == ENUM_FKBaseChunkState.eReady 
                || chunk.ChunkState == ENUM_FKBaseChunkState.eAwaitingRemoval)
                return;

            switch (chunk.ChunkState)
            {
                case ENUM_FKBaseChunkState.eAwaitingGenerate:
                    Generator.Generate(chunk);
                    break;
                case ENUM_FKBaseChunkState.eAwaitingLighting:
                case ENUM_FKBaseChunkState.eAwaitingRelighting:
                    FKLightning.Process(chunk);
                    break;
                case ENUM_FKBaseChunkState.eAwaitingBuild:
                case ENUM_FKBaseChunkState.eAwaitingRebuilding:
                    _VertexBuilder.Build(chunk);
                    break;
                default:
                    break;
            }
        }

        private void ProcessChunkInCacheRange(FKBaseChunk chunk)
        {
            if (chunk.ChunkState != ENUM_FKBaseChunkState.eAwaitingGenerate
                && chunk.ChunkState != ENUM_FKBaseChunkState.eAwaitingLighting)
                return;

            switch (chunk.ChunkState)
            {
                case ENUM_FKBaseChunkState.eAwaitingGenerate:
                    Generator.Generate(chunk);
                    break;
                case ENUM_FKBaseChunkState.eAwaitingLighting:
                    FKLightning.Process(chunk);
                    break;
                default:
                    break;
            }
        }

        #endregion ==== 核心函数 ====
    }

    public class FKChunkCacheException : Exception
    {
        public FKChunkCacheException() : base("View range can not be larger than cache range!")
        { }
    }
}