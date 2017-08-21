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
// Create Time         :    2017/7/24 11:01:41
// Update Time         :    2017/7/24 11:01:41
// Class Version       :    v1.0.0.0
// Class Description   :    
// ===============================================================================
using System;
using System.IO;
// ===============================================================================
namespace FKVoxelEngine.Base
{
    public class FKFileHandler : FKLogHandler, IDisposable
    {
        #region ==== 成员变量 ====

        private const string LogRootPath = "FKLogs";
        private readonly string _FileName;
        private readonly string _FileFullPath;
        private FileStream _FileStream;
        private StreamWriter _StreamWriter;
        private bool _bIsDisposed;

        #endregion ==== 成员变量 ====

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="fileName">文件名（无需路径）</param>
        /// <param name="minLevel">负责的日志最小级别</param>
        /// <param name="maxLevel">负责的日志最大级别</param>
        /// <param name="bIsIncludeTimeStamps">是否携带时间戳显示</param>
        /// <param name="bReset">是否清空源文件，是则清空，否则添加</param>
        public FKFileHandler(string fileName, FKLogger.ENUM_Level minLevel, FKLogger.ENUM_Level maxLevel, bool bIsIncludeTimeStamps, bool bReset = false)
        {
            MinimumLevel = minLevel;
            MaximumLevel = maxLevel;
            IncludeTimeStamps = bIsIncludeTimeStamps;
            _FileName = fileName;
            _FileFullPath = string.Format("{0}/{1}", LogRootPath, _FileName);
            _bIsDisposed = false;

            if (!Directory.Exists(LogRootPath))
                Directory.CreateDirectory(LogRootPath);

            _FileStream = new FileStream(_FileFullPath, bReset ? FileMode.Create : FileMode.Append, FileAccess.Write, FileShare.Read);
            _StreamWriter = new StreamWriter(_FileStream) { AutoFlush = true };
        }
        ~FKFileHandler()
        {
            Dispose(false);
        }
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        private void Dispose(bool bIsDisposing)
        {
            if (_bIsDisposed)   // 已释放
                return;

            if (bIsDisposing)
            {
                _StreamWriter.Close();
                _StreamWriter.Dispose();
                _FileStream.Close();
                _FileStream.Dispose();
            }

            this._FileStream = null;
            this._StreamWriter = null;
            _bIsDisposed = true;
        }

        #region ==== 对外接口 ====

        public override void LogMessage(FKLogger.ENUM_Level level, string logger, string message)
        {
            lock (this)
            {
                var timestamp = this.IncludeTimeStamps ? "[" + DateTime.Now.ToString("dd.MM.yyyy HH:mm:ss.fff") + "] " : "";

                if (!this._bIsDisposed)
                {
                    _StreamWriter.WriteLine(string.Format("{0}[{1}] [{2}]: {3}", timestamp, level.ToString().PadLeft(5), logger, message));
                }
            }
        }

        public override void LogException(FKLogger.ENUM_Level level, string logger, string message, Exception e)
        {
            lock (this)
            {
                var timestamp = this.IncludeTimeStamps ? "[" + DateTime.Now.ToString("dd.MM.yyyy HH:mm:ss.fff") + "] " : "";

                if (!this._bIsDisposed)
                {
                    _StreamWriter.WriteLine(string.Format("{0}[{1}] [{2}]: {3} - [Exception] {4}", timestamp, level.ToString().PadLeft(5), logger, message, e.ToString()));
                }
            }
        }

        #endregion ==== 对外接口 ====
    }
}