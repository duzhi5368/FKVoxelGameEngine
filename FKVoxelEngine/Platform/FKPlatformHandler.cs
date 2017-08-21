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
// Create Time         :    2017/7/21 13:34:38
// Update Time         :    2017/7/21 13:34:38
// Class Version       :    v1.0.0.0
// Class Description   :    
// ===============================================================================
using Microsoft.Xna.Framework;
// ===============================================================================

namespace FKVoxelEngine.Platform
{
    /// <summary>
    /// 基类，所有平台将继承本类
    /// </summary>
    public class FKPlatformHandler
    {
        #region ==== 成员变量 ====

        /// <summary>
        /// 配置信息组
        /// </summary>
        public FKPlatformConfig Config { get; set; }

        #endregion ==== 成员变量 ====

        #region ==== 继承函数 ====

        /// <summary>
        /// 子类各平台进行继承，在程序启动时调用
        /// </summary>
        internal virtual void PlatformStartup() { }
        /// <summary>
        /// 子类各平台进行继承，平台初始化调用
        /// </summary>
        protected virtual void Init() { }

        #endregion ==== 继承函数 ====

        #region ==== 对外接口 ====

        /// <summary>
        /// 平台初始化
        /// </summary>
        /// <param name="game"></param>
        /// <param name="graphicsDeviceManager"></param>
        public void Initialize()
        {
            Game Game = FKPlatformManager.GetInstance.Game;
            GraphicsDeviceManager GraphicsDeviceManager = FKPlatformManager.GetInstance.GraphicsDeviceManager;

            // 加载配置
            if(Config.ScreenConfig.Width != 0 && Config.ScreenConfig.Height != 0)
            {
                GraphicsDeviceManager.PreferredBackBufferWidth = Config.ScreenConfig.Width;
                GraphicsDeviceManager.PreferredBackBufferHeight = Config.ScreenConfig.Height;
            }
            GraphicsDeviceManager.IsFullScreen = Config.ScreenConfig.IsFullScreen;
            GraphicsDeviceManager.SynchronizeWithVerticalRetrace = Config.GraphicsConfig.IsVsyncEnabled;
            GraphicsDeviceManager.SupportedOrientations = Config.ScreenConfig.SupportedOrientations;

            Game.IsFixedTimeStep = Config.GraphicsConfig.IsFixedTimeStep;
            Game.IsMouseVisible = Config.InputConfig.IsMouseVisible;

            // 自己的Init
            this.Init();

            // 修改配置
            GraphicsDeviceManager.ApplyChanges();
        }

        #endregion ==== 对外接口 ====
    }
}