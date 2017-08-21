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
// Create Time         :    2017/7/26 13:57:59
// Update Time         :    2017/7/26 13:57:59
// Class Version       :    v1.0.0.0
// Class Description   :    
// ===============================================================================
using Microsoft.Xna.Framework.Graphics;
using System;
// ===============================================================================
namespace FKVoxelEngine.Framework
{
    public interface IFKAssetManagerService
    {
        SpriteFont DefaultFont { get; }
        Model SkyDomeModel { get; }

        Effect BlockEffect { get; }
        Effect BloomExtractEffect { get; }
        Effect BloomCombineEffect { get; }
        Effect GaussianBlurEffect { get; }
        Effect SkyDomeEffect { get; }
        Effect PerlinNoiseEffect { get; }

        Texture2D BlockTextureAtlas { get; }
        Texture2D CrackTextureAtlas { get; }
        Texture2D CrossHairNormalTexture { get; }
        Texture2D CrossHairShovelTexture { get; }
        Texture2D CloudMapTexture { get; }
        Texture2D StarMapTexture { get; }
        Texture2D CloudTexture { get; }
    }
}