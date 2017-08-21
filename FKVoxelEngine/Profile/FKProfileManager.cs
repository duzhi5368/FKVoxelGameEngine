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
// Create Time         :    2017/8/2 18:57:13
// Update Time         :    2017/8/2 18:57:13
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
// ===============================================================================
namespace FKVoxelEngine.Profile
{
    public class FKProfileManager : DrawableGameComponent, IFKProfileManagerService
    {
        private FKPrimitiveBatch _PrimitiveBatch;
        private SpriteBatch _SpriteBatch;
        private SpriteFont _SpriteFont;
        private Matrix _LocalProjection;
        private Matrix _LocalView;
        private IFKAssetManagerService _AssetManager;
        private bool _bIsUseProfile;
        private readonly List<FKDebugProfile> _Profiles = new List<FKDebugProfile>();
        private static readonly FKLogger Logger = FKLogManager.CreateLogger("FKProfileManager");
        public FKProfileManager(Game game)
            : base(game)
        {
            game.Services.AddService(typeof(IFKProfileManagerService), this);
        }

        public void ToggleProfileShow()
        {
            _bIsUseProfile = !_bIsUseProfile;
        }
        public override void Initialize()
        {
            Logger.Trace("Init()");

            _Profiles.Add(new FKFPSProfile(Game, new Rectangle(FKEngine.GetInstance.Config.Graphics.Width - 280, 50, 270, 35)));
            _Profiles.Add(new FKMemoryProfile(Game, new Rectangle(FKEngine.GetInstance.Config.Graphics.Width - 280, 105, 270, 35)));
            _Profiles.Add(new FKGenerateQueueProfile(Game, new Rectangle(FKEngine.GetInstance.Config.Graphics.Width - 280, 160, 270, 35)));
            _Profiles.Add(new FKLightenQueueProfile(Game, new Rectangle(FKEngine.GetInstance.Config.Graphics.Width - 280, 215, 270, 35)));
            _Profiles.Add(new FKBuildQueueProfile(Game, new Rectangle(FKEngine.GetInstance.Config.Graphics.Width - 280, 270, 270, 35)));
            _Profiles.Add(new FKReadyQueueProfile(Game, new Rectangle(FKEngine.GetInstance.Config.Graphics.Width - 280, 325, 270, 35)));
            _Profiles.Add(new FKRemoveQueueProfile(Game, new Rectangle(FKEngine.GetInstance.Config.Graphics.Width - 280, 380, 270, 35)));

            _AssetManager = (IFKAssetManagerService)Game.Services.GetService(typeof(IFKAssetManagerService));
            if (_AssetManager == null)
                throw new NullReferenceException("Can't find  asset manager component.");

            _bIsUseProfile = FKEngine.GetInstance.Config.Debug.GraphicsEnabled;

            base.Initialize();
        }

        protected override void LoadContent()
        {
            _PrimitiveBatch = new FKPrimitiveBatch(GraphicsDevice, 1000);
            _SpriteBatch = new SpriteBatch(GraphicsDevice);
            _SpriteFont = _AssetManager.DefaultFont;
            _LocalProjection = Matrix.CreateOrthographicOffCenter(0.0f, GraphicsDevice.Viewport.Width, GraphicsDevice.Viewport.Height, 0.0f, 0.0f, 1.0f);
            _LocalView = Matrix.Identity;

            foreach(var profile in _Profiles)
            {
                profile.AttachGraphics(_PrimitiveBatch, _SpriteBatch, _SpriteFont, _LocalProjection, _LocalView);
            }

            base.LoadContent();
        }

        public override void Draw(GameTime gameTime)
        {
            if (!_bIsUseProfile)
                return;

            // 保存状态
            var previousRasterizerState = Game.GraphicsDevice.RasterizerState;
            var previousDepthStencilState = Game.GraphicsDevice.DepthStencilState;

            Game.GraphicsDevice.RasterizerState = RasterizerState.CullNone;
            Game.GraphicsDevice.DepthStencilState = DepthStencilState.Default;

            // 画背景块
            _PrimitiveBatch.Begin(_LocalProjection, _LocalView);
            foreach(var profile in _Profiles)
            {
                profile.DrawGraph(gameTime);
            }
            _PrimitiveBatch.End();

            // 绘制文字
            _SpriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend);
            foreach(var profile in _Profiles)
            {
                profile.DrawString(gameTime);
            }
            _SpriteBatch.End();

            // 恢复状态
            Game.GraphicsDevice.RasterizerState = previousRasterizerState;
            Game.GraphicsDevice.DepthStencilState = previousDepthStencilState;

            base.Draw(gameTime);
        }

        public override void Update(GameTime gameTime)
        {
            if (!_bIsUseProfile)
                return;

            foreach(var profile in _Profiles)
            {
                profile.Update(gameTime);
            }

            base.Update(gameTime);
        }
    }
}