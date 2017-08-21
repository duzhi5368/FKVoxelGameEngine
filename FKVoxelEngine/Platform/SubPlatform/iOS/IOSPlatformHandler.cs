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
// Create Time         :    2017/7/21 16:36:36
// Update Time         :    2017/7/21 16:36:36
// Class Version       :    v1.0.0.0
// Class Description   :    
// ===============================================================================
using System;
using Microsoft.Xna.Framework;
// ===============================================================================
namespace FKVoxelEngine.Platform
{
    public class IOSPlatformHandler : FKPlatformHandler
    {
        public IOSPlatformHandler()
        {
            this.Config = new FKPlatformConfig
            {
                ScreenConfig =
                {
                    IsFullScreen = true,
                    Width = 480,
                    Height = 800,
#if IOS
                    SupportedOrientations = DisplayOrientation.FaceDown | DisplayOrientation.FaceUp,
#endif
                },
                InputConfig =
                {
                    IsMouseVisible = false,
                },
                GraphicsConfig =
                {
                    IsFixedTimeStep = false,
                    IsVsyncEnabled = false,
                    IsPostprocessEnabled = true,
                    IsExendedEffectsEnabled = false,
                },
            };
        }
        internal override void PlatformStartup()
        {
            using (var game = FKPlatformManager.GetInstance.Game)
            {
                game.Run();
            }
        }

    }
}