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
// Create Time         :    2017/7/26 18:06:28
// Update Time         :    2017/7/26 18:06:28
// Class Version       :    v1.0.0.0
// Class Description   :    
// ===============================================================================
using FKVoxelEngine.Base;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
// ===============================================================================
namespace FKVoxelEngine.Debug
{
    public class FKGameConsoleOptions : FKSingleton<FKGameConsoleOptions>
    {
        public Keys ToggleKey { get; set; }                     // 开启/关闭 键：默认波浪键
        public Color BackgroundColor { get; set; }              // 控制台背景色
        public Color BoundsColor { get; set; }                  // 控制台包围框色
        public Color BufferColor { get; set; }                  // 当前正在输入项 颜色
        public Color PastCommandColor { get; set; }             // 历史命令颜色
        public Color PastCommandOutputColor { get; set; }       // 之前的命令结果输出 颜色
        public Color PromptColor { get; set; }                  // 命令输入提示符颜色
        public Color CursorColor { get; set; }                  // 光标颜色
        public float AnimationSpeed { get; set; }               // 开启关闭时的卷入卷出动画播放速度
        public float CursorBlinkSpeed { get; set; }             // 当前光标闪烁速度
        public int Height { get; set; }                         // 控制台高度
        public string Prompt { get; set; }
        public char Cursor { get; set; }
        public int Padding { get; set; }
        public int Margin { get; set; }
        public bool OpenOnWrite { get; set; }
        public SpriteFont Font { get; set; }
        public Color FontColor
        {
            set
            {
                BufferColor = PastCommandColor = PastCommandOutputColor = PromptColor = CursorColor = value;
            }
        }

        internal Texture2D RoundedCorner { get; set; }

        private FKGameConsoleOptions()
        {
            ToggleKey = Keys.OemTilde;                      // 默认波浪符
            BackgroundColor = new Color(0, 0, 0, 195);      // 背景色默认灰黑
            BoundsColor = new Color(0, 0, 0, 255);          // 边框色默认黑
            FontColor = Color.White;                        // 先统一定制内部字体颜色默认白色
            {
                PromptColor = Color.Crimson;
                CursorColor = Color.OrangeRed;
                PastCommandOutputColor = Color.Aqua;
                PastCommandColor = Color.Goldenrod;
                BufferColor = Color.Gold;
            }
            AnimationSpeed = 1;                             // 卷入卷出动画速度
            CursorBlinkSpeed = 0.5f;                        // 光标闪烁速度
            Height = 300;                                   // 默认控制台高度
            Prompt = ">";                                   // 命令输入提示符
            Cursor = '_';                                   // 光标显示符
            Padding = 25;
            Margin = 25;
            OpenOnWrite = true;
        }
    }
}