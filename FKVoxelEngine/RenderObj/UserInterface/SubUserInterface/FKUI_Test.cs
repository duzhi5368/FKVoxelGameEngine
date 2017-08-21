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
// Create Time         :    2017/8/9 10:43:59
// Update Time         :    2017/8/9 10:43:59
// Class Version       :    v1.0.0.0
// Class Description   :    
// ===============================================================================
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using System;
using FKVoxelEngine.Framework;
// ===============================================================================
namespace FKVoxelEngine.RenderObj
{
    public class FKUI_Test : FKUI_Base
    {
        private Texture2D _CrosshairNormalTexture;
        private Texture2D _CrosshairShovelTexture;

        public FKUI_Test(FKUserInterfaceManager UIManager) 
            : base(UIManager)
        {

        }

        public override ENUM_UserInterfaceType GetUIType()
        {
            return ENUM_UserInterfaceType.eUT_Test;
        }

        public override FKUI_Base Initialize()
        {
            _CrosshairNormalTexture = _AssetManager.CrossHairNormalTexture;
            _CrosshairShovelTexture = _AssetManager.CrossHairShovelTexture;

            base.Initialize();
            return this;
        }

        public override void Enter()
        {

        }

        public override void Update(GameTime gameTime)
        {

        }

        public override void Draw(GameTime gameTime)
        {
            var crosshairTexture = _CrosshairNormalTexture;

            _SpriteBatch.Begin(SpriteSortMode.Immediate, BlendState.AlphaBlend);
            _SpriteBatch.Draw(crosshairTexture,
                              new Vector2((_UIManager.Game.GraphicsDevice.Viewport.Width / 2) - 10,
                                          (_UIManager.Game.GraphicsDevice.Viewport.Height / 2) - 10), Color.White);
            _SpriteBatch.End();
        }

        public override void Leave()
        {

        }
    }
}