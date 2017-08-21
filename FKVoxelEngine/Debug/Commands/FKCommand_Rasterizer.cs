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
// Create Time         :    2017/8/8 11:34:43
// Update Time         :    2017/8/8 11:34:43
// Class Version       :    v1.0.0.0
// Class Description   :    
// ===============================================================================
using FKVoxelEngine.Framework;
using System;
// ===============================================================================
namespace FKVoxelEngine.Debug
{
    [FKCommand("rasterizer", "Sets rasterizer mode.\nusage: rasterizer [wireframed|normal]")]
    public class FKCommand_Rasterizer : FKCommand
    {
        [FKDefaultCommand]
        public string Default(string[] @params)
        {
            return string.Format("Rasterizer is currently set to {0} mode.\nusage: rasterizer [wireframed|normal].",
                     FKEngine.GetInstance.Rasterizer.Wireframed
                         ? "wireframed"
                         : "normal");
        }

        [FKSubCommand("wireframed", "Sets rasterizer mode to wireframed.")]
        public string Wireframed(string[] @params)
        {
            FKEngine.GetInstance.Rasterizer.Wireframed = true;
            return "Rasterizer mode set to wireframed.";
        }

        [FKSubCommand("normal", "Sets rasterizer mode to normal.")]
        public string Normal(string[] @params)
        {
            FKEngine.GetInstance.Rasterizer.Wireframed = false;
            return "Rasterizer mode set to normal mode.";
        }
    }
}