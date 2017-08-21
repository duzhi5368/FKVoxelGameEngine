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
// Create Time         :    2017/7/26 18:59:24
// Update Time         :    2017/7/26 18:59:24
// Class Version       :    v1.0.0.0
// Class Description   :    
// ===============================================================================
using System;
using System.Linq;
// ===============================================================================
namespace FKVoxelEngine.Debug
{
    class FKCommandProcessor
    {
        /// <summary>
        /// 执行处理一段命令
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        public string Process(string command)
        {
            var output = FKCommandManager.Parse(command);
            return output;
        }

        static string GetCommandName(string command)
        {
            var firstSpace = command.IndexOf(' ');
            return command.Substring(0, firstSpace < 0 ? command.Length : firstSpace);
        }

        static string[] GetArguments(string command)
        {
            var firstSpace = command.IndexOf(' ');
            if (firstSpace < 0)
            {
                return new string[0];
            }

            var args = command.Substring(firstSpace, command.Length - firstSpace).Split(' ');
            return args.Where(a => a != "").ToArray();
        }
    }
}