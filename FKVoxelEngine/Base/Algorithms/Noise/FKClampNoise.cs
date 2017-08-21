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
// Create Time         :    2017/8/21 16:53:22
// Update Time         :    2017/8/21 16:53:22
// Class Version       :    v1.0.0.0
// Class Description   :    
// ===============================================================================
using System;
// ===============================================================================
namespace FKVoxelEngine.Base
{
    public class FKClampNoise : FKNoiseGen
    {
        public IFKNoise Noise { get; set; }

        public float MinValue { get; set; }
        public float MaxValue { get; set; }

        public FKClampNoise(IFKNoise noise)
        {
            Noise = noise;
            MinValue = 0;
            MaxValue = 1;
        }

        public override float Noise2D(float x, float y)
        {
            var NoiseValue = Noise.Noise2D(x, y);
            if (NoiseValue < MinValue)
                NoiseValue = MinValue;
            if (NoiseValue > MaxValue)
                NoiseValue = MaxValue;
            return NoiseValue;
        }

        public override float Noise3D(float x, float y, float z)
        {
            var NoiseValue = Noise.Noise3D(x, y, z);
            if (NoiseValue < MinValue)
                NoiseValue = MinValue;
            if (NoiseValue > MaxValue)
                NoiseValue = MaxValue;
            return NoiseValue;
        }
    }
}