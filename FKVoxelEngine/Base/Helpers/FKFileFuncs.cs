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
// Create Time         :    2017/7/21 19:04:11
// Update Time         :    2017/7/21 19:04:11
// Class Version       :    v1.0.0.0
// Class Description   :    
// ===============================================================================
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
// ===============================================================================
namespace FKVoxelEngine.Base
{
    public static class FKFileFuncs
    {
        public static string AssemblyRoot
        {
            get { return Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location); }
        }

        public static List<string> GetFilesByExtensionRecursive(string directory, string fileExtension)
        {
            var files = new List<string>();
            var stack = new Stack<string>();

            stack.Push(directory);

            while (stack.Count > 0)
            {
                var topDir = stack.Pop();
                var dirInfo = new DirectoryInfo(topDir);
                if (!Directory.Exists(topDir))　             　　                
                    Directory.CreateDirectory(topDir);　  

                files.AddRange((from fileInfo in dirInfo.GetFiles()
                                where string.Compare(fileInfo.Extension, fileExtension, System.StringComparison.OrdinalIgnoreCase) == 0
                                select topDir + "/" + fileInfo.Name).ToList());

                foreach (var dir in Directory.GetDirectories(topDir))
                {
                    stack.Push(dir);
                }
            }

            return files.Select(file => file.Replace("\\", "/")).ToList();
        }
    }
}