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
// Create Time         :    2017/7/29 18:23:22
// Update Time         :    2017/7/29 18:23:22
// Class Version       :    v1.0.0.0
// Class Description   :    
// ===============================================================================
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
// ===============================================================================
namespace FKVoxelEngine.Debug
{
    class FKGameConsoleRenderer
    {
        #region ==== 成员变量 ====

        public enum ENUM_State
        {
            eOpened = 0,
            eOpening,
            eClosed,
            eClosing
        }

        private readonly SpriteBatch _SpriteBatch;
        private readonly FKGameConsoleInputProcessor _InputProcessor;
        private readonly Texture2D _Pixel;
        private readonly int _Width;
        private readonly float _OneCharWidth;
        private readonly int _MaxCharPerLine;
        private Vector2 _OpenedPosotion, _ClosedPosition, _Position;
        private Vector2 _FirstCommandPositionOffset;
        private DateTime _StateChangeTime;
        private ENUM_State _CurrentState;

        #endregion ==== 成员变量 ====

        #region ==== 对外接口 ====

        public FKGameConsoleRenderer(Game game, SpriteBatch spriteBatch, FKGameConsoleInputProcessor inputProcessor)
        {
            _CurrentState = ENUM_State.eClosed; // 默认关闭
            _Width = game.GraphicsDevice.Viewport.Width;
            _Position = _ClosedPosition = new Vector2(FKGameConsoleOptions.GetInstance.Margin, -FKGameConsoleOptions.GetInstance.Height - FKGameConsoleOptions.GetInstance.RoundedCorner.Height);
            _OpenedPosotion = new Vector2(FKGameConsoleOptions.GetInstance.Margin, 0);
            _SpriteBatch = spriteBatch;
            _InputProcessor = inputProcessor;
            _Pixel = new Texture2D(game.GraphicsDevice, 1, 1, false, SurfaceFormat.Color);
            _Pixel.SetData(new[] { Color.White });
            _FirstCommandPositionOffset = new Vector2(Bounds.Left, 0);
            _OneCharWidth = FKGameConsoleOptions.GetInstance.Font.MeasureString("x").X;
            _MaxCharPerLine = (int)((Bounds.Width - FKGameConsoleOptions.GetInstance.Padding * 2) / _OneCharWidth);
        }

        public void Update(GameTime gameTime)
        {
            // 播放出场和关闭卷入卷出动画
            if(_CurrentState == ENUM_State.eOpening)
            {
                _Position.Y = MathHelper.SmoothStep(_Position.Y, _OpenedPosotion.Y, ((float)(DateTime.Now - _StateChangeTime).TotalSeconds / FKGameConsoleOptions.GetInstance.AnimationSpeed));
                if(_Position.Y == _OpenedPosotion.Y)
                {
                    _CurrentState = ENUM_State.eOpened;
                }
            }
            if(_CurrentState == ENUM_State.eClosing)
            {
                _Position.Y = MathHelper.SmoothStep(_Position.Y, _ClosedPosition.Y, ((float)(DateTime.Now - _StateChangeTime).TotalSeconds / FKGameConsoleOptions.GetInstance.AnimationSpeed));
                if (_Position.Y == _ClosedPosition.Y)
                {
                    _CurrentState = ENUM_State.eClosed;
                }
            }
        }
        public void Draw(GameTime gameTime)
        {
            if (_CurrentState == ENUM_State.eClosed)
                return;
            // 绘制背景
            _SpriteBatch.Draw(_Pixel, Bounds, FKGameConsoleOptions.GetInstance.BackgroundColor);
            // 绘制背景的包围边框
            DrawRoundedEdges();
            // 绘制全部历史可见命令和处理结果
            var nextCommandPosition = DrawCommands(_InputProcessor.Out, _FirstCommandPositionOffset);
            // 绘制等待输入符 >
            nextCommandPosition = DrawPrompt(nextCommandPosition);
            // 绘制当前命令和处理结果（非历史）
            var bufferPosition = DrawCommand(_InputProcessor.Buffer.ToString(), nextCommandPosition, FKGameConsoleOptions.GetInstance.BufferColor);
            // 绘制光标 _
            DrawCursor(bufferPosition, gameTime);
        }
        /// <summary>
        /// 开启控制台
        /// </summary>
        public void Open()
        {
            if (_CurrentState == ENUM_State.eOpening || _CurrentState == ENUM_State.eOpened)
                return;
            _StateChangeTime = DateTime.Now;
            _CurrentState = ENUM_State.eOpening;
        }
        /// <summary>
        /// 关闭控制台
        /// </summary>
        public void Close()
        {
            if (_CurrentState == ENUM_State.eClosing || _CurrentState == ENUM_State.eClosed)
                return;
            _StateChangeTime = DateTime.Now;
            _CurrentState = ENUM_State.eClosing;
        }
        /// <summary>
        /// 是否处于已开启状态
        /// </summary>
        public bool IsOpen
        {
            get
            {
                return _CurrentState == ENUM_State.eOpened;
            }
        }
        /// <summary>
        /// 控制台包围盒
        /// </summary>
        public Rectangle Bounds
        {
            get
            {
                return new Rectangle((int)_Position.X, (int)_Position.Y,
                    _Width - (FKGameConsoleOptions.GetInstance.Margin * 2),
                    FKGameConsoleOptions.GetInstance.Height);
            }
        }

        #endregion ==== 对外接口 ====

        #region ==== 部件渲染 ====

        /// <summary>
        /// 绘制控制台包围圈
        /// </summary>
        private void DrawRoundedEdges()
        {
            _SpriteBatch.Draw(FKGameConsoleOptions.GetInstance.RoundedCorner, 
                new Vector2(_Position.X, _Position.Y + FKGameConsoleOptions.GetInstance.Height),
                null, FKGameConsoleOptions.GetInstance.BoundsColor);
            _SpriteBatch.Draw(FKGameConsoleOptions.GetInstance.RoundedCorner,
                new Vector2(_Position.X + Bounds.Width - FKGameConsoleOptions.GetInstance.RoundedCorner.Width, _Position.Y + FKGameConsoleOptions.GetInstance.Height),
                null, FKGameConsoleOptions.GetInstance.BoundsColor, 0, Vector2.Zero, 1, SpriteEffects.FlipHorizontally, 1);
            _SpriteBatch.Draw(_Pixel, new Rectangle(Bounds.X + FKGameConsoleOptions.GetInstance.RoundedCorner.Width, Bounds.Y + FKGameConsoleOptions.GetInstance.Height,
                Bounds.Width - FKGameConsoleOptions.GetInstance.RoundedCorner.Width * 2, FKGameConsoleOptions.GetInstance.RoundedCorner.Height),
                FKGameConsoleOptions.GetInstance.BoundsColor);
        }
        /// <summary>
        /// 绘制输入光标 _
        /// </summary>
        /// <param name="pos"></param>
        /// <param name="gameTime"></param>
        private void DrawCursor(Vector2 pos, GameTime gameTime)
        {
            if (!IsInBounds(pos.Y))
                return;

            var split = SplitCommand(_InputProcessor.Buffer.ToString(), _MaxCharPerLine).Last();
            pos.X += FKGameConsoleOptions.GetInstance.Font.MeasureString(split).X;
            pos.Y -= FKGameConsoleOptions.GetInstance.Font.LineSpacing;
            _SpriteBatch.DrawString(FKGameConsoleOptions.GetInstance.Font,
                (int)(gameTime.TotalGameTime.TotalSeconds / FKGameConsoleOptions.GetInstance.CursorBlinkSpeed) % 2 == 0 ?
                FKGameConsoleOptions.GetInstance.Cursor.ToString() : "",
                pos, FKGameConsoleOptions.GetInstance.CursorColor);
        }
        /// <summary>
        /// 绘制单条命令和执行结果
        /// </summary>
        /// <param name="command"></param>
        /// <param name="pos"></param>
        /// <param name="color"></param>
        /// <returns></returns>
        private Vector2 DrawCommand(string command, Vector2 pos, Color color)
        {
            var splitLines = command.Length > _MaxCharPerLine ? SplitCommand(command, _MaxCharPerLine) : new[] { command };
            foreach(var line in splitLines)
            {
                if(IsInBounds(pos.Y))
                    _SpriteBatch.DrawString(FKGameConsoleOptions.GetInstance.Font, line, pos, color);

                ValidateFirstCommandPosition(pos.Y + FKGameConsoleOptions.GetInstance.Font.LineSpacing);
                pos.Y += FKGameConsoleOptions.GetInstance.Font.LineSpacing;
            }
            return pos;
        }
        /// <summary>
        /// 绘制全部历史命令和执行结果
        /// </summary>
        /// <param name="lines"></param>
        /// <param name="pos"></param>
        /// <returns></returns>
        private Vector2 DrawCommands(IEnumerable<FKGameConsoleOutputLine> lines, Vector2 pos)
        {
            var originalX = pos.X;
            foreach(var command in lines)
            {
                if(command.Type == ENUM_OutputLineType.eCommand)
                {
                    pos = DrawPrompt(pos);
                }
                pos.Y = DrawCommand(command.ToString(), pos, command.Type == ENUM_OutputLineType.eCommand ?
                    FKGameConsoleOptions.GetInstance.PastCommandColor : FKGameConsoleOptions.GetInstance.PastCommandOutputColor).Y;
                pos.X = originalX;
            }
            return pos;
        }
        /// <summary>
        /// 绘制等待输入符 >
        /// </summary>
        /// <param name="pos"></param>
        /// <returns></returns>
        private Vector2 DrawPrompt(Vector2 pos)
        {
            _SpriteBatch.DrawString(FKGameConsoleOptions.GetInstance.Font, FKGameConsoleOptions.GetInstance.Prompt, pos, FKGameConsoleOptions.GetInstance.PromptColor);
            pos.X += _OneCharWidth * FKGameConsoleOptions.GetInstance.Prompt.Length + _OneCharWidth;
            return pos;
        }

        #endregion ==== 部件渲染 ====

        #region ==== 内部函数 ====

        private bool IsInBounds(float yPosition)
        {
            return yPosition < _OpenedPosotion.Y + FKGameConsoleOptions.GetInstance.Height;
        }
        private void ValidateFirstCommandPosition(float fNextCommandY)
        {
            if (!IsInBounds(fNextCommandY))
            {
                _FirstCommandPositionOffset.Y -= FKGameConsoleOptions.GetInstance.Font.LineSpacing;
            }
        }
        private IEnumerable<string> SplitCommand(string command, int max)
        {
            var lines = new List<string>();
            while (command.Length > max)
            {
                var splitCommand = command.Substring(0, max);
                lines.Add(splitCommand);
                command = command.Substring(max, command.Length - max);
            }
            lines.Add(command);
            return lines;
        }

        #endregion ==== 内部函数 ====
    }
}