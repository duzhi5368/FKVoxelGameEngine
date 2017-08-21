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
// Create Time         :    2017/7/24 10:24:40
// Update Time         :    2017/7/24 10:24:40
// Class Version       :    v1.0.0.0
// Class Description   :    
// ===============================================================================
using System;
using System.Linq;
// ===============================================================================
namespace FKVoxelEngine.Base
{
    internal static class FKLogRouter
    {
        /// <summary>
        /// Log分派处理
        /// </summary>
        /// <param name="level"></param>
        /// <param name="logger">Logger名称</param>
        /// <param name="message">输出信息</param>
        public static void RouteMessage(FKLogger.ENUM_Level level, string logger, string message)
        {
            if (!FKLogManager.Enabled)
                return;
            if (FKLogManager.Handlers.Count <= 0)
                return;

            foreach (var Handler in FKLogManager.Handlers.Where(Handler => 
                    level >= Handler.MinimumLevel && level <= Handler.MaximumLevel))
            {
                Handler.LogMessage(level, logger, message);
            }
        }

        /// <summary>
        /// Exception类型Log分派处理
        /// </summary>
        /// <param name="level"></param>
        /// <param name="logger"></param>
        /// <param name="message"></param>
        /// <param name="e"></param>
        public static void RouteException(FKLogger.ENUM_Level level, string logger, string message, Exception e)
        {
            if (!FKLogManager.Enabled)
                return;
            if (FKLogManager.Handlers.Count <= 0)
                return;

            foreach (var Handler in FKLogManager.Handlers.Where(Handler =>
                    level >= Handler.MinimumLevel && level <= Handler.MaximumLevel))
            {
                Handler.LogException(level, logger, message, e);
            }
        }
    }
}