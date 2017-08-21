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
// Create Time         :    2017/7/21 15:46:05
// Update Time         :    2017/7/21 15:46:05
// Class Version       :    v1.0.0.0
// Class Description   :    
// ===============================================================================
using System;
// ===============================================================================
namespace FKVoxelEngine.Platform
{
    public class WindowsPlatformHandler : FKPlatformHandler
    {
        public WindowsPlatformHandler()
        {
            this.Config = new FKPlatformConfig
            {
                ScreenConfig =
                {
                    IsFullScreen = false,
                    Width = 800,
                    Height = 600,
                },
                InputConfig =
                {
                    IsMouseVisible = true,
                },
                GraphicsConfig =
                {
                    IsFixedTimeStep = false,
                    IsVsyncEnabled = false,
                    IsPostprocessEnabled = true,
                    IsExendedEffectsEnabled = true,
                },
            };
        }

        internal override void PlatformStartup()
        {
            using(var game = FKPlatformManager.GetInstance.Game)
            {
                game.Run();
            }
        }
    }
}