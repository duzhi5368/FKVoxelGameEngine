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
// Create Time         :    2017/7/21 17:07:00
// Update Time         :    2017/7/21 17:07:00
// Class Version       :    v1.0.0.0
// Class Description   :    
// ===============================================================================
using System;
#if MACOS && DESKTOP
using MonoMac.AppKit;
using MonoMac.Foundation;
#endif
// ===============================================================================
namespace FKVoxelEngine.Platform
{
    public class MacOSPlatformHandler : FKPlatformHandler
    {
        public MacOSPlatformHandler()
        {
            this.Config = new FKPlatformConfig
            {
                ScreenConfig =
                {
                    IsFullScreen = false,
                    Width = 1280,
                    Height = 720,
                },
                InputConfig =
                {
                    IsMouseVisible = true,
                },
                GraphicsConfig =
                {
                    IsFixedTimeStep = false,
                    IsVsyncEnabled = false,
                    IsPostprocessEnabled = false,
                    IsExendedEffectsEnabled = true,
                },
            };
        }

        internal override void PlatformStartup()
        {
#if MACOS && DESKTOP
            NSApplication.Init();

            using (var p = new NSAutoreleasePool())
            {
                NSApplication.SharedApplication.Delegate = new AppDelegate();
                NSApplication.Main(null);
            }
#endif
        }
    }

#if MACOS && DESKTOP
    class AppDelegate : NSApplicationDelegate
    {
        public override void FinishedLaunching(MonoMac.Foundation.NSObject notification)
        {
            using (var game = FKPlatformManager.Game)
            {
                game.Run();
            }
        }

        public override bool ApplicationShouldTerminateAfterLastWindowClosed(NSApplication sender)
        {
            return true;
        }
    }
#endif
}