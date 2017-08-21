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
// Create Time         :    2017/8/2 14:36:57
// Update Time         :    2017/8/2 14:36:57
// Class Version       :    v1.0.0.0
// Class Description   :    引擎基本常规资源加载与管理
// ===============================================================================
using FKVoxelEngine.Framework;
using Microsoft.Xna.Framework;
using System;
using Microsoft.Xna.Framework.Graphics;
using FKVoxelEngine.Base;
using FKVoxelEngine.Platform;
// ===============================================================================
namespace FKVoxelEngine.Asset
{
    public class FKAssetManager : GameComponent, IFKAssetManagerService
    {
        public Model SkyDomeModel { get; private set; }
        public Effect SkyDomeEffect { get; private set; }
        public Effect BlockEffect { get; private set; }
        public Effect BloomCombineEffect { get; private set; }
        public Effect BloomExtractEffect { get; private set; }
        public Effect GaussianBlurEffect { get; private set; }
        public Effect PerlinNoiseEffect { get; private set; }

        public Texture2D BlockTextureAtlas { get; private set; }
        public Texture2D CloudMapTexture { get; private set; }
        public Texture2D CloudTexture { get; private set; }
        public Texture2D CrackTextureAtlas { get; private set; }
        public Texture2D CrossHairNormalTexture { get; private set; }
        public Texture2D CrossHairShovelTexture { get; private set; }
        public Texture2D StarMapTexture { get; private set; }

        public SpriteFont DefaultFont { get; private set; }


        private static readonly FKLogger _Logger = FKLogManager.CreateLogger("FKAssetManager");
        private static string _strEffectShaderExtension;

        public FKAssetManager(Game game)
            : base(game)
        {
            if (FKPlatformManager.GetInstance.CurGameFramework == FKFrameworkEnum.eFramework_MonoGame)
            {
                if (FKPlatformManager.GetInstance.CurGraphicsAPI == FKGraphicsAPIEnum.eGraphicsAPI_DirectX11)
                {
                    _strEffectShaderExtension = ".dx11";
                }
                else
                {
                    _strEffectShaderExtension = "";
                }
            }
            else
                _strEffectShaderExtension = "";

            Game.Services.AddService(typeof(IFKAssetManagerService), this);
        }

        public override void Initialize()
        {
            _Logger.Trace("Init()");

            LoadDefaultContent();
            base.Initialize();
        }

        private void LoadDefaultContent()
        {
            try
            {
                SkyDomeModel = Game.Content.Load<Model>(FKEngineAssets.Model_SkyDome);

                BlockEffect = LoadEffectShader(FKEngineAssets.Effect_Block);
                BloomExtractEffect = LoadEffectShader(FKEngineAssets.Effect_BloomExtract);
                BloomCombineEffect = LoadEffectShader(FKEngineAssets.Effect_BloomCombine);
                GaussianBlurEffect = LoadEffectShader(FKEngineAssets.Effect_GaussianBlur);
                SkyDomeEffect = LoadEffectShader(FKEngineAssets.Effect_SkyDome);
                PerlinNoiseEffect = LoadEffectShader(FKEngineAssets.Effect_PerlinNoise);

                BlockTextureAtlas = Game.Content.Load<Texture2D>(FKEngineAssets.Texture_Terrain);
                CrackTextureAtlas = Game.Content.Load<Texture2D>(FKEngineAssets.Texture_Cracks);
                CrossHairNormalTexture = Game.Content.Load<Texture2D>(FKEngineAssets.Texture_NormalCrosshairs);
                CrossHairShovelTexture = Game.Content.Load<Texture2D>(FKEngineAssets.Texture_ShovelCrosshairs);
                CloudMapTexture = Game.Content.Load<Texture2D>(FKEngineAssets.Texture_Cloudmap);
                StarMapTexture = Game.Content.Load<Texture2D>(FKEngineAssets.Texture_Starmap);
                CloudTexture = Game.Content.Load<Texture2D>(FKEngineAssets.Texture_CloudTexture);

                DefaultFont = Game.Content.Load<SpriteFont>(FKEngineAssets.Font_DefaultFont);
            }
            catch(Exception e)
            {
                _Logger.FatalException(e, "Error while loading assets!");
                Console.ReadLine();
                Environment.Exit(-1);
            }
        }

        private Effect LoadEffectShader(string path)
        {
            return Game.Content.Load<Effect>(path + _strEffectShaderExtension);
        }
    }
}