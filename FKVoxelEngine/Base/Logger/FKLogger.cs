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
// Create Time         :    2017/7/24 10:11:40
// Update Time         :    2017/7/24 10:11:40
// Class Version       :    v1.0.0.0
// Class Description   :    
// ===============================================================================
using System;
using System.Globalization;
// ===============================================================================
namespace FKVoxelEngine.Base
{
    public class FKLogger
    {
        public enum ENUM_Level
        {
            eLevel_Trace,
            eLevel_Debug,
            eLevel_Info,
            eLevel_Warn,
            eLevel_Error,
            eLevel_Fatal,
            eLevel_Dump,
        }
        public string Name { get; protected set; }

        public FKLogger(string name)
        {
            Name = name;
        }

        #region ==== 对外接口 ====

        public void Trace(string message)
        {
            Log(ENUM_Level.eLevel_Trace, message, null);
        }
        public void Trace(string message, params object[] args)
        {
            Log(ENUM_Level.eLevel_Trace, message, args);
        }
        public void Debug(string message)
        {
            Log(ENUM_Level.eLevel_Debug, message, null);
        }
        public void Debug(string message, params object[] args)
        {
            Log(ENUM_Level.eLevel_Debug, message, args);
        }
        public void Info(string message)
        {
            Log(ENUM_Level.eLevel_Info, message, null);
        }
        public void Info(string message, params object[] args)
        {
            Log(ENUM_Level.eLevel_Info, message, args);
        }
        public void Warn(string message)
        {
            Log(ENUM_Level.eLevel_Warn, message, null);
        }
        public void Warn(string message, params object[] args)
        {
            Log(ENUM_Level.eLevel_Warn, message, args);
        }
        public void Error(string message)
        {
            Log(ENUM_Level.eLevel_Error, message, null);
        }
        public void Error(string message, params object[] args)
        {
            Log(ENUM_Level.eLevel_Error, message, args);
        }
        public void Fatal(string message)
        {
            Log(ENUM_Level.eLevel_Fatal, message, null);
        }
        public void Fatal(string message, params object[] args)
        {
            Log(ENUM_Level.eLevel_Fatal, message, args);
        }

        public void TraceException(Exception e, string message)
        {
            LogException(ENUM_Level.eLevel_Trace, message, null, e);
        }
        public void TraceException(Exception e, string message, params object[] args)
        {
            LogException(ENUM_Level.eLevel_Trace, message, args, e);
        }
        public void DebugException(Exception e, string message)
        {
            LogException(ENUM_Level.eLevel_Debug, message, null, e);
        }
        public void DebugException(Exception e, string message, params object[] args)
        {
            LogException(ENUM_Level.eLevel_Debug, message, args, e);
        }
        public void WarnException(Exception e, string message)
        {
            LogException(ENUM_Level.eLevel_Warn, message, null, e);
        }
        public void WarnException(Exception e, string message, params object[] args)
        {
            LogException(ENUM_Level.eLevel_Warn, message, args, e);
        }
        public void ErrorException(Exception e, string message)
        {
            LogException(ENUM_Level.eLevel_Error, message, null, e);
        }
        public void ErrorException(Exception e, string message, params object[] args)
        {
            LogException(ENUM_Level.eLevel_Error, message, args, e);
        }
        public void FatalException(Exception e, string message)
        {
            LogException(ENUM_Level.eLevel_Fatal, message, null, e);
        }
        public void FatalException(Exception e, string message, params object[] args)
        {
            LogException(ENUM_Level.eLevel_Fatal, message, args, e);
        }

        #endregion ==== 对外接口 =====

        #region ==== 核心函数 ====

        private void Log(ENUM_Level level, string message, object[] args)
        {
            FKLogRouter.RouteMessage(level, this.Name, args == null ? message : string.Format(CultureInfo.InvariantCulture, message, args));
        }

        private void LogException(ENUM_Level level, string message, object[] args, Exception e)
        {
            FKLogRouter.RouteException(level, this.Name, args == null ? message : string.Format(CultureInfo.InvariantCulture, message, args), e);
        }

        #endregion ==== 核心函数 ====
    }
}