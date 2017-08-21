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
// Create Time         :    2017/8/21 16:43:46
// Update Time         :    2017/8/21 16:43:46
// Class Version       :    v1.0.0.0
// Class Description   :    
// ===============================================================================
using System;
// ===============================================================================
namespace FKVoxelEngine.Base
{
    public enum ENUM_InterpolateType
    {
        eIT_COSINE,
        eIT_LINEAR
    }
    public abstract class FKNoiseGen : IFKNoise
    {
        public abstract float Noise2D(float x, float y);
        public abstract float Noise3D(float x, float y, float z);

        public static float Interpolate(float pointA, float pointB, float t, ENUM_InterpolateType type)
        {
            switch (type)
            {
                case ENUM_InterpolateType.eIT_COSINE:
                    return CosineInterpolate(pointA, pointB, t);
                case ENUM_InterpolateType.eIT_LINEAR:
                    return LinearInterpolate(pointA, pointB, t);
                default:
                    return LinearInterpolate(pointA, pointB, t);
            }
        }

        private static float CosineInterpolate(float pointA, float pointB, float t)
        {
            var F = t * Math.PI;
            var G = (1 - Math.Cos(F)) * 0.5;
            return (float)(pointA * (1 - G) + pointB * G);
        }

        private static float CubicInterpolate(float pointA, float pointB, float pointC,
            float pointD, float t)
        {
            var E = (pointD - pointC) - (pointA - pointB);
            var F = (pointA - pointB) - E;
            var G = pointC - pointA;
            var H = pointB;
            return (float)(E * Math.Pow(t, 3) + F * Math.Pow(t, 2) + G * t + H);
        }

        private static float LinearInterpolate(float pointA, float pointB, float t)
        {
            return pointA * (1 - t) + pointB * t;
        }

        public static float BiLinearInterpolate(float x, float y, float point00,
            float point01, float point10, float point11)
        {
            float Point0 = LinearInterpolate(point00, point10, x);
            float Point1 = LinearInterpolate(point01, point11, x);

            return LinearInterpolate(Point0, Point1, y);
        }

        public static float TriLinearInterpolate(float x, float y, float z,
            float point000, float point001, float point010,
            float point100, float point011, float point101,
            float point110, float point111)
        {
            float Point0 = BiLinearInterpolate(x, y, point000, point001, point100, point101);
            float Point1 = BiLinearInterpolate(x, y, point010, point011, point110, point111);

            return LinearInterpolate(Point0, Point1, z);
        }
    }
}