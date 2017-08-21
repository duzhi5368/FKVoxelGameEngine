using FKVoxelClient.Base;
using FKVoxelEngine.Base;
using FKVoxelEngine.Platform;
using FKVoxelEngineTest.Setting;
using System;
using System.Reflection;
using System.Threading;

namespace FKVoxelEngineTest
{
#if WINDOWS || LINUX
    /// <summary>
    /// The main class.
    /// </summary>
    public class Program
    {
        private static readonly FKLogger Logger = FKLogManager.CreateLogger("App");

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main(string[] args)
        {
            new Program(args);
        }

        public Program(string[] args){
#if !DEBUG
            // 进行错误捕获
            FKCrashReporter.DefaultCrashFileName = "FKVoxelEngineTestCrash.txt";
            FKCrashReporter.EnableGlobalExceptionHandler(true, true);
            new FKCrashReporter().Start(delegate { MyMain(); });
#else
            MyMain();
#endif
        }

        private void MyMain()
        {
            // 输出基本信息
            Console.ForegroundColor = ConsoleColor.Yellow;
            PrintBanner();
            PrintDebugKeys();
            Console.ResetColor();

            // 初始化Log
            InitLoggers();

            // 输出其他信息
            Logger.Info(string.Format("Platform = {0} DotNet = {1}.",
                FKPlatformManager.GetInstance.CurPlatform,
                FKPlatformManager.GetInstance.DotNetFrameworkVersion));
            Logger.Info(string.Format("Game framework = {0} Version = {1} RenderAPI = {2}.",
                FKPlatformManager.GetInstance.CurGameFramework,
                FKPlatformManager.GetInstance.GameFrameworkVersion,
                FKPlatformManager.GetInstance.CurGraphicsAPI));

            // 开始执行
            using (var game = new GameApp())
            {
                Logger.Info("Start game loop...");
                Thread.CurrentThread.Name = "FKMainThread";
                FKPlatformManager.GetInstance.Startup(game, game.graphics);
            }

            Environment.Exit(0);
        }

        /// <summary>
        /// 输出静态信息
        /// </summary>
        private static void PrintBanner()
        {
            Console.WriteLine("==============================================");
            Console.WriteLine("Copyright (C) 2011 - 2013, FreeKnight personal");
            Assembly appAssembly = Assembly.GetExecutingAssembly();
            Version appVersion = appAssembly.GetName().Version;
            Assembly engineAssembly = Assembly.LoadFrom("FKVoxelEngine.dll");
            Version engineVersion = engineAssembly.GetName().Version;
            Console.WriteLine($"AppVersion = {appVersion.ToString()}");
            Console.WriteLine($"EngineVersion = {engineVersion.ToString()}");
            Console.WriteLine("==============================================");
        }
        /// <summary>
        /// 输出DEBUG KEY
        /// </summary>
        private static void PrintDebugKeys()
        {
            Console.WriteLine("Debug keys:");
            Console.WriteLine("-----------------------------");
            Console.WriteLine("~: Debug Console: On/Off");
            Console.WriteLine("F1: Infinitive-world: On/Off.");
            Console.WriteLine("F2: Capture Mouse: On/Off.");
            Console.WriteLine("F3: In-game Debugger: On/Off.");
            Console.WriteLine("F4: Profile Debugger: On/Off.");
            Console.WriteLine("F5: Wireframe Render: On/Off.");
            Console.WriteLine("F12: Test Dump.");
            Console.WriteLine("-----------------------------");
            Console.WriteLine();
        }
        /// <summary>
        /// 初始化Logger日志
        /// </summary>
        private static void InitLoggers()
        {
            var logSettings = new LogSetting();

            FKLogManager.Enabled = true;

            foreach (var targetConfig in logSettings.Handlers)
            {
                if (!targetConfig.Enabled)
                    continue;

                FKLogHandler handler = null;
                switch (targetConfig.Target.ToLower())
                {
                    case "console":
                        handler = new FKConsoleHandler(targetConfig.MinimumLevel, targetConfig.MaximumLevel,
                                                   targetConfig.IncludeTimeStamps);
                        break;
                    case "file":
                        handler = new FKFileHandler(targetConfig.FileName, targetConfig.MinimumLevel,
                                                targetConfig.MaximumLevel, targetConfig.IncludeTimeStamps,
                                                targetConfig.ResetOnStartup);
                        break;
                }

                if (handler != null)
                    FKLogManager.AttachLogHandler(handler);
            }
        }
    }
#endif
        }
