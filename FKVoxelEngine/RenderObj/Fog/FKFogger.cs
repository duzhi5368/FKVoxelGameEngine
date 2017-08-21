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
// Create Time         :    2017/8/8 15:52:12
// Update Time         :    2017/8/8 15:52:12
// Class Version       :    v1.0.0.0
// Class Description   :    
// ===============================================================================
using FKVoxelEngine.Base;
using FKVoxelEngine.Framework;
using Microsoft.Xna.Framework;
using System;
// ===============================================================================
namespace FKVoxelEngine.RenderObj
{
    public class FKFogger : GameComponent, IFKFoggerService
    {
        public ENUM_FogState State { get; set; }
        public Vector2 FogVector
        {
            get { return _FogVector[(byte)State]; }
        }
        private readonly Vector2[] _FogVector = new[]
        {
            new Vector2(0, 0),      // None
            new Vector2(FKBaseChunk.WidthInBlocks * (FKBaseChunkCache.ViewRange - 7), FKBaseChunk.WidthInBlocks * (FKBaseChunkCache.ViewRange)), // Near
            new Vector2(FKBaseChunk.WidthInBlocks * (FKBaseChunkCache.ViewRange - 2), FKBaseChunk.WidthInBlocks * (FKBaseChunkCache.ViewRange)), // Far
        };


        private static readonly FKLogger Logger = FKLogManager.CreateLogger("FKFogger");

        public FKFogger(Game game)
            : base(game)
        {
            Game.Services.AddService(typeof(IFKFoggerService), this);
        }

        public override void Initialize()
        {
            Logger.Trace("Init()");

            State = ENUM_FogState.eNone;

            base.Initialize();
        }
    }
}