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
// Create Time         :    2017/7/26 18:17:00
// Update Time         :    2017/7/26 18:17:00
// Class Version       :    v1.0.0.0
// Class Description   :    游戏控制台的输入相应处理
// ===============================================================================
using FKVoxelEngine.Framework;
using FKVoxelEngine.Input;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
// ===============================================================================
namespace FKVoxelEngine.Debug
{
    internal class FKGameConsoleInputProcessor
    {
        public event EventHandler Open = delegate { };
        public event EventHandler Close = delegate { };
        public event EventHandler PlayerCommand = delegate { };
        public event EventHandler ConsoleCommand = delegate { };

        public FKCommandHistory                 CommandHistory { get; set; }
        public FKGameConsoleOutputLine          Buffer { get; set; }
        public List<FKGameConsoleOutputLine>    Out { get; set; }

        private bool _bIsActive;
        private FKCommandProcessor _CommandProcessor;

        public FKGameConsoleInputProcessor(FKCommandProcessor commandProcessor)
        {
            _CommandProcessor = commandProcessor;
            _bIsActive = false;
            CommandHistory = new FKCommandHistory();
            Out = new List<FKGameConsoleOutputLine>();
            Buffer = new FKGameConsoleOutputLine("", ENUM_OutputLineType.eCommand);

            var inputManager = (IFKInputManagerService)(FKEngine.GetInstance.Game.Services.GetService(typeof(IFKInputManagerService)));
            inputManager.KeyDown += new FKInputManager.KeyEventHandler(OnKeyDown);
        }
        /// <summary>
        /// 对外接口:添加一行输出
        /// </summary>
        /// <param name="text"></param>
        public void AddToOutput(string text)
        {
            if(FKGameConsoleOptions.GetInstance.OpenOnWrite)
            {
                _bIsActive = true;
                Open(this, FKKeyEventArgs.Empty);
            }

            foreach(var line in text.Split('\n'))
            {
                Out.Add(new FKGameConsoleOutputLine(line, ENUM_OutputLineType.eOutput));
            }
        }


        /// <summary>
        /// 按键响应
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private bool OnKeyDown(object sender, FKKeyEventArgs e)
        {
            if(e.KeyCode == FKGameConsoleOptions.GetInstance.ToggleKey)
            {
                ToggleConsole();
                return true;
            }

            switch (e.KeyCode)  // 不截获处理的按键
            {
                case Keys.F1:
                case Keys.F2:
                case Keys.F3:
                case Keys.F4:
                case Keys.F5:
                case Keys.F6:
                case Keys.F7:
                case Keys.F8:
                case Keys.F9:
                case Keys.F10:
                case Keys.F11:
                case Keys.F12:
                    return false;
            }

            switch (e.KeyCode)
            {
                case Keys.Enter:
                    Buffer.Output = ExecuteBuffer();
                    break;
                case Keys.Back:
                    Buffer.Output = DeleteChar();
                    break;
                case Keys.Tab:
                    Buffer.Output = AutoComplete();
                    break;
                case Keys.Up:
                    Buffer.Output = CommandHistory.Prev();
                    break;
                case Keys.Down:
                    Buffer.Output = CommandHistory.Next();
                    break;
                default:
                    Buffer.Output += TranslateChar(e.KeyCode);
                    break;
            }
            return true;
        }
        /// <summary>
        /// 开启/关闭Console
        /// </summary>
        private void ToggleConsole()
        {
            _bIsActive = !_bIsActive;
            if (_bIsActive)
            {
                Open(this, FKKeyEventArgs.Empty);
            }
            else
            {
                Close(this, FKKeyEventArgs.Empty);
            }
        }
        /// <summary>
        /// 倒退一个字符
        /// </summary>
        /// <returns></returns>
        private string DeleteChar()
        {            
            if (Buffer.Output.Length > 0)
               return Buffer.Output.Substring(0, Buffer.Output.Length - 1);
            return "";
        }
        /// <summary>
        /// 执行一段命令
        /// </summary>
        /// <returns></returns>
        private string ExecuteBuffer()
        {
            if (Buffer.Output.Length <= 0)
                return "";
            // 进行处理
            var output = _CommandProcessor.Process(Buffer.Output).Split('\n').Where(l => l != "");
            // 输出命令
            Out.Add(new FKGameConsoleOutputLine(Buffer.Output, ENUM_OutputLineType.eCommand));
            // 输出处理结果
            foreach(var line in output)
            {
                Out.Add(new FKGameConsoleOutputLine(line, ENUM_OutputLineType.eOutput));
            }
            // 添加记录中
            CommandHistory.Add(Buffer.Output);
            return "";
        }
        /// <summary>
        /// 自动补齐本行命令
        /// </summary>
        /// <returns></returns>
        private string AutoComplete()
        {
            var lastSpacePosition = Buffer.Output.LastIndexOf(' ');
            var textToMatch = lastSpacePosition < 0 ? Buffer.Output : Buffer.Output.Substring(lastSpacePosition + 1, Buffer.Output.Length - lastSpacePosition - 1);
            var match = FKCommandManager.GetMatchingCommand(textToMatch);
            if (null == match)
                return Buffer.Output;   // 无法检测到 智能提示 对象
            var restOfTheCommand = match.Attributes.Name.Substring(textToMatch.Length);
            return Buffer.Output + restOfTheCommand + " ";
        }
        /// <summary>
        /// 按键转义字符
        /// </summary>
        /// <returns></returns>
        private string TranslateChar(Keys key)
        {
            char letter = ' ';
            if (key == Keys.OemPeriod)
                letter =  '.';
            if(key >= Keys.A && key <= Keys.Z)
            {
                if(Keyboard.GetState().IsKeyDown(Keys.LeftShift) || Keyboard.GetState().IsKeyDown(Keys.RightShift))
                {
                    letter = ((char)('A' + ((int)key - (int)Keys.A)));
                }
                else
                {
                    letter =((char)('a' + ((int)key - (int)Keys.A)));
                }
            }
            if (key >= Keys.NumPad0 && key <= Keys.NumPad9)
            {
                letter = ((char)('0' + ((int)key - (int)Keys.NumPad0)));
            }
            if (key >= Keys.D0 && key <= Keys.D9)
            {
                letter = ((char)('0' + ((int)key - (int)Keys.D0)));
            }

            if (IsPrintable(letter))
            {
                return letter.ToString();
            }
            return "";
        }
        /// <summary>
        /// 判断一个字符是否被当前字体支持
        /// </summary>
        /// <param name="letter"></param>
        /// <returns></returns>
        private bool IsPrintable(char letter)
        {
            return FKGameConsoleOptions.GetInstance.Font.Characters.Contains(letter);
        }
    }
}