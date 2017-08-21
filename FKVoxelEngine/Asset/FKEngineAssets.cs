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
// Create Time         :    2017/7/29 18:08:37
// Update Time         :    2017/7/29 18:08:37
// Class Version       :    v1.0.0.0
// Class Description   :    引擎基本常规资源枚举
// ===============================================================================
using System;
// ===============================================================================
namespace FKVoxelEngine.Asset
{
    public static class FKEngineAssets
    {
        public static string Font_ConsoleFont = @"Fonts/ConsoleFont";
        public static string Font_DefaultFont = @"Fonts/Verdana";

        public static string Model_SkyDome = @"Models/SkyDome";

        public static string Effect_Block = @"Effects/BlockEffect";
        public static string Effect_BloomExtract = @"Effects/PostProcessing/Bloom/BloomExtract";
        public static string Effect_BloomCombine = @"Effects/PostProcessing/Bloom/BloomCombine";
        public static string Effect_GaussianBlur = @"Effects/PostProcessing/Bloom/GaussianBlur";
        public static string Effect_SkyDome = @"Effects/SkyDome";
        public static string Effect_PerlinNoise = @"Effects/PerlinNoise";

        public static string Texture_RoundedCorner = @"Textures/RoundedCorner";
        public static string Texture_Terrain = @"Textures/Terrain";
        public static string Texture_Cracks = @"Textures/Cracks";
        public static string Texture_NormalCrosshairs = @"Textures/Crosshairs/Normal";
        public static string Texture_ShovelCrosshairs = @"Textures/Crosshairs/Shovel";
        public static string Texture_Cloudmap = @"Textures/Cloudmap";
        public static string Texture_Starmap = @"Textures/Starmap";
        public static string Texture_CloudTexture = @"Textures/CloudTexture";
    }
}