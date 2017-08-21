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
// Create Time         :    2017/7/26 13:09:14
// Update Time         :    2017/7/26 13:09:14
// Class Version       :    v1.0.0.0
// Class Description   :    
// ===============================================================================
using Microsoft.Xna.Framework;
using System;
using FKVoxelEngine.Base;
using FKVoxelEngine.Framework;
using System.Collections.Generic;
// ===============================================================================
namespace FKVoxelEngine.RenderObj
{
    public class FKBaseBlockStorage : GameComponent, IFKBaseBlockStorageService
    {
        public static FKBaseBlock[] Blocks;
        public static readonly int XStep = CacheLengthInBlocks * FKBaseChunk.HeightInBlocks;
        public static readonly int ZStep = FKBaseChunk.HeightInBlocks;
        public static int CacheWidthInBlocks = ((FKBaseChunkCache.CacheRange * 2) + 1) * FKBaseChunk.WidthInBlocks;
        public static int CacheLengthInBlocks = ((FKBaseChunkCache.CacheRange * 2) + 1) * FKBaseChunk.LengthInBlocks;
        private IFKBaseChunkCacheService _ChunkCache;
        private static readonly FKLogger Logger = FKLogManager.CreateLogger("FKBaseBlockStorage");


        public FKBaseBlockStorage(Game game)
            : base(game)
        {
            Game.Services.AddService(typeof(IFKBaseBlockStorageService), this);
        }

        public override void Initialize()
        {
            Logger.Trace("Init()");

            _ChunkCache = (IFKBaseChunkCacheService)Game.Services.GetService(typeof(IFKBaseChunkCacheService));
            Blocks = new FKBaseBlock[CacheWidthInBlocks * CacheLengthInBlocks * FKBaseChunk.HeightInBlocks];

            base.Initialize();
        }
        public FKBaseBlock GetBlockAt(FKVector3Int position)
        {
            return GetBlockAt(position.X, position.Y, position.Z);
        }

        public FKBaseBlock GetBlockAt(Vector3 position)
        {
            return GetBlockAt((int)position.X, (int)position.Y, (int)position.Z);
        }

        public FKBaseBlock GetBlockAt(int x, int y, int z)
        {
            if (!FKBaseChunkCache.IsInBounds(x, y, z))
                return FKBaseBlock.Empty;

            var flattenIndex = GetBlockIndexByWorldPosition(x, y, z);
            return Blocks[flattenIndex];
        }

        public List<FKBaseChunk.ENUM_Edges> GetChunkEdgesBlockIsIn(int x, int y, int z)
        {
            var edges = new List<FKBaseChunk.ENUM_Edges>();

            edges.Add(FKBaseChunk.ENUM_Edges.XDecreasing);
            edges.Add(FKBaseChunk.ENUM_Edges.XIncreasing);
            edges.Add(FKBaseChunk.ENUM_Edges.ZDecreasing);
            edges.Add(FKBaseChunk.ENUM_Edges.ZIncreasing);

            return edges;
        }

        public static int GetBlockIndexByWorldPosition(int x, int z)
        {
            var warpX = x % CacheWidthInBlocks;
            if (warpX < 0)
                warpX += CacheWidthInBlocks;

            var warpZ = z % CacheLengthInBlocks;
            if (warpZ < 0)
                warpZ += CacheLengthInBlocks;

            var flattenIndex = warpX * XStep + warpZ * ZStep;
            return flattenIndex;
        }

        public static int GetBlockIndexByWorldPosition(int x, int y, int z)
        {
            return GetBlockIndexByWorldPosition(x, z) + y;
        }

        public static int GetBlockIndexByRelativePosition(FKBaseChunk chunk, int x, int z)
        {
            var xIndex = chunk.WorldPosition.X + x;
            var zIndex = chunk.WorldPosition.Z + z;

            var warpX = xIndex % CacheWidthInBlocks;
            if (warpX < 0)
                warpX += CacheWidthInBlocks;

            var warpZ = zIndex % CacheLengthInBlocks;
            if (warpZ < 0)
                warpZ += CacheLengthInBlocks;

            var flattenIndex = warpX * XStep + warpZ * ZStep;
            return flattenIndex;
        }

        public static int GetBlockIndexByRelativePosition(FKBaseChunk chunk, int x, int y, int z)
        {
            return GetBlockIndexByRelativePosition(chunk, x, z) + y;
        }

        public void SetBlockAt(FKVector3Int position, FKBaseBlock block)
        {
            SetBlockAt(position.X, position.Y, position.Z, block);
        }

        public void SetBlockAt(Vector3 position, FKBaseBlock block)
        {
            SetBlockAt((int)position.X, (int)position.Y, (int)position.Z, block);
        }

        public void SetBlockAt(int x, int y, int z, FKBaseBlock block)
        {
            var chunk = _ChunkCache.GetChunkByWorldPosition(x, z);
            if (chunk == null)
                return;

            var flattenIndex = GetBlockIndexByWorldPosition(x, y, z);
            Blocks[flattenIndex] = block;

            // 如果是边缘block
            var edgesBlockIsIn = GetChunkEdgesBlockIsIn(x, y, z);
            foreach(var edge in edgesBlockIsIn)
            {
                var neighborChunk = _ChunkCache.GetNeighborChunk(chunk, edge);
                neighborChunk.ChunkState = ENUM_FKBaseChunkState.eAwaitingRelighting;
            }

            chunk.ChunkState = ENUM_FKBaseChunkState.eAwaitingRelighting;
        }
    }
}