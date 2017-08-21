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
// Create Time         :    2017/7/24 10:28:35
// Update Time         :    2017/7/24 10:28:35
// Class Version       :    v1.0.0.0
// Class Description   :    
// ===============================================================================
using System;
using System.Collections.Generic;
using System.Diagnostics;
// ===============================================================================
namespace FKVoxelEngine.Base
{
    public static class FKLogManager
    {
        public static bool Enabled { get; set; }
        internal readonly static List<FKLogHandler> Handlers = new List<FKLogHandler>();
        internal readonly static Dictionary<string, FKLogger> Loggers = new Dictionary<string, FKLogger>();

        /// <summary>
        /// 创建一个Logger对象
        /// </summary>
        /// <returns></returns>
        public static FKLogger CreateLogger()
        {
            var frame = new StackFrame(1, false);
            var name = frame.GetMethod().DeclaringType.Name;
            if (name == null)
                throw new Exception("Error getting full name for declaring type in logger.");
            if (!Loggers.ContainsKey(name))
                Loggers.Add(name, new FKLogger(name));

            return Loggers[name];
        }

        /// <summary>
        /// 创建一个自定义名字的Logger对象
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public static FKLogger CreateLogger(string name)
        {
            if(name == null)
                throw new Exception("Error empty log name params.");
            if (!Loggers.ContainsKey(name))
                Loggers.Add(name, new FKLogger(name));

            return Loggers[name];
        }

        /// <summary>
        /// 添加一个消息处理方式
        /// </summary>
        /// <param name="handler"></param>
        public static void AttachLogHandler(FKLogHandler handler)
        {
            Handlers.Add(handler);
        }
    }
}