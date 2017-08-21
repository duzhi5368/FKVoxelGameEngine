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
// Create Time         :    2017/7/21 15:04:11
// Update Time         :    2017/7/21 15:04:11
// Class Version       :    v1.0.0.0
// Class Description   :    
// ===============================================================================
using System;
using System.Globalization;
using System.IO;
// ===============================================================================
namespace FKVoxelEngine.Base
{
    public class FKSystemFuncs
    {
        /// <summary>
        /// 检查当前代码是否运行在MonoFramework上
        /// </summary>
        /// <returns></returns>
        public static bool IsRunningOnMono()
        {
            return Type.GetType("Mono.Runtime") != null;
        }

        public static string GetStorePath()
        {
            string appPath = Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location);
            try
            {
                var di = new DirectoryInfo(appPath);
                if (di.Name.Equals("AutoUpdaterTemp", StringComparison.InvariantCultureIgnoreCase))
                {
                    appPath = di.Parent.FullName;
                }
            }
            catch
            {
            }
            string mdfolder = "UserData";
            if (appPath.Contains(Environment.GetFolderPath(Environment.SpecialFolder.ProgramFiles)) && !IsRunningOnMono())
            {
                string mdpath = Path.Combine(
                                    Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments),
                                    mdfolder);
                return mdpath;
            }
            else
            {
                return Path.Combine(appPath, mdfolder);
            }
        }

        public static void SafeCreateDir(string dir)
        {
            if (!Directory.Exists(dir))
                Directory.CreateDirectory(dir);
        }

        public static string GetLanguageIso6391()
        {
            return CultureInfo.CurrentCulture.TwoLetterISOLanguageName;
        }
    }
}