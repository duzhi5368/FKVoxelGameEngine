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
// Create Time         :    2017/8/21 17:00:21
// Update Time         :    2017/8/21 17:00:21
// Class Version       :    v1.0.0.0
// Class Description   :    
// ===============================================================================
using System;
// ===============================================================================
namespace FKVoxelEngine.Base
{
    public enum ENUM_NoiseModifier
    {
        eNM_Add,
        eNM_Subtract,
        eNM_Multiply,
        eNM_Power
    }
    public class FKModifyNoise : FKNoiseGen
    {
        public IFKNoise PrimaryNoise { get; set; }
        public IFKNoise SecondaryNoise { get; set; }
        public ENUM_NoiseModifier Modifier { get; set; }

        public FKModifyNoise(IFKNoise primaryNoise, IFKNoise secondaryNoise,
            ENUM_NoiseModifier modifier = ENUM_NoiseModifier.eNM_Add)
        {
            PrimaryNoise = primaryNoise;
            SecondaryNoise = secondaryNoise;
            Modifier = modifier;
        }

        public override float Noise2D(float x, float y)
        {
            switch (Modifier)
            {
                case ENUM_NoiseModifier.eNM_Add:
                    return PrimaryNoise.Noise2D(x, y) + SecondaryNoise.Noise2D(x, y);
                case ENUM_NoiseModifier.eNM_Multiply:
                    return PrimaryNoise.Noise2D(x, y) * SecondaryNoise.Noise2D(x, y);
                case ENUM_NoiseModifier.eNM_Power:
                    return (float)Math.Pow(PrimaryNoise.Noise2D(x, y), SecondaryNoise.Noise2D(x, y));
                case ENUM_NoiseModifier.eNM_Subtract:
                    return PrimaryNoise.Noise2D(x, y) - SecondaryNoise.Noise2D(x, y);
                default:
                    return PrimaryNoise.Noise2D(x, y) + SecondaryNoise.Noise2D(x, y);
            }
        }

        public override float Noise3D(float x, float y, float z)
        {
            switch (Modifier)
            {
                case ENUM_NoiseModifier.eNM_Add:
                    return PrimaryNoise.Noise3D(x, y, z) + SecondaryNoise.Noise3D(x, y, z);
                case ENUM_NoiseModifier.eNM_Multiply:
                    return PrimaryNoise.Noise3D(x, y, z) * SecondaryNoise.Noise3D(x, y, z);
                case ENUM_NoiseModifier.eNM_Power:
                    return (float)Math.Pow(PrimaryNoise.Noise3D(x, y, z), SecondaryNoise.Noise3D(x, y, z));
                case ENUM_NoiseModifier.eNM_Subtract:
                    return PrimaryNoise.Noise3D(x, y, z) - SecondaryNoise.Noise3D(x, y, z);
                default:
                    return PrimaryNoise.Noise3D(x, y, z) + SecondaryNoise.Noise3D(x, y, z);
            }
        }
    }
}