using FKVoxelClient.Language;
using FKVoxelEngine.Base;
using FKVoxelEngine.Config;
using FKVoxelEngine.Framework;
using FKVoxelEngine.Graphics;
using FKVoxelEngine.Platform;
using FKVoxelEngine.Profile;
using FKVoxelEngineTest.Setting;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace FKVoxelEngineTest
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class GameApp : Game
    {
        public GraphicsDeviceManager graphics;
        public FKGraphicsManager screenManager;
        private FKTimeRuler _TimeRuler;
        private FKLanguageManager _LanguageMgr;
        private static readonly FKLogger Logger = FKLogManager.CreateLogger("GameApp");
        public GameApp()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }

        protected override void Initialize()
        {
            Logger.Trace("Init()");

            Window.Title = string.Format("FKVoxelGame [{0}/{1}]",
                FKPlatformManager.GetInstance.CurGameFramework,
                FKPlatformManager.GetInstance.CurGraphicsAPI);

            IsMouseVisible = false;

            var audioSetting = new AudioSetting();
            var graphicsSetting = new GraphicsSettings();

            var config = new FKEngineConfig {
                Chunk =
                {
                    WidthInBlocks = 16,
                    HeightInBlocks = 128,
                    LengthInBlocks = 16,
                },
                Cache =
                {
                    CacheExtraChunks = true,
                    ViewRange = 12,
                    CacheRange = 16,
                },
                Graphics =
                {
                    Width = graphicsSetting.Width,
                    Height = graphicsSetting.Height,
                    FullScreenEnabled = graphicsSetting.FullScreenEnabled,
                    VerticalSyncEnabled = graphicsSetting.VerticalSyncEnabled,
                    FixedTimeStepsEnabled = graphicsSetting.FixedTimeStepsEnabled,
                },
                World =
                {
                    IsInfinitive = true,
                },
                Debug =
                {
                    GraphicsEnabled = true,
                },
                Effect =
                {
                    BloomEnabled = false,
                    BloomState = ENUM_FKBloomState.eSaturated,
                },
                Audio =
                {
                    Enabled = audioSetting.Enabled,
                }
            };

            FKEngine.GetInstance.Init(this, config);
            Logger.Info("\n" + FKEngine.GetInstance.Config.ToString());

            screenManager = new FKGraphicsManager(this, graphics);
            FKEngine.GetInstance.EngineStart += OnEngineStart;
            FKEngine.GetInstance.Run();

            base.Initialize();
        }

        private void OnEngineStart(object sender, EventArgs e)
        {
            _TimeRuler = new FKTimeRuler(this)
            {
                Visible = true,
                ShowLog = true,
            };
            Components.Add(_TimeRuler);

            _LanguageMgr = new FKLanguageManager(this);
            Components.Add(_LanguageMgr);
        }

        protected override void LoadContent()
        {
        }

        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        protected override void Update(GameTime gameTime)
        {
            _TimeRuler.StartFrame();
            _TimeRuler.BeginMark("Update", Color.Blue);

            base.Update(gameTime);

            _TimeRuler.EndMark("Update");
        }

        protected override void Draw(GameTime gameTime)
        {
            _TimeRuler.BeginMark("Draw", Color.Yellow);

            var skyColor = new Color(128, 173, 254);
            GraphicsDevice.Clear(skyColor);

            GraphicsDevice.RasterizerState = FKEngine.GetInstance.Rasterizer.State;
            base.Draw(gameTime);

            _TimeRuler.EndMark("Draw");
        }
    }
}
