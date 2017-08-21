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
// Create Time         :    2017/8/14 11:54:00
// Update Time         :    2017/8/14 11:54:00
// Class Version       :    v1.0.0.0
// Class Description   :    
// ===============================================================================
using System;
// ===============================================================================
namespace FKVoxelEngineTest.Setting
{
    public class GraphicsSettings : SettingReader
    {
        public int Width
        {
            get { return GetInt("Width", 1280); }
            set { Set("Width", value); }
        }
        public int Height
        {
            get { return GetInt("Height", 720); }
            set { Set("Height", value); }
        }
        public bool FullScreenEnabled
        {
            get { return GetBoolean("FullScreen", true); }
            set { Set("FullScreen", value); }
        }
        public bool VerticalSyncEnabled
        {
            get { return GetBoolean("VSync", true); }
            set { Set("VSync", value); }
        }
        public bool FixedTimeStepsEnabled
        {
            get { return GetBoolean("FixedTimeSteps", true); }
            set { Set("FixedTimeSteps", value); }
        }

        public GraphicsSettings()
            : base("Graphics") { }
    }
}