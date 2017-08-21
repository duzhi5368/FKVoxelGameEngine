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
// Create Time         :    2017/8/9 12:03:40
// Update Time         :    2017/8/9 12:03:40
// Class Version       :    v1.0.0.0
// Class Description   :    
// ===============================================================================
using FKVoxelEngine.Base;
using FKVoxelEngine.Framework;
using FKVoxelEngine.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Globalization;
using System.Text;
// ===============================================================================
namespace FKVoxelEngine.Profile
{
    public class FKStatistics : DrawableGameComponent, IFKStatisticsService
    {
        public int FPS { get; private set; }
        public long MemoryUsed { get { return GC.GetTotalMemory(false); } }
        public int GenerateQueue { get; private set; }
        public int LightenQueue { get; private set; }
        public int BuildQueue { get; private set; }
        public int ReadyQueue { get; private set; }
        public int RemoveQueue { get; private set; }


        private int _FrameCounter = 0;
        private TimeSpan _ElapsedTime = TimeSpan.Zero;
        private string _DrawnBlocks;
        private string _TotalBlocks;

        private FKPrimitiveBatch _PrimitiveBatch;
        private SpriteBatch _SpriteBatch;
        private SpriteFont _SpriteFont;
        private Matrix _LocalProjection;
        private Matrix _LocalView;
        private Rectangle _Bounds;

        private readonly Vector2[] _BackgroundPolygon = new Vector2[4];
        private readonly StringBuilder _StringBuilder = new StringBuilder(512, 512);

        private IFKWorldService _World;
        private IFKFoggerService _Fogger;
        private IFKBaseChunkCacheService _ChunkCache;
        private IFKBaseChunkStorageService _ChunkStorage;
        private IFKAssetManagerService _AssetManager;
        private IFKCameraService _Camera;

        private static readonly FKLogger Logger = FKLogManager.CreateLogger("FKStatistics");

        public FKStatistics(Game game)
            : base(game)
        {
            Game.Services.AddService(typeof(IFKStatisticsService), this);
        }

        public override void Initialize()
        {
            Logger.Trace("Init()");

            _Fogger = (IFKFoggerService)Game.Services.GetService(typeof(IFKFoggerService));
            _World = (IFKWorldService)Game.Services.GetService(typeof(IFKWorldService));
            _Camera = (IFKCameraService)Game.Services.GetService(typeof(IFKCameraService));
            _ChunkCache = (IFKBaseChunkCacheService)Game.Services.GetService(typeof(IFKBaseChunkCacheService));
            _ChunkStorage = (IFKBaseChunkStorageService)Game.Services.GetService(typeof(IFKBaseChunkStorageService));
            _AssetManager = (IFKAssetManagerService)Game.Services.GetService(typeof(IFKAssetManagerService));

            if (_AssetManager == null)
                throw new NullReferenceException("Can not find asset manager comonpent.");

            base.Initialize();
        }

        protected override void LoadContent()
        {
            _PrimitiveBatch = new FKPrimitiveBatch(GraphicsDevice, 1000);
            _LocalProjection = Matrix.CreateOrthographicOffCenter(0.0f, GraphicsDevice.Viewport.Width, GraphicsDevice.Viewport.Height, 0.0f, 0.0f, 1.0f);
            _LocalView = Matrix.Identity;
            _SpriteBatch = new SpriteBatch(GraphicsDevice);
            _SpriteFont = _AssetManager.DefaultFont;

            _Bounds = new Rectangle(10, 10, Game.GraphicsDevice.Viewport.Bounds.Width - 20, 20);
            _BackgroundPolygon[0] = new Vector2(_Bounds.X - 2, _Bounds.Y - 2);
            _BackgroundPolygon[1] = new Vector2(_Bounds.X - 2, _Bounds.Y + _Bounds.Height + 14);
            _BackgroundPolygon[2] = new Vector2(_Bounds.X + 2 + _Bounds.Width, _Bounds.Y + _Bounds.Height + 14);
            _BackgroundPolygon[3] = new Vector2(_Bounds.X + 2 + _Bounds.Width, _Bounds.Y - 2);

            base.LoadContent();
        }

        public override void Update(GameTime gameTime)
        {
            _ElapsedTime += gameTime.ElapsedGameTime;
            if (_ElapsedTime < TimeSpan.FromSeconds(1))
                return;

            _ElapsedTime -= TimeSpan.FromSeconds(1);
            FPS = _FrameCounter;
            _FrameCounter = 0;
        }

        public override void Draw(GameTime gameTime)
        {
            _FrameCounter++;

            {   
                // 保存状态
                var previousRasterizerState = Game.GraphicsDevice.RasterizerState;
                var previousDepthStencilState = Game.GraphicsDevice.DepthStencilState;

                Game.GraphicsDevice.RasterizerState = RasterizerState.CullNone;
                Game.GraphicsDevice.DepthStencilState = DepthStencilState.Default;

                _PrimitiveBatch.Begin(_LocalProjection, _LocalView);

                FKBasicShapesHelper.DrawSolidPolygon(_PrimitiveBatch, _BackgroundPolygon, 4, Color.Black, true);

                _PrimitiveBatch.End();

                Game.GraphicsDevice.RasterizerState = previousRasterizerState;
                Game.GraphicsDevice.DepthStencilState = previousDepthStencilState;
            }

            {
                if (_ChunkCache.ChunksDrawn >= 31)
                    _DrawnBlocks = (_ChunkCache.ChunksDrawn / 31.0f).ToString("F2") + "M";
                else if (_ChunkCache.ChunksDrawn > 1)
                    _DrawnBlocks = (_ChunkCache.ChunksDrawn / 0.03f).ToString("F2") + "K";
                else
                    _DrawnBlocks = "0";

                if (_ChunkStorage.Count > 31)
                    _TotalBlocks = (_ChunkStorage.Count / 31.0f).ToString("F2") + "M";
                else if (_ChunkStorage.Count > 1)
                    _TotalBlocks = (_ChunkStorage.Count / 0.03f).ToString("F2") + "K";
                else
                    _TotalBlocks = FKEngine.GetInstance.Config.Chunk.VolumeInBlocks.ToString(CultureInfo.InvariantCulture);
            }

            { // 使用 string.Format 特别慢，还是使用stringBuilder吧
                _SpriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend);

                _StringBuilder.Length = 0;
                _StringBuilder.Append("FPS:");
                _StringBuilder.Append(FPS);
                _SpriteBatch.DrawString(_SpriteFont, _StringBuilder, new Vector2(_Bounds.X + 5, _Bounds.Y + 5), Color.White);

                _StringBuilder.Length = 0;
                _StringBuilder.Append("Mem:");
                _StringBuilder.Append(MemoryUsed.GetKiloString());
                _SpriteBatch.DrawString(_SpriteFont, _StringBuilder, new Vector2(_Bounds.X + 105, _Bounds.Y + 5), Color.White);

                _StringBuilder.Length = 0;
                _StringBuilder.Append("Chunks:");
                _StringBuilder.AppendNumber(_ChunkCache.ChunksDrawn);
                _StringBuilder.Append("/");
                _StringBuilder.AppendNumber(_ChunkStorage.Count);
                _SpriteBatch.DrawString(_SpriteFont, _StringBuilder, new Vector2(_Bounds.X + 205, _Bounds.Y + 5), Color.White);

                _StringBuilder.Length = 0;
                _StringBuilder.Append("Blocks:");
                _StringBuilder.Append(_DrawnBlocks);
                _StringBuilder.Append("/");
                _StringBuilder.Append(_TotalBlocks);
                _SpriteBatch.DrawString(_SpriteFont, _StringBuilder, new Vector2(_Bounds.X + 305, _Bounds.Y + 5), Color.White);

                GenerateQueue = _ChunkCache.StateStatistics[RenderObj.ENUM_FKBaseChunkState.eAwaitingGenerate]
                    + _ChunkCache.StateStatistics[RenderObj.ENUM_FKBaseChunkState.eGenerating];
                LightenQueue = _ChunkCache.StateStatistics[RenderObj.ENUM_FKBaseChunkState.eAwaitingLighting]
                    + _ChunkCache.StateStatistics[RenderObj.ENUM_FKBaseChunkState.eLighting] + _ChunkCache.StateStatistics[RenderObj.ENUM_FKBaseChunkState.eAwaitingRelighting];
                BuildQueue = _ChunkCache.StateStatistics[RenderObj.ENUM_FKBaseChunkState.eAwaitingBuild]
                    + _ChunkCache.StateStatistics[RenderObj.ENUM_FKBaseChunkState.eBuilding] + _ChunkCache.StateStatistics[RenderObj.ENUM_FKBaseChunkState.eAwaitingRebuilding];
                ReadyQueue = _ChunkCache.StateStatistics[RenderObj.ENUM_FKBaseChunkState.eReady];
                RemoveQueue = _ChunkCache.StateStatistics[RenderObj.ENUM_FKBaseChunkState.eAwaitingRemoval];

                _StringBuilder.Length = 0;
                _StringBuilder.Append("GenQ:");
                _StringBuilder.Append(GenerateQueue);
                _SpriteBatch.DrawString(_SpriteFont, _StringBuilder, new Vector2(_Bounds.X + 450, _Bounds.Y + 5), Color.White);

                _StringBuilder.Length = 0;
                _StringBuilder.Append("LigQ:");
                _StringBuilder.Append(LightenQueue);
                _SpriteBatch.DrawString(_SpriteFont, _StringBuilder, new Vector2(_Bounds.X + 550, _Bounds.Y + 5), Color.White);

                _StringBuilder.Length = 0;
                _StringBuilder.Append("BuiQ:");
                _StringBuilder.Append(BuildQueue);
                _SpriteBatch.DrawString(_SpriteFont, _StringBuilder, new Vector2(_Bounds.X + 650, _Bounds.Y + 5), Color.White);

                _StringBuilder.Length = 0;
                _StringBuilder.Append("ReaQ:");
                _StringBuilder.Append(ReadyQueue);
                _SpriteBatch.DrawString(_SpriteFont, _StringBuilder, new Vector2(_Bounds.X + 750, _Bounds.Y + 5), Color.White);

                _StringBuilder.Length = 0;
                _StringBuilder.Append("RemQ:");
                _StringBuilder.Append(RemoveQueue);
                _SpriteBatch.DrawString(_SpriteFont, _StringBuilder, new Vector2(_Bounds.X + 850, _Bounds.Y + 5), Color.White);

                _StringBuilder.Length = 0;
                _StringBuilder.Append("Infinitive:");
                _StringBuilder.Append(FKEngine.GetInstance.Config.World.IsInfinitive ? "On" : "Off");
                _SpriteBatch.DrawString(_SpriteFont, _StringBuilder, new Vector2(_Bounds.X + 5, _Bounds.Y + 15), Color.White);

                _StringBuilder.Length = 0;
                _StringBuilder.Append("Fog:");
                _StringBuilder.Append(_Fogger.State);
                _SpriteBatch.DrawString(_SpriteFont, _StringBuilder, new Vector2(_Bounds.X + 105, _Bounds.Y + 15), Color.White);

                _StringBuilder.Length = 0;
                _StringBuilder.Append("Pos:");
                _StringBuilder.Append(_Camera.Position);
                _SpriteBatch.DrawString(_SpriteFont, _StringBuilder, new Vector2(_Bounds.X + 205, _Bounds.Y + 15), Color.White);

                _SpriteBatch.End();
            }

            base.Draw(gameTime);
        }
    }
}