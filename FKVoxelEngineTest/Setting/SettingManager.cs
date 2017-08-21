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
// Create Time         :    2017/8/11 10:47:04
// Update Time         :    2017/8/11 10:47:04
// Class Version       :    v1.0.0.0
// Class Description   :    
// ===============================================================================
using FKVoxelEngine.Base;
using Nini.Config;
using System;
using System.IO;
// ===============================================================================
namespace FKVoxelEngineTest.Setting
{
    public class SettingManager
    {
        private static readonly FKLogger Logger = FKLogManager.CreateLogger("Setting");

        private static readonly IniConfigSource Parser;
        private static readonly string ConfigFile;
        private static bool _FileExists = false;

        const string INI_CONFIG_FILE = "Content/Config.ini";
        static SettingManager()
        {
            ConfigFile = string.Format("{0}/{1}", FKFileFuncs.AssemblyRoot, INI_CONFIG_FILE);
            if (!File.Exists(ConfigFile))
            {
                Parser = new IniConfigSource();
                _FileExists = false;
                Logger.Warn($"Loading setting file = {INI_CONFIG_FILE} failed, will be using default settings.");

                Parser.Alias.AddAlias("On", true);
                Parser.Alias.AddAlias("Off", false);

                Parser.Alias.AddAlias("MinimumLevel", FKLogger.ENUM_Level.eLevel_Trace);
                Parser.Alias.AddAlias("MaximumLevel", FKLogger.ENUM_Level.eLevel_Trace);
            }
            else
            {
                try
                {
                    Parser = new IniConfigSource(ConfigFile);
                    _FileExists = true;
                }
                catch (Exception e)
                {
                    Parser = new IniConfigSource();
                    _FileExists = false;
                    Logger.WarnException(e, $"Loading setting file = {INI_CONFIG_FILE} failed, will be using default settings.");
                }
                finally
                {
                    Parser.Alias.AddAlias("On", true);
                    Parser.Alias.AddAlias("Off", false);

                    Parser.Alias.AddAlias("MinimumLevel", FKLogger.ENUM_Level.eLevel_Trace);
                    Parser.Alias.AddAlias("MaximumLevel", FKLogger.ENUM_Level.eLevel_Trace);
                }
            }

            Parser.ExpandKeyValues();
        }

        static internal IConfig Section(string section)
        {
            return Parser.Configs[section];
        }

        static internal IConfig AddSection(string section)
        {
            return Parser.AddConfig(section);
        }

        static internal void Save()
        {
            if (_FileExists)
                Parser.Save();
            else
            {
                Parser.Save(ConfigFile);
                _FileExists = true;
            }
        }
    }
}