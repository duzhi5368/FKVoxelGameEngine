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
// Create Time         :    2017/7/26 17:12:14
// Update Time         :    2017/7/26 17:12:14
// Class Version       :    v1.0.0.0
// Class Description   :    
// ===============================================================================
using System;
// ===============================================================================
namespace FKVoxelEngine.Config
{
    public class FKGraphicsConfig
    {
        public int Width { get; set; }
        public int Height { get; set; }
        public bool FullScreenEnabled { get; set; }
        public bool VerticalSyncEnabled { get; set; }
        public bool FixedTimeStepsEnabled { get; set; }


        internal FKGraphicsConfig()
        {
            Width = 1280;
            Height = 720;
            FullScreenEnabled = VerticalSyncEnabled = FixedTimeStepsEnabled = false;
        }

        internal bool Setup()
        {
            return true;
        }
        public override string ToString()
        {
            string ret = string.Empty;

            ret += $"Width = {Width}\n";
            ret += $"Height = {Height}\n";
            ret += $"FullScreenEnabled = {FullScreenEnabled}\n";
            ret += $"VerticalSyncEnabled = {VerticalSyncEnabled}\n";
            ret += $"FixedTimeStepsEnabled = {FixedTimeStepsEnabled}\n";

            return ret;
        }

    }
}