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
// Create Time         :    2017/7/26 14:29:05
// Update Time         :    2017/7/26 14:29:05
// Class Version       :    v1.0.0.0
// Class Description   :    
// ===============================================================================
using System;
using FKVoxelEngine.Config;
using Microsoft.Xna.Framework;
using FKVoxelEngine.Base;
using FKVoxelEngine.Debug;
using FKVoxelEngine.Graphics;
using FKVoxelEngine.Input;
using FKVoxelEngine.Asset;
using FKVoxelEngine.RenderObj;
using FKVoxelEngine.Profile;
using FKVoxelEngine.Audio;
using Microsoft.Xna.Framework.Graphics;
// ===============================================================================
namespace FKVoxelEngine.Framework
{
    public class FKEngine : FKSingleton<FKEngine>
    {
        public FKEngineConfig Config { get; private set; }
        public FKRasterizer Rasterizer { get; private set; }
        public Game Game { get; private set; }

        public FKGameConsole Console { get; private set; }

        public delegate void EngineStartHandler(object sender, EventArgs e);
        public EngineStartHandler EngineStart;

        public bool Disposed = false;

        private static readonly FKLogger Logger = FKLogManager.CreateLogger("FKEngine");
        private FKEngine()
        {

        }
        public void Init(Game game, FKEngineConfig config)
        {
            Logger.Trace("Init() Begin");
            Game = game;
            Config = config;

            if (Config.Setup())
            {
                Logger.Trace("Config Setup() successed.");
            }
            else
            {
                Logger.Trace("Config Setup() failed.");
            }
            Logger.Trace("Init() End");
        }

        public void Run()
        {
            Logger.Trace("Run() Begin");
            AddComponents();
            CreateConsole();
            NotifyEngineStart(EventArgs.Empty);
            Logger.Trace("Run() End");
        }

        private void NotifyEngineStart(EventArgs e)
        {
            var handler = EngineStart;
            if (handler != null)
                handler(typeof(FKEngine), e);
        }

        private void AddComponents()
        {
            Logger.Trace($"AddComponents() Begin, MemUsed = {GC.GetTotalMemory(false).GetKiloString()}");

            Rasterizer = new FKRasterizer();
            var inputManager = new FKInputManager(Game);
            var assetManager = new FKAssetManager(Game);
            var sky = new FKSky(Game);
            var fog = new FKFogger(Game);
            var chunkStorage = new FKBaseChunkStorage(Game);
            var vertexBuilder = new FKVertexBuilder(Game);
            var chunkCache = new FKBaseChunkCache(Game);
            var blockStorage = new FKBaseBlockStorage(Game);
            var world = new FKWorld(Game);
            var camera = new FKCamera(Game);
            var userInterface = new FKUserInterfaceManager(Game);
            var inGameDebugger = new FKInGameDebugger(Game);
            var statistics = new FKStatistics(Game);
            var profileManager = new FKProfileManager(Game);
            var audioManager = new FKAudioManager(Game);

            Game.Components.Add(inputManager);
            Game.Components.Add(assetManager);
            Game.Components.Add(sky);
            Game.Components.Add(fog);
            Game.Components.Add(chunkStorage);
            Game.Components.Add(vertexBuilder);
            Game.Components.Add(chunkCache);
            Game.Components.Add(blockStorage);
            Game.Components.Add(world);
            Game.Components.Add(camera);
            Game.Components.Add(userInterface);
            Game.Components.Add(inGameDebugger);
            Game.Components.Add(statistics);
            Game.Components.Add(profileManager);
            Game.Components.Add(audioManager);

            Logger.Trace($"AddComponents() End, MemUsed = {GC.GetTotalMemory(false).GetKiloString()}");
        }

        private void CreateConsole()
        {
            Logger.Trace("CreateConsole() Begin");

            FKGameConsoleOptions.GetInstance.Font = ((IFKAssetManagerService)Game.Services.GetService(typeof(IFKAssetManagerService))).DefaultFont;
            Console = new FKGameConsole(Game);

            Logger.Trace("CreateConsole() End");
        }

        private void Dispose(bool disposing)
        {
            if (Disposed)
                return;

            // TODO:

            Disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        ~FKEngine()
        {
            Dispose(false);
        }
    }
}