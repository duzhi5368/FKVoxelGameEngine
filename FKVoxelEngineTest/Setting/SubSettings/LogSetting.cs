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
// Create Time         :    2017/8/11 11:32:58
// Update Time         :    2017/8/11 11:32:58
// Class Version       :    v1.0.0.0
// Class Description   :    
// ===============================================================================
using FKVoxelEngine.Base;
using System;
// ===============================================================================
namespace FKVoxelEngineTest.Setting
{
    public class LogSetting : SettingReader
    {
        public LogHandlerConfig[] Handlers = new[]
        {
            new LogHandlerConfig("ConsoleLog"),
            new LogHandlerConfig("EngineLog"),
        };

        internal LogSetting()
            : base("Logging")
        {

        }
    }

    public class LogHandlerConfig : SettingReader
    {
        public LogHandlerConfig(string LoggerName)
            : base(LoggerName)
        {

        }

        public bool Enabled
        {
            get { return GetBoolean("Enabled", true); }
            set { Set("Enabled", value); }
        }
        public string Target
        {
            get { return GetString("Target", "Console"); }
            set { GetString("Target", value); }
        }
        public bool IncludeTimeStamps
        {
            get { return GetBoolean("IncludeTimeStamps", false); }
            set { Set("IncludeTimeStamps", value); }
        }
        public string FileName
        {
            get { return GetString("FileName", ""); }
            set { GetString("FileName", value); }
        }
        public FKLogger.ENUM_Level MinimumLevel
        {
            get { return (FKLogger.ENUM_Level)(GetInt("MinimumLevel", (int)FKLogger.ENUM_Level.eLevel_Info, true)); }
            set { Set("MinimumLevel", (int)value); }
        }
        public FKLogger.ENUM_Level MaximumLevel
        {
            get { return (FKLogger.ENUM_Level)(GetInt("MaximumLevel", (int)FKLogger.ENUM_Level.eLevel_Fatal, true)); }
            set { Set("MaximumLevel", (int)value); }
        }
        public bool ResetOnStartup
        {
            get { return GetBoolean("ResetOnStartup", false); }
            set { Set("ResetOnStartup", value); }
        }
    }
}