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
// Create Time         :    2017/7/24 11:38:04
// Update Time         :    2017/7/24 11:38:04
// Class Version       :    v1.0.0.0
// Class Description   :    
// ===============================================================================
using System;
// ===============================================================================
namespace FKVoxelEngine.Base
{
    public class FKConsoleHandler : FKLogHandler
    {
        public FKConsoleHandler(FKLogger.ENUM_Level minLevel, FKLogger.ENUM_Level maxLevel, bool bIsIncludeTimestamps)
        {
            MinimumLevel = minLevel;
            MaximumLevel = maxLevel;
            IncludeTimeStamps = bIsIncludeTimestamps;
        }

        public override void LogMessage(FKLogger.ENUM_Level level, string logger, string message)
        {
            var timestamp = this.IncludeTimeStamps ? "[" + DateTime.Now.ToString("dd.MM.yyyy HH:mm:ss.fff") + "] " : "";
            SetConsoleForegroundColor(level);
            Console.WriteLine(string.Format("{0}[{1}] [{2}]: {3}", timestamp, level.ToString().PadLeft(5), logger, message));
        }

        public override void LogException(FKLogger.ENUM_Level level, string logger, string message, Exception e)
        {
            var timestamp = this.IncludeTimeStamps ? "[" + DateTime.Now.ToString("dd.MM.yyyy HH:mm:ss.fff") + "] " : "";
            SetConsoleForegroundColor(level);
            Console.WriteLine(string.Format("{0}[{1}] [{2}]: {3} - [Exception] {4}", timestamp, level.ToString().PadLeft(5), logger, message, e.ToString()));
        }

        private static void SetConsoleForegroundColor(FKLogger.ENUM_Level level)
        {
            switch (level)
            {
                case FKLogger.ENUM_Level.eLevel_Trace:
                    Console.ForegroundColor = ConsoleColor.DarkGray;
                    break;
                case FKLogger.ENUM_Level.eLevel_Debug:
                    Console.ForegroundColor = ConsoleColor.Cyan;
                    break;
                case FKLogger.ENUM_Level.eLevel_Info:
                    Console.ForegroundColor = ConsoleColor.White;
                    break;
                case FKLogger.ENUM_Level.eLevel_Warn:
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    break;
                case FKLogger.ENUM_Level.eLevel_Error:
                    Console.ForegroundColor = ConsoleColor.Magenta;
                    break;
                case FKLogger.ENUM_Level.eLevel_Fatal:
                    Console.ForegroundColor = ConsoleColor.Red;
                    break;
                case FKLogger.ENUM_Level.eLevel_Dump:
                    Console.BackgroundColor = ConsoleColor.Yellow;
                    Console.ForegroundColor = ConsoleColor.Red;
                    break;
                default:
                    break;
            }
        }
    }
}