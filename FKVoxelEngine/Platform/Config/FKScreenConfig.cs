﻿/* 
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
// Create Time         :    2017/7/21 13:36:03
// Update Time         :    2017/7/21 13:36:03
// Class Version       :    v1.0.0.0
// Class Description   :    
// ===============================================================================
using Microsoft.Xna.Framework;
// ===============================================================================
namespace FKVoxelEngine.Platform
{
    public class FKScreenConfig
    {
        /// <summary>
        /// 屏幕宽度
        /// </summary>
        public int Width { get; set; }
        /// <summary>
        /// 屏幕高度
        /// </summary>
        public int Height { get; set; }
        /// <summary>
        /// 是否使用全屏
        /// </summary>
        public bool IsFullScreen { get; set; }
        /// <summary>
        /// 本平台支持的屏幕朝向方式
        /// </summary>
        public DisplayOrientation SupportedOrientations { get; set; }

        public FKScreenConfig()
        {
            Width = Height = 0;
            IsFullScreen = false;
            SupportedOrientations = DisplayOrientation.Default;
        }
    }
}