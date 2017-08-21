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
// Create Time         :    2017/7/21 13:19:21
// Update Time         :    2017/7/21 13:19:21
// Class Version       :    v1.0.0.0
// Class Description   :    
// ===============================================================================
using FKVoxelEngine.Base;
using Microsoft.Xna.Framework;
using System;
// ===============================================================================
namespace FKVoxelEngine.Platform
{
    public class FKPlatformManager : FKSingleton<FKPlatformManager>
    {
        #region ==== 全局变量 =====

        public FKPlatformEnum        CurPlatform { get; private set; }
        public FKFrameworkEnum       CurGameFramework { get; private set; }
        public FKGraphicsAPIEnum     CurGraphicsAPI { get; private set; }
        public Version               DotNetFrameworkVersion { get; private set; }
        public Version               GameFrameworkVersion { get; private set; }
        public FKPlatformHandler     Handler { get; private set; }
        public FKPlatformHelper      Helper { get; private set; }

        public Game                  Game { get; private set; }
        public GraphicsDeviceManager GraphicsDeviceManager { get; private set; }

        #endregion ==== 全局变量 =====

        #region ==== 内部函数 =====

        private FKPlatformManager()
        {
            IdentifyPlatform();
        }

        /// <summary>
        /// 识别平台信息
        /// </summary>
        private void IdentifyPlatform()
        {
#if WINDOWS && DESKTOP
                CurPlatform = FKPlatformEnum.ePlatform_Windows;
                Handler = new WindowsPlatformHandler();
                Helper = new WindowsPlatformHelper();
#elif WINDOWS && METRO
                CurPlatform = FKPlatformEnum.ePlatform_WindowsMetro;
                Handler = new WindowsMetroPlatformHandler();
                Helper = new WindowsMetroPlatformHelper();
#elif LINUX && DESKTOP
                CurPlatform = FKPlatformEnum.ePlatform_Linux;
                Handler = new LinuxPlatformHandler();
#elif MACOS && DESKTOP
                CurPlatform = FKPlatformEnum.ePlatform_MacOS;
                Handler = new MacOSPlatformHandler();
#elif WINPHONE7
                CurPlatform = FKPlatformEnum.ePlatform_WindowsPhone7;
                Handler = new WindowsPhone7PlatformHandler();
                Helper = new WindowsPhone7PlatformHelper();
#elif WINPHONE8
                CurPlatform = FKPlatformEnum.ePlatform_WindowsPhone8;
                Handler = new WindowsPhone8PlatformHandler();
                Helper = new WindowsPhone8PlatformHelper();
#elif ANDROID
                CurPlatform = FKPlatformEnum.ePlatform_Android;
                Handler = new AndroidPlatformHandler();
#elif IOS
                CurPlatform = FKPlatformEnum.ePlatform_iOS;
                Handler = new IOSPlatformHandler();
#endif

            if (Handler == null)
            {
                throw new Exception("Unknown platform! Please check your App macros define.");
            }

            // 检查环境版本
#if METRO
            DotNetFrameworkVersion = System.Reflection.IntrospectionExtensions.GetTypeInfo(typeof(Object)).Assembly.GetName().Version;
            GameFrameworkVersion = System.Reflection.IntrospectionExtensions.GetTypeInfo(typeof(Microsoft.Xna.Framework.Game)).Assembly.GetName().Version;
#else
            DotNetFrameworkVersion = Environment.Version;
    #if WINPHONE7 || WINPHONE8
            GameFrameworkVersion = new Version(typeof(Microsoft.Xna.Framework.Game).Assembly.FullName.Split(',')[1].Split('=')[1]);
    #else
            GameFrameworkVersion = System.Reflection.Assembly.GetAssembly(typeof(Microsoft.Xna.Framework.Game)).GetName().Version;
#endif
#endif
            if (!FKSystemFuncs.IsRunningOnMono())
            {
                CurGameFramework = FKFrameworkEnum.eFramework_XNA;
                CurGraphicsAPI = FKGraphicsAPIEnum.eGraphicsAPI_DirectX9;
            }
            else
            {
                CurGameFramework = FKFrameworkEnum.eFramework_MonoGame;
#if DIRECTX11
                CurGraphicsAPI = FKGraphicsAPIEnum.eGraphicsAPI_DirectX11;
#elif OPENGL
                CurGraphicsAPI = FKGraphicsAPIEnum.eGraphicsAPI_OpenGL;
#endif
            }
        }

        #endregion ==== 内部函数 =====

        #region ==== 对外接口 =====

        /// <summary>
        /// 在程序启动时第一时间启用，可以丢到MonoGameClient的构造中
        /// </summary>
        /// <param name="game"></param>
        /// <param name="graphicsDeviceManager"></param>
        public void Startup(Game game, GraphicsDeviceManager gd)
        {
            Game = game;
            GraphicsDeviceManager = gd;

            // 启动平台初始化
            Handler.PlatformStartup();
        }
        /// <summary>
        /// 在MonoGameClient的Initialize()中
        /// </summary>
        public void Init()
        {
            Handler.Initialize();
        }

        #endregion ==== 对外接口 =====
    }
}