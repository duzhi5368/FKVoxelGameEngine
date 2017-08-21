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
// Create Time         :    2017/7/26 13:11:41
// Update Time         :    2017/7/26 13:11:41
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
    public class FKCommand
    {
        private static readonly FKLogger Logger = FKLogManager.CreateLogger("FKCommand");

        private readonly Dictionary<FKSubCommandAttribute, MethodInfo> _Commands = new Dictionary<FKSubCommandAttribute, MethodInfo>();
        public FKCommandAttribute Attributes { get; private set; }

        /// <summary>
        /// 注册命令集
        /// </summary>
        /// <param name="attributes"></param>
        public void RegisterCommand(FKCommandAttribute attributes)
        {
            Attributes = attributes;
            RegisterDefaultCommands();
            RegisterCommands();
        }

        /// <summary>
        /// 执行一个命令
        /// </summary>
        /// <param name="parameters"></param>
        /// <returns></returns>
        public virtual string Handle(string parameters)
        {
            string[] @params = null;
            FKSubCommandAttribute target = null;

            if (parameters == string.Empty)
                target = this.GetDefaultSubcommand();
            else
            {
                @params = parameters.Split(' ');
                target = this.GetSubcommand(@params[0]) ?? this.GetDefaultSubcommand();

                if (target != this.GetDefaultSubcommand())
                    @params = @params.Skip(1).ToArray();
            }


            return (string)this._Commands[target].Invoke(this, new object[] { @params });
        }
        /// <summary>
        /// 获取一个命令的帮助信息
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        public string GetHelp(string command)
        {
            foreach (var pair in this._Commands)
            {
                if (command != pair.Key.Name) continue;
                return pair.Key.Help;
            }

            return string.Empty;
        }

        protected FKSubCommandAttribute GetDefaultSubcommand()
        {
            return this._Commands.Keys.First();
        }

        protected FKSubCommandAttribute GetSubcommand(string name)
        {
            return this._Commands.Keys.FirstOrDefault(command => command.Name == name);
        }

        private void RegisterCommands()
        {
            foreach (var method in this.GetType().GetMethods())
            {
                object[] attributes = method.GetCustomAttributes(typeof(FKSubCommandAttribute), true);
                if (attributes.Length == 0)
                    continue;

                var attribute = (FKSubCommandAttribute)attributes[0];
                if (attribute is FKDefaultCommand)
                    continue;

                if (!this._Commands.ContainsKey(attribute))
                    this._Commands.Add(attribute, method);
                else
                    Logger.Warn("There exists an already registered command '{0}'.", attribute.Name);
            }
        }

        private void RegisterDefaultCommands()
        {
            foreach (var method in this.GetType().GetMethods())
            {
                object[] attributes = method.GetCustomAttributes(typeof(FKDefaultCommand), true);
                if (attributes.Length == 0) continue;
                if (method.Name.ToLower() == "fallback") continue;

                this._Commands.Add(new FKDefaultCommand(), method);
                return;
            }

            // 如果找不到DefaultCommand，我们就注册 Fallback 命令
            this._Commands.Add(new FKDefaultCommand(), this.GetType().GetMethod("Fallback"));
        }

        /// <summary>
        /// 默认Fallback命令
        /// </summary>
        /// <param name="params"></param>
        /// <returns></returns>
        [FKDefaultCommand]
        public virtual string Fallback(string[] @params = null)
        {
            var output = "Available subcommands: ";
            foreach (var pair in this._Commands)
            {
                if (pair.Key.Name.Trim() == string.Empty)
                    continue;

                output += pair.Key.Name + ", ";
            }

            return output.Substring(0, output.Length - 2) + ".";
        }
    }
}