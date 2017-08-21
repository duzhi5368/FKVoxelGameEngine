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
// Create Time         :    2017/7/21 13:35:23
// Update Time         :    2017/7/21 13:35:23
// Class Version       :    v1.0.0.0
// Class Description   :    
// ===============================================================================
using System;
// ===============================================================================
namespace FKVoxelEngine.Platform
{
    public class FKPlatformConfig
    {
        /// <summary>
        /// 显示配置
        /// </summary>
        public FKScreenConfig ScreenConfig { get; private set; }
        /// <summary>
        /// 渲染配置
        /// </summary>
        public FKGraphicsConfig GraphicsConfig { get; private set; }
        /// <summary>
        /// 输入配置
        /// </summary>
        public FKInputConfig InputConfig { get; private set; }
        /// <summary>
        /// Debug配置
        /// </summary>
        public FKDebugConfig DebugConfig { get; private set; }

        public FKPlatformConfig()
        {
            ScreenConfig    = new FKScreenConfig();
            GraphicsConfig  = new FKGraphicsConfig();
            InputConfig     = new FKInputConfig();
            DebugConfig     = new FKDebugConfig();
        }
    }
}