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
// Create Time         :    2017/7/31 12:00:31
// Update Time         :    2017/7/31 12:00:31
// Class Version       :    v1.0.0.0
// Class Description   :    
// ===============================================================================
using FKVoxelEngine.Base;
using FKVoxelEngine.Debug;
using FKVoxelEngine.Framework;
using FKVoxelEngine.Graphics;
using FKVoxelEngine.RenderObj;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System;
// ===============================================================================
namespace FKVoxelEngine.Input
{
    public class FKInputManager : GameComponent, IFKInputManagerService
    {
        public bool CaptureMouse { get; private set; }
        public bool CursorCentered { get; private set; }
        public delegate bool KeyEventHandler(object sender, FKKeyEventArgs e);
        public event KeyEventHandler KeyDown;

        private MouseState _PrevMouseState;
        private KeyboardState _PrevKeyboardState;
        private static readonly FKLogger Logger = FKLogManager.CreateLogger("FKInputManager");

        private IFKCameraService                    _CameraService;
        private IFKInGameDebuggerService            _InGameDebuggerService;
        private IFKFoggerService                    _FoggerService;
        private IFKSkyService                       _SkyService;
        private IFKBaseChunkCacheService            _BaseChunkCache;
        private IFKProfileManagerService            _ProfileManager;

        public FKInputManager(Game game)
            : base(game)
        {
            Logger.Trace("ctor()");

            Game.Services.AddService(typeof(IFKInputManagerService), this);
            CaptureMouse = true;
            CursorCentered = true;
        }

        public override void Initialize()
        {
            Logger.Trace("Init()");

            _CameraService = (IFKCameraService)Game.Services.GetService(typeof(IFKCameraService));
            _InGameDebuggerService = (IFKInGameDebuggerService)Game.Services.GetService(typeof(IFKInGameDebuggerService));
            _FoggerService = (IFKFoggerService)Game.Services.GetService(typeof(IFKFoggerService));
            _SkyService = (IFKSkyService)Game.Services.GetService(typeof(IFKSkyService));
            _BaseChunkCache = (IFKBaseChunkCacheService)Game.Services.GetService(typeof(IFKBaseChunkCacheService));
            _ProfileManager = (IFKProfileManagerService)Game.Services.GetService(typeof(IFKProfileManagerService));

            _PrevMouseState = Mouse.GetState();
            _PrevKeyboardState = Keyboard.GetState();

            base.Initialize();
        }

        public override void Update(GameTime gameTime)
        {
            this.ProcessMouse(gameTime);
            this.ProcessKeyboard(gameTime);
        }
        /// <summary>
        /// 全局鼠标事件处理
        /// </summary>
        /// <param name="gameTime"></param>
        private void ProcessMouse(GameTime gameTime)
        {
            var currentState = Mouse.GetState();
            if (currentState == _PrevMouseState || !CaptureMouse)
            {
                return;
            }

            {   //摄像机处理
                float fRotation = currentState.X - FKEngine.GetInstance.Config.Graphics.Width / 2;
                if (fRotation != 0)
                {
                    _CameraService.RotateCamera(fRotation);
                }

                float fElevation = currentState.Y - FKEngine.GetInstance.Config.Graphics.Height / 2;
                if (fElevation != 0)
                {
                    _CameraService.ElevateCamera(fElevation);
                }
            }
            
            { // 左右鼠标键
                if (currentState.LeftButton == ButtonState.Pressed && _PrevMouseState.LeftButton == ButtonState.Released)
                {
                    // todo
                }
                if (currentState.RightButton == ButtonState.Pressed && _PrevMouseState.RightButton == ButtonState.Released)
                {
                    // todo;
                }
            }

            _PrevMouseState = currentState;

            // 锁死鼠标在屏幕中央
            CenterCursor();
        }
        /// <summary>
        /// 全局键盘事件
        /// </summary>
        /// <param name="gameTime"></param>
        private void ProcessKeyboard(GameTime gameTime)
        {
            var currentState = Keyboard.GetState();

            bool bHandlered = false;

            // DEBUG按键
            if(!bHandlered)
            {
                if (_PrevKeyboardState.IsKeyUp(Keys.F1) && currentState.IsKeyDown(Keys.F1))
                {
                    FKEngine.GetInstance.Config.World.IsInfinitive = !FKEngine.GetInstance.Config.World.IsInfinitive;
                    bHandlered = true;
                }
                if (_PrevKeyboardState.IsKeyUp(Keys.F2) && currentState.IsKeyDown(Keys.F2))
                {
                    CaptureMouse = !CaptureMouse;
                    Game.IsMouseVisible = !CaptureMouse;
                    bHandlered = true;
                }
                if (_PrevKeyboardState.IsKeyUp(Keys.F3) && currentState.IsKeyDown(Keys.F3))
                {
                    _InGameDebuggerService.ToggleInGameDebugger();
                    bHandlered = true;
                }
                if (_PrevKeyboardState.IsKeyUp(Keys.F4) && currentState.IsKeyDown(Keys.F4))
                {
                    _ProfileManager.ToggleProfileShow();
                    bHandlered = true; 
                }
                if (_PrevKeyboardState.IsKeyUp(Keys.F5) && currentState.IsKeyDown(Keys.F5))
                {
                    FKEngine.GetInstance.Rasterizer.Wireframed = !FKEngine.GetInstance.Rasterizer.Wireframed;
                    bHandlered = true;
                }
                if (_PrevKeyboardState.IsKeyUp(Keys.F12) && currentState.IsKeyDown(Keys.F12))
                {
                    int a = 0;
                    int c = 10 / a;
                    bHandlered = true;
                }
                // 波浪符开启控制台
                if (_PrevKeyboardState.IsKeyUp(Keys.OemTilde) && currentState.IsKeyDown(Keys.OemTilde))
                {
                    KeyDown(null, new FKKeyEventArgs(Keys.OemTilde));
                    bHandlered = true;
                }
            }
            // 控制台处理
            if (!bHandlered && FKEngine.GetInstance.Console.Opened)
            {
                // 控制台按键
                foreach(var @key in Enum.GetValues(typeof(Keys)))
                {
                    if(_PrevKeyboardState.IsKeyUp((Keys)@key) && currentState.IsKeyDown((Keys)@key))
                    {
                        bHandlered = KeyDown(null, new FKKeyEventArgs((Keys)@key));
                    }
                }
            }
            // 摄像机控制
            if (!bHandlered)
            {
                if (currentState.IsKeyDown(Keys.Up) || currentState.IsKeyDown(Keys.W))
                    _CameraService.MoveCamera(gameTime, ENUM_CameraMoveDirection.eForward);
                if (currentState.IsKeyDown(Keys.Down) || currentState.IsKeyDown(Keys.S))
                    _CameraService.MoveCamera(gameTime, ENUM_CameraMoveDirection.eBackward);
                if (currentState.IsKeyDown(Keys.Left) || currentState.IsKeyDown(Keys.A))
                    _CameraService.MoveCamera(gameTime, ENUM_CameraMoveDirection.eLeft);
                if (currentState.IsKeyDown(Keys.Right) || currentState.IsKeyDown(Keys.D))
                    _CameraService.MoveCamera(gameTime, ENUM_CameraMoveDirection.eRight);
                if (_PrevKeyboardState.IsKeyUp(Keys.PageUp))
                    _CameraService.MoveCamera(gameTime, ENUM_CameraMoveDirection.eUp);
                if (_PrevKeyboardState.IsKeyUp(Keys.PageDown))
                    _CameraService.MoveCamera(gameTime, ENUM_CameraMoveDirection.eDown);
            }

            this._PrevKeyboardState = currentState;
        }

        private void CenterCursor()
        {
            Mouse.SetPosition(Game.Window.ClientBounds.Width / 2, Game.Window.ClientBounds.Height / 2);
        }
    }
}