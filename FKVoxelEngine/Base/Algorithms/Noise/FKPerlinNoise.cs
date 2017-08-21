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
// Create Time         :    2017/7/24 17:15:53
// Update Time         :    2017/7/24 17:15:53
// Class Version       :    v1.0.0.0
// Class Description   :    
// ===============================================================================
using System;
// ===============================================================================
namespace FKVoxelEngine.Base
{
    /// <summary>
    /// 柏林噪音
    /// http://students.vassar.edu/mazucker/code/perlin-noise-math-faq.html
    /// http://freespace.virgin.net/hugo.elias/models/m_perlin.htm
    /// </summary>
    public class FKPerlinNoise : FKNoiseGen
    {
        #region ==== 成员变量 ====

        private const int GradientTableSize = 256;
        private readonly Random _Random;
        private readonly double[] _GradientTable = new double[GradientTableSize * 3];
        private readonly byte[] _PermTable = new byte[] {
              225,155,210,108,175,199,221,144,203,116, 70,213, 69,158, 33,252,
                5, 82,173,133,222,139,174, 27,  9, 71, 90,246, 75,130, 91,191,
              169,138,  2,151,194,235, 81,  7, 25,113,228,159,205,253,134,142,
              248, 65,224,217, 22,121,229, 63, 89,103, 96,104,156, 17,201,129,
               36,  8,165,110,237,117,231, 56,132,211,152, 20,181,111,239,218,
              170,163, 51,172,157, 47, 80,212,176,250, 87, 49, 99,242,136,189,
              162,115, 44, 43,124, 94,150, 16,141,247, 32, 10,198,223,255, 72,
               53,131, 84, 57,220,197, 58, 50,208, 11,241, 28,  3,192, 62,202,
               18,215,153, 24, 76, 41, 15,179, 39, 46, 55,  6,128,167, 23,188,
              106, 34,187,140,164, 73,112,182,244,195,227, 13, 35, 77,196,185,
               26,200,226,119, 31,123,168,125,249, 68,183,230,177,135,160,180,
               12,  1,243,148,102,166, 38,238,251, 37,240,126, 64, 74,161, 40,
              184,149,171,178,101, 66, 29, 59,146, 61,254,107, 42, 86,154,  4,
              236,232,120, 21,233,209, 45, 98,193,114, 78, 19,206, 14,118,127,
               48, 79,147, 85, 30,207,219, 54, 88,234,190,122, 95, 67,143,109,
              137,214,145, 93, 92,100,245,  0,216,186, 60, 83,105, 97,204, 52};

        #endregion ==== 成员变量 ====

        #region ==== 对外接口 ====

        public FKPerlinNoise(int nSeed)
        {
            _Random = new Random(nSeed);
            InitGradients();
        }

        public override float Noise2D(float X, float Y)
        {
            var N = ((int)X * 1619 + (int)Y * 31337 * 1013 * _Random.Next()) & 0x7fffffff;
            N = (N << 13) ^ N;
            return (float)(1.0 - ((N * (N * N * 15731 + 789221) + 1376312589) & 0x7fffffff) / 1073741824.0);
        }
        public double Noise3D(double x, double y, double z)
        {
            int ix = (int)System.Math.Floor(x);
            double fx0 = x - ix;
            double fx1 = fx0 - 1;
            double wx = FKMathFuncs.Smooth(fx0);

            int iy = (int)System.Math.Floor(y);
            double fy0 = y - iy;
            double fy1 = fy0 - 1;
            double wy = FKMathFuncs.Smooth(fy0);

            int iz = (int)System.Math.Floor(z);
            double fz0 = z - iz;
            double fz1 = fz0 - 1;
            double wz = FKMathFuncs.Smooth(fz0);

            double vx0 = Lattice(ix, iy, iz, fx0, fy0, fz0);
            double vx1 = Lattice(ix + 1, iy, iz, fx1, fy0, fz0);
            double vy0 = FKMathFuncs.Lerp(wx, vx0, vx1);

            vx0 = Lattice(ix, iy + 1, iz, fx0, fy1, fz0);
            vx1 = Lattice(ix + 1, iy + 1, iz, fx1, fy1, fz0);
            double vy1 = FKMathFuncs.Lerp(wx, vx0, vx1);

            double vz0 = FKMathFuncs.Lerp(wy, vy0, vy1);

            vx0 = Lattice(ix, iy, iz + 1, fx0, fy0, fz1);
            vx1 = Lattice(ix + 1, iy, iz + 1, fx1, fy0, fz1);
            vy0 = FKMathFuncs.Lerp(wx, vx0, vx1);

            vx0 = Lattice(ix, iy + 1, iz + 1, fx0, fy1, fz1);
            vx1 = Lattice(ix + 1, iy + 1, iz + 1, fx1, fy1, fz1);
            vy1 = FKMathFuncs.Lerp(wx, vx0, vx1);

            double vz1 = FKMathFuncs.Lerp(wy, vy0, vy1);
            return FKMathFuncs.Lerp(wz, vz0, vz1);
        }
        public override float Noise3D(float x, float y, float z)
        {
            return (float)Noise3D((double)x, (double)y, (double)z);
        }

        #endregion ==== 对外接口 ====

        #region ==== 内部函数 ====

        /// <summary>
        /// 初始化梯度表
        /// </summary>
        private void InitGradients()
        {
            for (int i = 0; i < GradientTableSize; i++)
            {
                double z = 1f - 2f * _Random.NextDouble();
                double r = Math.Sqrt(1f - z * z);
                double theta = 2 * Math.PI * _Random.NextDouble();
                _GradientTable[i * 3] = r * Math.Cos(theta);
                _GradientTable[i * 3 + 1] = r * Math.Sin(theta);
                _GradientTable[i * 3 + 2] = z;
            }
        }
        /// <summary>
        /// 重排列
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        private int Permutate(int x)
        {
            const int mask = GradientTableSize - 1;
            return _PermTable[x & mask];
        }
        /// <summary>
        /// 转换 x,y,z 三元组为单个梯度表索引
        /// </summary>
        /// <param name="ix"></param>
        /// <param name="iy"></param>
        /// <param name="iz"></param>
        /// <returns></returns>
        private int Index(int ix, int iy, int iz)
        {
            return Permutate(ix + Permutate(iy + Permutate(iz)));
        }
        /// <summary>
        /// 使用[ix. iy, iz]查找一个随机梯度后，乘以[fx, fy, fz]向量
        /// </summary>
        /// <param name="ix"></param>
        /// <param name="iy"></param>
        /// <param name="iz"></param>
        /// <param name="fx"></param>
        /// <param name="fy"></param>
        /// <param name="fz"></param>
        /// <returns></returns>
        private double Lattice(int ix, int iy, int iz, double fx, double fy, double fz)
        {
            int index = Index(ix, iy, iz);
            int g = index * 3;
            return _GradientTable[g] * fx + _GradientTable[g + 1] * fy + _GradientTable[g + 2] * fz;
        }

        #endregion ==== 内部函数 ====
    }
}