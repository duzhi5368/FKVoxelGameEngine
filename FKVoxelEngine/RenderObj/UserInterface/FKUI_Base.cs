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
// Create Time         :    2017/8/9 10:31:17
// Update Time         :    2017/8/9 10:31:17
// Class Version       :    v1.0.0.0
// Class Description   :    
// ===============================================================================
using FKVoxelEngine.Framework;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
// ===============================================================================
namespace FKVoxelEngine.RenderObj
{
    public class FKUI_Base
    {
        protected FKUserInterfaceManager _UIManager;

        protected IFKAssetManagerService _AssetManager;

        protected SpriteBatch _SpriteBatch;
        public FKUI_Base(FKUserInterfaceManager UIManager)
        {
            _UIManager = UIManager;
            _AssetManager = (IFKAssetManagerService)_UIManager.Game.Services.GetService(typeof(IFKAssetManagerService));
        }

        public virtual ENUM_UserInterfaceType GetUIType()
        {
            return ENUM_UserInterfaceType.eUI_None;
        }

        public virtual FKUI_Base Initialize()
        {
            _SpriteBatch = new SpriteBatch(_UIManager.Game.GraphicsDevice);
            return this;
        }

        public virtual void Enter()
        {

        }

        public virtual void Update(GameTime gameTime)
        {

        }

        public virtual void Draw(GameTime gameTime)
        {

        }

        public virtual void Leave()
        {

        }
    }
}