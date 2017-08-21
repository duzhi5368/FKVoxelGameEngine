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
// Create Time         :    2017/8/11 14:32:04
// Update Time         :    2017/8/11 14:32:04
// Class Version       :    v1.0.0.0
// Class Description   :    
// ===============================================================================
using FKVoxelEngine.Base;
using FKVoxelEngine.Framework;
using Microsoft.Xna.Framework;
using System;
// ===============================================================================
namespace FKVoxelEngine.Graphics
{
    public class FKGraphicsManager : IFKGraphicsManagerService
    {
        public bool FixedTimeStepsEnabled { get; private set; }
        public bool FullScreenEnabled { get; private set; }
        public bool VerticalSyncEnabled { get; private set; }

        private readonly Game _Game;
        private readonly GraphicsDeviceManager _GraphicsDeviceManager;

        private static readonly FKLogger Logger = FKLogManager.CreateLogger("FKGraphicsManager");

        public FKGraphicsManager(Game game, GraphicsDeviceManager gdm)
        {
            Logger.Trace("ctor()");

            _Game = game;
            _GraphicsDeviceManager = gdm;
            _Game.Services.AddService(typeof(IFKGraphicsManagerService), this);

            FullScreenEnabled = _GraphicsDeviceManager.IsFullScreen = FKEngine.GetInstance.Config.Graphics.FullScreenEnabled;
            _GraphicsDeviceManager.PreferredBackBufferWidth = FKEngine.GetInstance.Config.Graphics.Width;
            _GraphicsDeviceManager.PreferredBackBufferHeight = FKEngine.GetInstance.Config.Graphics.Height;
            FixedTimeStepsEnabled = _Game.IsFixedTimeStep = FKEngine.GetInstance.Config.Graphics.FixedTimeStepsEnabled;
            VerticalSyncEnabled = _GraphicsDeviceManager.SynchronizeWithVerticalRetrace = FKEngine.GetInstance.Config.Graphics.VerticalSyncEnabled;

            _GraphicsDeviceManager.ApplyChanges();
        }

        public void EnableFullScreen(bool enabled)
        {
            FullScreenEnabled = enabled;
            _GraphicsDeviceManager.IsFullScreen = FullScreenEnabled;
            _GraphicsDeviceManager.ApplyChanges();
        }

        public void EnableVerticalSync(bool enabled)
        {
            VerticalSyncEnabled = enabled;
            _GraphicsDeviceManager.SynchronizeWithVerticalRetrace = VerticalSyncEnabled;
            _GraphicsDeviceManager.ApplyChanges();
        }

        public void ToggleFixedTimeSteps()
        {
            FixedTimeStepsEnabled = !FixedTimeStepsEnabled;
            _Game.IsFixedTimeStep = FixedTimeStepsEnabled;
            _GraphicsDeviceManager.ApplyChanges();
        }
    }
}