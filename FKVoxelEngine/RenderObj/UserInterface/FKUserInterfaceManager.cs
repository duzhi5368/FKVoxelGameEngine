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
// Create Time         :    2017/8/9 10:26:50
// Update Time         :    2017/8/9 10:26:50
// Class Version       :    v1.0.0.0
// Class Description   :    
// ===============================================================================
using FKVoxelEngine.Base;
using FKVoxelEngine.Framework;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
// ===============================================================================
namespace FKVoxelEngine.RenderObj
{
    public class FKUserInterfaceManager : DrawableGameComponent, IFKUserInterfaceManagerService
    {
        private IFKAssetManagerService _AssetManager;

        private Dictionary<ENUM_UserInterfaceType, FKUI_Base> _UIMap = new Dictionary<ENUM_UserInterfaceType, FKUI_Base>();
        private ENUM_UserInterfaceType _NextUIType = ENUM_UserInterfaceType.eUI_None;
        private FKUI_Base _CurrentUI = null;

        private static readonly FKLogger Logger = FKLogManager.CreateLogger("FKUI");

        public FKUserInterfaceManager(Game game)
            : base(game)
        {
            Game.Services.AddService(typeof(IFKUserInterfaceManagerService), this);
        }

        public override void Initialize()
        {
            Logger.Trace("Init()");

            _AssetManager = (IFKAssetManagerService)Game.Services.GetService(typeof(IFKAssetManagerService));
            if (_AssetManager == null)
                throw new NullReferenceException("Can not find asset manager component.");

            // 添加子UI项
            _UIMap.Add(ENUM_UserInterfaceType.eUT_Test, (new FKUI_Test(this)).Initialize());

            // 设置初始UI
            _NextUIType = ENUM_UserInterfaceType.eUT_Test;

            base.Initialize();
        }

        public override void Update(GameTime gameTime)
        {
            if (_CurrentUI == null)
                return;
            if (_CurrentUI.GetUIType() != _NextUIType)
            {
                FKUI_Base next = null;
                if (_UIMap.TryGetValue(_NextUIType, out next))
                {
                    _CurrentUI.Leave();
                    _CurrentUI = next;
                    _CurrentUI.Enter();
                }
            }

            _CurrentUI.Update(gameTime);

            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            if (_CurrentUI == null)
                return;

            _CurrentUI.Draw(gameTime);

            base.Draw(gameTime);
        }

        public void ChangeUI(ENUM_UserInterfaceType type)
        {
            _NextUIType = type;
        }
    }
}