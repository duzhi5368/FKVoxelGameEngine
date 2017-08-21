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
// Create Time         :    2017/7/26 19:00:12
// Update Time         :    2017/7/26 19:00:12
// Class Version       :    v1.0.0.0
// Class Description   :    
// ===============================================================================
using FKVoxelEngine.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
// ===============================================================================
namespace FKVoxelEngine.Debug
{
    public static partial class FKCommandManager
    {
        private static readonly FKLogger Logger = FKLogManager.CreateLogger("FKCommandManager");
        private static readonly Dictionary<FKCommandAttribute, FKCommand> CommandGroups = new Dictionary<FKCommandAttribute, FKCommand>();

        static FKCommandManager()
        {
            RegisterCommandGroups();
        }

        private static void RegisterCommandGroups()
        {
            foreach (var type in Assembly.GetExecutingAssembly().GetTypes())
            {
                if (!type.IsSubclassOf(typeof(FKCommand)))
                    continue;

                var attributes = (FKCommandAttribute[])type.GetCustomAttributes(typeof(FKCommandAttribute), true);
                if (attributes.Length == 0)
                    continue;

                var groupAttribute = attributes[0];
                if (CommandGroups.ContainsKey(groupAttribute))
                    Logger.Warn("There exists an already registered command group named '{0}'.", groupAttribute.Name);

                var commandGroup = (FKCommand)Activator.CreateInstance(type);
                commandGroup.RegisterCommand(groupAttribute);
                CommandGroups.Add(groupAttribute, commandGroup);
            }
        }

        public static string Parse(string line)
        {
            string output = string.Empty;
            string command;
            string parameters;
            var found = false;

            if (line == null)
                return output;

            if (line.Trim() == string.Empty)
                return output;

            if (!ExtractCommandAndParameters(line, out command, out parameters))
            {
                output = "Unknown command: " + line;
                Logger.Info(output);
                return output;
            }

            foreach (var pair in CommandGroups)
            {
                if (pair.Key.Name != command) continue;
                output = pair.Value.Handle(parameters);
                found = true;
                break;
            }

            if (found == false)
                output = "ERROR: command not found.";

            if (output != string.Empty)
                Logger.Info(output);

            return output;
        }
        /// <summary>
        /// 进行自动 命令补齐提示，智能提示
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        public static FKCommand GetMatchingCommand(string command)
        {
            var matchingCommands = CommandGroups.Values.Where(c => c.Attributes.Name.StartsWith(command));
            return matchingCommands.FirstOrDefault();
        }

        public static bool ExtractCommandAndParameters(string line, out string command, out string parameters)
        {
            line = line.Trim();
            command = string.Empty;
            parameters = string.Empty;

            if (line == string.Empty)
                return false;

            command = line.Split(' ')[0].ToLower(); // get command
            parameters = String.Empty;
            if (line.Contains(' '))
            {
                parameters = line.Substring(line.IndexOf(' ') + 1).Trim(); // get parameters if any.
            }

            return true;
        }
    }
}