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
// Create Time         :    2017/8/18 15:08:53
// Update Time         :    2017/8/18 15:08:53
// Class Version       :    v1.0.0.0
// Class Description   :    
// ===============================================================================
using FKVoxelEngine.Base;
using System;
using System.Diagnostics;
using System.IO;
using System.Text;
using System.Threading;
using System.Windows.Forms;
// ===============================================================================
namespace FKVoxelClient.Base
{
    public delegate void CrashHandler();
    public class FKCrashReporter
    {
        private CrashHandler OnCrash;
        private string _strCrashFileName = "";
        private static string s_strDefaultCrashFileName = "Crash.txt";
        private static bool s_bIsShowInConsole = false;
        private static bool s_bIsShowMsgBox = true;

        public static string DefaultCrashFileName
        {
            get { return s_strDefaultCrashFileName; }
            set { s_strDefaultCrashFileName = value; }
        }
        public CrashHandler OnCrashHandler
        {
            set { OnCrash = value; }
        }

        public FKCrashReporter()
            : this(s_strDefaultCrashFileName) { }

        public FKCrashReporter(string filename)
        {
            _strCrashFileName = filename;
        }

        public void Start(ThreadStart start)
        {
            if (!Debugger.IsAttached)
            {
                try
                {
                    start();
                }
                catch(Exception e)
                {
                    Crash(e);
                }
            }
            else
            {
                start();
            }
        }

        public void Crash(Exception exCrash)
        {
            StringBuilder strGuiMessage = new StringBuilder();
            strGuiMessage.Append(DateTime.Now.ToString() + "> Critical Error: " + exCrash.Message);

            try
            {
                if (s_bIsShowInConsole)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.Out.WriteLine("Unhandled exception ocurred.");
                }

                FKSystemFuncs.SafeCreateDir(FKPathFuncs.PathCrash);
                string strCrashFile = Path.Combine(FKPathFuncs.PathCrash, _strCrashFileName);
                using(FileStream fs = File.Open(strCrashFile, FileMode.Append))
                {
                    using(StreamWriter sw = new StreamWriter(fs))
                    {
                        _Log(DateTime.Now.ToString() + ":Critical error occured", sw);
                        _CallOnCrash(sw);
                        Exception exToLog = exCrash;
                        while(exToLog != null)
                        {
                            _Log(exToLog.ToString(), sw);
                            _Log("---------------------", sw);
                            exToLog = exToLog.InnerException;
                        }
                    }
                }

                strGuiMessage.AppendLine("Crash report created: \"" + strCrashFile + "\"");
            }
            catch(Exception ex)
            {
                strGuiMessage.AppendLine("Crash report failed." + ex.ToString());
            }
            finally
            {
                if (s_bIsShowInConsole)
                {
                    Console.WriteLine("Press any key to shutdown...");
                }
                if (s_bIsShowMsgBox)
                {
                    _DisplayInGui(strGuiMessage.ToString());
                }

                // 关闭
                Environment.Exit(1);
            }
        }

        private void _Log(string str, TextWriter tw)
        {
            tw.WriteLine(str);
            Console.WriteLine(str);
        }

        private void _CallOnCrash(TextWriter tw)
        {
            if (OnCrash != null)
            {
                try
                {
                    OnCrash();
                }
                catch (Exception ex)
                {
                    tw.WriteLine("OnCrash() failed: " + ex.ToString());
                }
            }
        }

        private void _DisplayInGui(string strTxt)
        {
            try
            {
                for (int i = 0; i < 5; i++)
                {
                    Cursor.Show();
                    Thread.Sleep(100);
                    Application.DoEvents();
                }

                MessageBox.Show(strTxt, "Critical error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch
            {
                // 这都出错，哥就真没脾气了...
            }
        }

        public static void EnableGlobalExceptionHandler(bool bIsShowInConsole, bool bIsShowInMsgBox)
        {
            s_bIsShowInConsole = bIsShowInConsole;
            s_bIsShowMsgBox = bIsShowInMsgBox;
            AppDomain.CurrentDomain.UnhandledException -= CurrentDomainUnhandledException;
            AppDomain.CurrentDomain.UnhandledException += CurrentDomainUnhandledException;
        }

        private static void CurrentDomainUnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            Exception ex = e.ExceptionObject as Exception;

            FKCrashReporter reporter = new FKCrashReporter();
            reporter.Crash(ex);
        }
    }
}