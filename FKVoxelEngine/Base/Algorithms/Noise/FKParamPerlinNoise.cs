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
// Create Time         :    2017/8/21 17:20:11
// Update Time         :    2017/8/21 17:20:11
// Class Version       :    v1.0.0.0
// Class Description   :    
// ===============================================================================
using System;
// ===============================================================================
namespace FKVoxelEngine.Base
{
    public class FKParamPerlinNoise : FKNoiseGen
    {
        public int Seed { get; set; }
        public int Octaves { get; set; }
        public float Amplitude { get; set; }
        public float Persistance { get; set; }
        public float Frequency { get; set; }
        public float Lacunarity { get; set; }
        public ENUM_InterpolateType Interpolation { get; set; }

        public FKParamPerlinNoise(int seed)
        {
            Seed = seed;
            Octaves = 2;
            Amplitude = 2;
            Persistance = 1;
            Frequency = 1;
            Lacunarity = 2;
            Interpolation = ENUM_InterpolateType.eIT_COSINE;
        }

        private float _Noise2D(float X, float Y)
        {
            var N = ((int)X * 1619 + (int)Y * 31337 * 1013 * Seed) & 0x7fffffff;
            N = (N << 13) ^ N;
            return (float)(1.0 - ((N * (N * N * 15731 + 789221) + 1376312589) & 0x7fffffff) / 1073741824.0);
        }

        private float _Noise3D(float X, float Y, float Z)
        {
            var N = ((int)X * 1619 + (int)Y * 31337 + (int)Z * 52591 * 1013 * Seed) & 0x7fffffff;
            N = (N << 13) ^ N;
            return (float)(1.0 - ((N * (N * N * 15731 + 789221) + 1376312589) & 0x7fffffff) / 1073741824.0);
        }

        /*
         * Perlin Noise methods
         */
        public override float Noise2D(float X, float Y)
        {
            float Total = 0.0f;
            float _Frequency = Frequency;
            float _Amplitude = Amplitude;

            for (int I = 0; I < Octaves; I++)
            {
                Total += Interpolated2D(X * _Frequency, Y * _Frequency) * _Amplitude;
                _Frequency *= Lacunarity;
                _Amplitude *= Persistance;
            }
            return Total;
        }

        public override float Noise3D(float X, float Y, float Z)
        {
            float Total = 0.0f;
            float _Frequency = Frequency;
            float _Amplitude = Amplitude;

            for (int I = 0; I < Octaves; I++)
            {
                Total += Interpolated3D(X * _Frequency, Y * _Frequency, Z * _Frequency) * _Amplitude;
                _Frequency *= Lacunarity;
                _Amplitude *= Persistance;
            }
            return Total;
        }


        private float Smooth2D(float X, float Y)
        {
            float X0 = X - 1;
            float X1 = X + 1;
            float Y0 = Y - 1;
            float Y1 = Y + 1;

            float Corners = (_Noise2D(X0, Y0) + _Noise2D(X1, Y0) + _Noise2D(X0, Y1) + _Noise2D(X1, Y1)) / 16;
            float Sides = (_Noise2D(X0, Y) + _Noise2D(X1, Y) + _Noise2D(X, Y0) + _Noise2D(X, Y1)) / 8;
            float Center = _Noise2D(X, Y) / 4;

            return Corners + Sides + Center;
        }

        private float Smooth3D(float X, float Y, float Z)
        {
            float edges = 0;
            edges += _Noise3D(X + 1, Y + 1, Z) + _Noise3D(X - 1, Y + 1, Z) + _Noise3D(X, Y + 1, Z + 1) + _Noise3D(X, Y + 1, Z - 1);
            edges += _Noise3D(X + 1, Y - 1, Z) + _Noise3D(X - 1, Y - 1, Z) + _Noise3D(X, Y - 1, Z + 1) + _Noise3D(X, Y - 1, Z - 1);
            edges += _Noise3D(X + 1, Y, Z + 1) + _Noise3D(X + 1, Y, Z - 1) + _Noise3D(X - 1, Y, Z + 1) + _Noise3D(X - 1, Y, Z - 1);
            edges /= 48;
            float corners = 0;
            corners += _Noise3D(X - 1, Y - 1, Z - 1) + _Noise3D(X - 1, Y - 1, Z + 1) + _Noise3D(X - 1, Y + 1, Z - 1) + _Noise3D(X - 1, Y + 1, Z + 1);
            corners += _Noise3D(X + 1, Y - 1, Z - 1) + _Noise3D(X + 1, Y - 1, Z + 1) + _Noise3D(X + 1, Y + 1, Z - 1) + _Noise3D(X + 1, Y + 1, Z + 1);
            corners /= 32;
            float sides = 0;
            corners += _Noise3D(X - 1, Y, Z) + _Noise3D(X - 1, Y, Z) + _Noise3D(X, Y + 1, Z);
            corners += _Noise3D(X, Y - 1, Z) + _Noise3D(X, Y, Z + 1) + _Noise3D(X, Y, Z - 1);
            corners /= 16;
            float center = _Noise3D(X, Y, Z) / 8;
            return corners + sides + center;
        }


        public float Interpolated2D(float X, float Y)
        {
            //Grid Cell Coordinates
            int X0 = FKMathFuncs.Floor(X);
            int X1 = X0 + 1;
            int Y0 = FKMathFuncs.Floor(Y);
            int Y1 = Y0 + 1;

            //Interpolation weights
            float SX = X - (float)X0;
            float SY = Y - (float)Y0;

            //Interpolate
            float N0 = Smooth2D(X0, Y0);
            float N1 = Smooth2D(X1, Y0);
            float N2 = Smooth2D(X0, Y1);
            float N3 = Smooth2D(X1, Y1);
            float ix0 = Interpolate(N0, N1, SX, Interpolation);
            float ix1 = Interpolate(N2, N3, SX, Interpolation);
            return Interpolate(ix0, ix1, SY, Interpolation);
        }

        public float Interpolated3D(float X, float Y, float Z)
        {
            int X0 = FKMathFuncs.Floor(X);
            int X1 = X0 + 1;
            int Y0 = FKMathFuncs.Floor(Y);
            int Y1 = Y0 + 1;
            int Z0 = FKMathFuncs.Floor(Z);
            int Z1 = Z0 + 1;

            //Interpolation weights
            float SX = X - (float)X0;
            float SY = Y - (float)Y0;
            float SZ = Z - (float)Z0;

            //Interpolate
            float N0 = Smooth3D(X0, Y0, Z0);
            float N1 = Smooth3D(X1, Y0, Z0);
            float N2 = Smooth3D(X0, Y1, Z0);
            float N3 = Smooth3D(X1, Y1, Z0);
            float N4 = Smooth3D(X0, Y0, Z1);
            float N5 = Smooth3D(X1, Y0, Z1);
            float N6 = Smooth3D(X0, Y1, Z1);
            float N7 = Smooth3D(X1, Y1, Z1);
            float ix0 = Interpolate(N0, N1, SX, Interpolation);
            float ix1 = Interpolate(N2, N3, SX, Interpolation);
            float ix2 = Interpolate(N4, N5, SX, Interpolation);
            float ix3 = Interpolate(N6, N7, SX, Interpolation);
            float iy0 = Interpolate(ix0, ix1, SY, Interpolation);
            float iy1 = Interpolate(ix2, ix3, SY, Interpolation);
            return Interpolate(iy0, iy1, SZ, Interpolation);
        }
    }
}