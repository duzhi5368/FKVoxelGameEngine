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
// Create Time         :    2017/8/1 15:21:07
// Update Time         :    2017/8/1 15:21:07
// Class Version       :    v1.0.0.0
// Class Description   :    
// ===============================================================================
using FKVoxelEngine.Base;
using FKVoxelEngine.RenderObj;
using Microsoft.Xna.Framework;
using System;
// ===============================================================================
namespace FKVoxelEngine.Framework
{
    public class FKWorld : DrawableGameComponent, IFKWorldService
    {
        public static Vector4 NightColor = Color.Black.ToVector4();
        public static Vector4 SunColor = Color.White.ToVector4();
        public static Vector4 HorizonColor = Color.DarkGray.ToVector4();
        public static Vector4 EveningTint = Color.Red.ToVector4();
        public static Vector4 MorningTint = Color.Gold.ToVector4();

        private IFKBaseChunkStorageService _ChunkStorage;
        private IFKBaseChunkCacheService _ChunkCache;
        private IFKCameraService _Camera;
        private static readonly FKLogger _Logger = FKLogManager.CreateLogger("FKWorld");

        public FKWorld(Game game)
            :base(game)
        {
            Game.Services.AddService(typeof(IFKWorldService), this);
        }

        public override void Initialize()
        {
            _Logger.Trace("Init()");

            _ChunkStorage = (IFKBaseChunkStorageService)Game.Services.GetService(typeof(IFKBaseChunkStorageService));
            _ChunkCache = (IFKBaseChunkCacheService)Game.Services.GetService(typeof(IFKBaseChunkCacheService));
            _Camera = (IFKCameraService)Game.Services.GetService(typeof(IFKCameraService));

            _Camera.SetCamera(new FKVector2Int(0, 0));
            _Camera.LookAt(Vector3.Forward);

            base.Initialize();
        }

        public void SetCamera(FKVector2Int relativePosition)
        {
            for (int z = -FKBaseChunkCache.CacheRange; z <= FKBaseChunkCache.CacheRange; z++)
            {
                for (int x = -FKBaseChunkCache.CacheRange; x <= FKBaseChunkCache.CacheRange; x++)
                {
                    var chunk = new FKBaseChunk(new FKVector2Int(relativePosition.X + x, relativePosition.Z + z));
                    _ChunkStorage[chunk.RelativePosition.X, chunk.RelativePosition.Z] = chunk;

                    if (chunk.RelativePosition == relativePosition)
                        _Camera.CurrentChunk = chunk;
                }
            }

            _ChunkStorage.SouthWestEdge = new FKVector2Int(relativePosition.X - FKBaseChunkCache.ViewRange,
                                                        relativePosition.Z - FKBaseChunkCache.ViewRange);
            _ChunkStorage.NorthEastEdge = new FKVector2Int(relativePosition.X + FKBaseChunkCache.ViewRange,
                                                        relativePosition.Z + FKBaseChunkCache.ViewRange);

            FKBaseChunkCache.BoundingBox =
                new BoundingBox(
                    new Vector3(_ChunkStorage.SouthWestEdge.X * FKBaseChunk.WidthInBlocks, 0, _ChunkStorage.SouthWestEdge.Z * FKBaseChunk.LengthInBlocks),
                    new Vector3((_ChunkStorage.NorthEastEdge.X + 1) * FKBaseChunk.WidthInBlocks, FKBaseChunk.HeightInBlocks, (_ChunkStorage.NorthEastEdge.Z + 1) * FKBaseChunk.LengthInBlocks));
        }
    }
}