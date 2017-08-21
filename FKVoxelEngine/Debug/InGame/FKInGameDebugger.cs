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
// Create Time         :    2017/7/26 13:50:23
// Update Time         :    2017/7/26 13:50:23
// Class Version       :    v1.0.0.0
// Class Description   :    
// ===============================================================================
using FKVoxelEngine.Base;
using FKVoxelEngine.Framework;
using FKVoxelEngine.Graphics;
using FKVoxelEngine.RenderObj;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
// ===============================================================================
namespace FKVoxelEngine.Debug
{
    public sealed class FKInGameDebugger : DrawableGameComponent, IFKInGameDebuggerService
    {
        private static readonly FKLogger Logger = FKLogManager.CreateLogger("FKInGameDebugger");

        private SpriteBatch                 _SpriteBatch;
        private SpriteFont                  _SpriteFont;
        private bool                        _bIsActive = false;

        private IFKWorldService             _WorldService;
        private IFKCameraService            _CameraService;
        private IFKBaseChunkStorageService  _ChunkStorageService;
        private IFKAssetManagerService      _AssetManagerService;

        public FKInGameDebugger(Game game) 
            : base(game)
        {
            game.Services.AddService(typeof(IFKInGameDebuggerService), this);
        }

        public override void Initialize()
        {
            Logger.Trace("Init()");

            _WorldService = (IFKWorldService)(Game.Services.GetService(typeof(IFKWorldService)));
            _CameraService = (IFKCameraService)(Game.Services.GetService(typeof(IFKCameraService)));
            _ChunkStorageService = (IFKBaseChunkStorageService)(Game.Services.GetService(typeof(IFKBaseChunkStorageService)));
            _AssetManagerService = (IFKAssetManagerService)(Game.Services.GetService(typeof(IFKAssetManagerService)));

            _SpriteFont = _AssetManagerService == null ? null : _AssetManagerService.DefaultFont;
            _SpriteBatch = new SpriteBatch(Game.GraphicsDevice);

            base.Initialize();
        }

        public override void Draw(GameTime gameTime)
        {
            if (!_bIsActive)
            {
                return;
            }

            _SpriteBatch.Begin();

            var viewFrustrum = new BoundingFrustum(_CameraService.View * _CameraService.Projection);
            foreach (FKBaseChunk chunk in _ChunkStorageService.Values)
            {
                //if (chunk != _CameraService.CurrentChunk)
                //    continue;

                if (!chunk.BoundingBox.Intersects(viewFrustrum)) 
                    continue;

                chunk.DrawInGameDebugVisual(Game.GraphicsDevice, _CameraService, _SpriteBatch, _SpriteFont);
            }

            _SpriteBatch.End();
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
        }

        public void ToggleInGameDebugger()
        {
            _bIsActive = !_bIsActive;
        }
    }
}