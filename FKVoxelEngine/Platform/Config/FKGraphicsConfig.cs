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
// Create Time         :    2017/7/21 13:45:42
// Update Time         :    2017/7/21 13:45:42
// Class Version       :    v1.0.0.0
// Class Description   :    
// ===============================================================================

// ===============================================================================
namespace FKVoxelEngine.Platform
{
    public class FKGraphicsConfig
    {
        /// <summary>
        /// 是否强制渲染时稳帧
        /// </summary>
        public bool IsFixedTimeStep { get; set; }
        /// <summary>
        /// 是否开启垂直同步
        /// </summary>
        public bool IsVsyncEnabled { get; set; }
        /// <summary>
        /// 是否开启了后处理渲染
        /// </summary>
        public bool IsPostprocessEnabled { get; set; }
        /// <summary>
        /// 是否允许启动自定义shader
        /// </summary>
        public bool IsExendedEffectsEnabled { get; set; }

        public FKGraphicsConfig()
        {
            IsFixedTimeStep = IsVsyncEnabled = IsPostprocessEnabled = IsExendedEffectsEnabled = false;
        }
    }
}