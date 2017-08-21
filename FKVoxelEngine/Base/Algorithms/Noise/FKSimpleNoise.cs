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
// Create Time         :    2017/7/24 14:33:57
// Update Time         :    2017/8/16 15:11:57
// Class Version       :    v2.0.0.0
// Class Description   :    完全更新Noise生成机制
// ===============================================================================
using Microsoft.Xna.Framework;
using System;
// ===============================================================================
namespace FKVoxelEngine.Base
{
    // https://github.com/WardBenjamin/SimplexNoise/blob/master/SimplexNoise/Noise.cs
    public class FKSimpleNoise : FKNoiseGen
    {
        /* Old Version
         * 
        #region ==== 静态魔数 ====

        private static int[][] grad3 = {
                                           new int[] {1, 1, 0},
                                           new int[] {-1, 1, 0},
                                           new int[] {1, -1, 0},
                                           new int[] {-1, -1, 0},
                                           new int[] {1, 0, 1},
                                           new int[] {-1, 0, 1},
                                           new int[] {1, 0, -1},
                                           new int[] {-1, 0, -1},
                                           new int[] {0, 1, 1},
                                           new int[] {0, -1, 1},
                                           new int[] {0, 1, -1},
                                           new int[] {0, -1, -1}
                                       };
        private static int[][] grad4 = {
                                           new int[] {0, 1, 1, 1},
                                           new int[] {0, 1, 1, -1},
                                           new int[] {0, 1, -1, 1},
                                           new int[] {0, 1, -1, -1},
                                           new int[] {0, -1, 1, 1},
                                           new int[] {0, -1, 1, -1},
                                           new int[] {0, -1, -1, 1},
                                           new int[] {0, -1, -1, -1},
                                           new int[] {1, 0, 1, 1},
                                           new int[] {1, 0, 1, -1},
                                           new int[] {1, 0, -1, 1},
                                           new int[] {1, 0, -1, -1},
                                           new int[] {-1, 0, 1, 1},
                                           new int[] {-1, 0, 1, -1},
                                           new int[] {-1, 0, -1, 1},
                                           new int[] {-1, 0, -1, -1},
                                           new int[] {1, 1, 0, 1},
                                           new int[] {1, 1, 0, -1},
                                           new int[] {1, -1, 0, 1},
                                           new int[] {1, -1, 0, -1},
                                           new int[] {-1, 1, 0, 1},
                                           new int[] {-1, 1, 0, -1},
                                           new int[] {-1, -1, 0, 1},
                                           new int[] {-1, -1, 0, -1},
                                           new int[] {1, 1, 1, 0},
                                           new int[] {1, 1, -1, 0},
                                           new int[] {1, -1, 1, 0},
                                           new int[] {1, -1, -1, 0},
                                           new int[] {-1, 1, 1, 0},
                                           new int[] {-1, 1, -1, 0},
                                           new int[] {-1, -1, 1, 0},
                                           new int[] {-1, -1, -1, 0}
                                       };
        private static int[][] simplex = {
                                             new int[] {0, 1, 2, 3}, new int[] {0, 1, 3, 2}, new int[] {0, 0, 0, 0},
                                             new int[] {0, 2, 3, 1}, new int[] {0, 0, 0, 0}, new int[] {0, 0, 0, 0},
                                             new int[] {0, 0, 0, 0}, new int[] {1, 2, 3, 0},
                                             new int[] {0, 2, 1, 3}, new int[] {0, 0, 0, 0}, new int[] {0, 3, 1, 2},
                                             new int[] {0, 3, 2, 1}, new int[] {0, 0, 0, 0}, new int[] {0, 0, 0, 0},
                                             new int[] {0, 0, 0, 0}, new int[] {1, 3, 2, 0},
                                             new int[] {0, 0, 0, 0}, new int[] {0, 0, 0, 0}, new int[] {0, 0, 0, 0},
                                             new int[] {0, 0, 0, 0}, new int[] {0, 0, 0, 0}, new int[] {0, 0, 0, 0},
                                             new int[] {0, 0, 0, 0}, new int[] {0, 0, 0, 0},
                                             new int[] {1, 2, 0, 3}, new int[] {0, 0, 0, 0}, new int[] {1, 3, 0, 2},
                                             new int[] {0, 0, 0, 0}, new int[] {0, 0, 0, 0}, new int[] {0, 0, 0, 0},
                                             new int[] {2, 3, 0, 1}, new int[] {2, 3, 1, 0},
                                             new int[] {1, 0, 2, 3}, new int[] {1, 0, 3, 2}, new int[] {0, 0, 0, 0},
                                             new int[] {0, 0, 0, 0}, new int[] {0, 0, 0, 0}, new int[] {2, 0, 3, 1},
                                             new int[] {0, 0, 0, 0}, new int[] {2, 1, 3, 0},
                                             new int[] {0, 0, 0, 0}, new int[] {0, 0, 0, 0}, new int[] {0, 0, 0, 0},
                                             new int[] {0, 0, 0, 0}, new int[] {0, 0, 0, 0}, new int[] {0, 0, 0, 0},
                                             new int[] {0, 0, 0, 0}, new int[] {0, 0, 0, 0},
                                             new int[] {2, 0, 1, 3}, new int[] {0, 0, 0, 0}, new int[] {0, 0, 0, 0},
                                             new int[] {0, 0, 0, 0}, new int[] {3, 0, 1, 2}, new int[] {3, 0, 2, 1},
                                             new int[] {0, 0, 0, 0}, new int[] {3, 1, 2, 0},
                                             new int[] {2, 1, 0, 3}, new int[] {0, 0, 0, 0}, new int[] {0, 0, 0, 0},
                                             new int[] {0, 0, 0, 0}, new int[] {3, 1, 0, 2}, new int[] {0, 0, 0, 0},
                                             new int[] {3, 2, 0, 1}, new int[] {3, 2, 1, 0}
                                         };

        private static int[] p = {
                                     151, 160, 137, 91, 90, 15, 131, 13, 201, 95, 96, 53, 194, 233, 7, 225, 140, 36, 103
                                     , 30, 69, 142, 8, 99, 37, 240, 21, 10, 23,
                                     190, 6, 148, 247, 120, 234, 75, 0, 26, 197, 62, 94, 252, 219, 203, 117, 35, 11, 32,
                                     57, 177, 33,
                                     88, 237, 149, 56, 87, 174, 20, 125, 136, 171, 168, 68, 175, 74, 165, 71, 134, 139,
                                     48, 27, 166,
                                     77, 146, 158, 231, 83, 111, 229, 122, 60, 211, 133, 230, 220, 105, 92, 41, 55, 46,
                                     245, 40, 244,
                                     102, 143, 54, 65, 25, 63, 161, 1, 216, 80, 73, 209, 76, 132, 187, 208, 89, 18, 169,
                                     200, 196,
                                     135, 130, 116, 188, 159, 86, 164, 100, 109, 198, 173, 186, 3, 64, 52, 217, 226, 250
                                     , 124, 123,
                                     5, 202, 38, 147, 118, 126, 255, 82, 85, 212, 207, 206, 59, 227, 47, 16, 58, 17, 182
                                     , 189, 28, 42,
                                     223, 183, 170, 213, 119, 248, 152, 2, 44, 154, 163, 70, 221, 153, 101, 155, 167, 43
                                     , 172, 9,
                                     129, 22, 39, 253, 19, 98, 108, 110, 79, 113, 224, 232, 178, 185, 112, 104, 218, 246
                                     , 97, 228,
                                     251, 34, 242, 193, 238, 210, 144, 12, 191, 179, 162, 241, 81, 51, 145, 235, 249, 14
                                     , 239, 107,
                                     49, 192, 214, 31, 181, 199, 106, 157, 184, 84, 204, 176, 115, 121, 50, 45, 127, 4,
                                     150, 254,
                                     138, 236, 205, 93, 222, 114, 67, 29, 24, 72, 243, 141, 128, 195, 78, 66, 215, 61,
                                     156, 180
                                 };

        #endregion ==== 静态魔数 ====

        // 置换表
        private static int[] PermTable = new int[512];

        static FKSimpleNoise()
        {
            for (int i = 0; i < 512; i++)
                PermTable[i] = p[i & 255];
        }

        /// <summary>
        /// 简易噪音生成
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="z"></param>
        /// <returns></returns>
        public static float Noise3D(float xin, float yin, float zin)
        {
            float n0, n1, n2, n3; 
            float F3 = 1.0f / 3.0f;
            float s = (xin + yin + zin) * F3;       // 简易有效的参数
            int i = FKMathFuncs.Floor(xin + s);
            int j = FKMathFuncs.Floor(yin + s);
            int k = FKMathFuncs.Floor(zin + s);
            float G3 = 1.0f / 6.0f;                 // 简易有效的参数
            float t = (i + j + k) * G3;
            float X0 = i - t;                       // 退还单元cell原点到标准 x,y,z 空间
            float Y0 = j - t;
            float Z0 = k - t;
            float x0 = xin - X0;                    // 到单元cell原点的 x,y,z 距离
            float y0 = yin - Y0;
            float z0 = zin - Z0;

            int i1, j1, k1;
            int i2, j2, k2;      
            if (x0 >= y0)
            {
                if (y0 >= z0)
                {
                    i1 = 1;
                    j1 = 0;
                    k1 = 0;
                    i2 = 1;
                    j2 = 1;
                    k2 = 0;
                } // X Y Z 顺序 
                else if (x0 >= z0)
                {
                    i1 = 1;
                    j1 = 0;
                    k1 = 0;
                    i2 = 1;
                    j2 = 0;
                    k2 = 1;
                } // X Z Y 顺序 
                else
                {
                    i1 = 0;
                    j1 = 0;
                    k1 = 1;
                    i2 = 1;
                    j2 = 0;
                    k2 = 1;
                } // Z X Y 顺序 
            }
            else
            {
                // x0<y0 
                if (y0 < z0)
                {
                    i1 = 0;
                    j1 = 0;
                    k1 = 1;
                    i2 = 0;
                    j2 = 1;
                    k2 = 1;
                } // Z Y X 顺序 
                else if (x0 < z0)
                {
                    i1 = 0;
                    j1 = 1;
                    k1 = 0;
                    i2 = 0;
                    j2 = 1;
                    k2 = 1;
                } // Y Z X 顺序 
                else
                {
                    i1 = 0;
                    j1 = 1;
                    k1 = 0;
                    i2 = 1;
                    j2 = 1;
                    k2 = 0;
                } // Y X Z 顺序 
            }

            float x1 = x0 - i1 + G3;
            float y1 = y0 - j1 + G3;
            float z1 = z0 - k1 + G3;
            float x2 = x0 - i2 + 2.0f * G3;
            float y2 = y0 - j2 + 2.0f * G3;
            float z2 = z0 - k2 + 2.0f * G3;
            float x3 = x0 - 1.0f + 3.0f * G3;
            float y3 = y0 - 1.0f + 3.0f * G3;
            float z3 = z0 - 1.0f + 3.0f * G3;

            // 计算四个角分别的散列梯度指数
            int ii = i & 255;
            int jj = j & 255;
            int kk = k & 255;
            int gi0 = PermTable[ii + PermTable[jj + PermTable[kk]]] % 12;
            int gi1 = PermTable[ii + i1 + PermTable[jj + j1 + PermTable[kk + k1]]] % 12;
            int gi2 = PermTable[ii + i2 + PermTable[jj + j2 + PermTable[kk + k2]]] % 12;
            int gi3 = PermTable[ii + 1 + PermTable[jj + 1 + PermTable[kk + 1]]] % 12;

            // 计算四个角分别的噪音值
            float t0 = 0.6f - x0 * x0 - y0 * y0 - z0 * z0;
            if (t0 < 0) n0 = 0.0f;
            else
            {
                t0 *= t0;
                n0 = t0 * t0 * FKMathFuncs.Dot(grad3[gi0], x0, y0, z0);
            }
            float t1 = 0.6f - x1 * x1 - y1 * y1 - z1 * z1;
            if (t1 < 0) n1 = 0.0f;
            else
            {
                t1 *= t1;
                n1 = t1 * t1 * FKMathFuncs.Dot(grad3[gi1], x1, y1, z1);
            }
            float t2 = 0.6f - x2 * x2 - y2 * y2 - z2 * z2;
            if (t2 < 0) n2 = 0.0f;
            else
            {
                t2 *= t2;
                n2 = t2 * t2 * FKMathFuncs.Dot(grad3[gi2], x2, y2, z2);
            }
            float t3 = 0.6f - x3 * x3 - y3 * y3 - z3 * z3;
            if (t3 < 0) n3 = 0.0f;
            else
            {
                t3 *= t3;
                n3 = t3 * t3 * FKMathFuncs.Dot(grad3[gi3], x3, y3, z3);
            }
            // 从每个Corner获取噪音值并进行叠加
            // 最终这个值需要被缩放到 [-1, 1]之间
            return 32.0f * (n0 + n1 + n2 + n3);
        }

        /// <summary>
        /// 2D 简易噪音生成
        /// </summary>
        /// <param name="xin"></param>
        /// <param name="yin"></param>
        /// <returns></returns>
        public static float Noise2D(float xin, float yin)
        {
            float n0, n1, n2;
            float F2 = (float)(0.5 * (System.Math.Sqrt(3.0) - 1.0));
            float s = (xin + yin) * F2;     // 2D版简易有效参数
            int i = FKMathFuncs.Floor(xin + s);
            int j = FKMathFuncs.Floor(yin + s);
            float g2 = (float)((3.0 - System.Math.Sqrt(3.0)) / 6.0);
            float t = (i + j) * g2;
            float X0 = i - t;               // 退还单元cell原点到标准 x,y 空间
            float Y0 = j - t;
            float x0 = xin - X0;            // 到单元cell原点的 x,y 距离
            float y0 = yin - Y0;

            int i1, j1;
            if (x0 > y0)
            {
                i1 = 1;
                j1 = 0;
            }
            else
            {
                i1 = 0;
                j1 = 1;
            }

            float x1 = x0 - i1 + g2;
            float y1 = y0 - j1 + g2;
            float x2 = x0 - 1.0f + 2.0f * g2;
            float y2 = y0 - 1.0f + 2.0f * g2;

            int ii = i & 255;
            int jj = j & 255;
            int gi0 = PermTable[ii + PermTable[jj]] % 12;
            int gi1 = PermTable[ii + i1 + PermTable[jj + j1]] % 12;
            int gi2 = PermTable[ii + 1 + PermTable[jj + 1]] % 12;

            // 计算三个角的噪音值
            float t0 = 0.5f - x0 * x0 - y0 * y0;
            if (t0 < 0)
                n0 = 0.0f;
            else
            {
                t0 *= t0;
                n0 = t0 * t0 * FKMathFuncs.Dot(grad3[gi0], x0, y0);
            }
            float t1 = 0.5f - x1 * x1 - y1 * y1;
            if (t1 < 0)
                n1 = 0.0f;
            else
            {
                t1 *= t1;
                n1 = t1 * t1 * FKMathFuncs.Dot(grad3[gi1], x1, y1);
            }
            float t2 = 0.5f - x2 * x2 - y2 * y2;
            if (t2 < 0)
                n2 = 0.0f;
            else
            {
                t2 *= t2;
                n2 = t2 * t2 * FKMathFuncs.Dot(grad3[gi2], x2, y2);
            }
            // 从每个Corner获取噪音值并进行叠加
            // 最终这个值需要被缩放到 [-1, 1]之间
            float returnNoise = 70.0f * (n0 + n1 + n2);
            // 将总噪音值控制到 [0, 1] 范围内
            return (returnNoise + 1.0f) * 0.5f;
        }
        */

        #region ==== 对外接口 ====

        public float Noise1D(float x)
        {
            var X = FloorToInt(x) & 0xff;
            x -= Floor(x);
            var u = Fade(x);
            return Lerp(u, Grad(perm[X], x), Grad(perm[X + 1], x - 1)) * 2;
        }
        public override float Noise2D(float x, float y)
        {
            var X = FloorToInt(x) & 0xff;
            var Y = FloorToInt(y) & 0xff;
            x -= Floor(x);
            y -= Floor(y);
            var u = Fade(x);
            var v = Fade(y);
            var A = (perm[X] + Y) & 0xff;
            var B = (perm[X + 1] + Y) & 0xff;
            return Lerp(v, Lerp(u, Grad(perm[A], x, y), Grad(perm[B], x - 1, y)),
                           Lerp(u, Grad(perm[A + 1], x, y - 1), Grad(perm[B + 1], x - 1, y - 1)));
        }
        public float Noise2D(Vector2 coord)
        {
            return Noise2D(coord.X, coord.Y);
        }
        public override float Noise3D(float x, float y, float z)
        {
            var X = FloorToInt(x) & 0xff;
            var Y = FloorToInt(y) & 0xff;
            var Z = FloorToInt(z) & 0xff;
            x -= Floor(x);
            y -= Floor(y);
            z -= Floor(z);
            var u = Fade(x);
            var v = Fade(y);
            var w = Fade(z);
            var A = (perm[X] + Y) & 0xff;
            var B = (perm[X + 1] + Y) & 0xff;
            var AA = (perm[A] + Z) & 0xff;
            var BA = (perm[B] + Z) & 0xff;
            var AB = (perm[A + 1] + Z) & 0xff;
            var BB = (perm[B + 1] + Z) & 0xff;
            return Lerp(w, Lerp(v, Lerp(u, Grad(perm[AA], x, y, z), Grad(perm[BA], x - 1, y, z)),
                                   Lerp(u, Grad(perm[AB], x, y - 1, z), Grad(perm[BB], x - 1, y - 1, z))),
                           Lerp(v, Lerp(u, Grad(perm[AA + 1], x, y, z - 1), Grad(perm[BA + 1], x - 1, y, z - 1)),
                                   Lerp(u, Grad(perm[AB + 1], x, y - 1, z - 1), Grad(perm[BB + 1], x - 1, y - 1, z - 1))));
        }
        public float Noise3D(Vector3 coord)
        {
            return Noise3D(coord.X, coord.Y, coord.Z);
        }

        #endregion ==== 对外接口 ====

        #region ==== 内部函数 ====

        private static float Fade(float t)
        {
            return t * t * t * (t * (t * 6 - 15) + 10);
        }
        private static float Lerp(float t, float a, float b)
        {
            return a + t * (b - a);
        }
        private static float Grad(int hash, float x)
        {
            return (hash & 1) == 0 ? x : -x;
        }
        private static float Grad(int hash, float x, float y)
        {
            return ((hash & 1) == 0 ? x : -x) + ((hash & 2) == 0 ? y : -y);
        }
        private static float Grad(int hash, float x, float y, float z)
        {
            var h = hash & 15;
            var u = h < 8 ? x : y;
            var v = h < 4 ? y : (h == 12 || h == 14 ? x : z);
            return ((h & 1) == 0 ? u : -u) + ((h & 2) == 0 ? v : -v);
        }
        private static int FloorToInt(float f)
        {
            return (int)Math.Floor(f + 0.5);
        }
        private static float Floor(float f)
        {
            return (float)Math.Floor(f);
        }

        #endregion ==== 内部函数 ====

        #region ==== 全局常量 ====
        private static int[] perm = {
            151,160,137,91,90,15,
            131,13,201,95,96,53,194,233,7,225,140,36,103,30,69,142,8,99,37,240,21,10,23,
            190, 6,148,247,120,234,75,0,26,197,62,94,252,219,203,117,35,11,32,57,177,33,
            88,237,149,56,87,174,20,125,136,171,168, 68,175,74,165,71,134,139,48,27,166,
            77,146,158,231,83,111,229,122,60,211,133,230,220,105,92,41,55,46,245,40,244,
            102,143,54, 65,25,63,161, 1,216,80,73,209,76,132,187,208, 89,18,169,200,196,
            135,130,116,188,159,86,164,100,109,198,173,186, 3,64,52,217,226,250,124,123,
            5,202,38,147,118,126,255,82,85,212,207,206,59,227,47,16,58,17,182,189,28,42,
            223,183,170,213,119,248,152, 2,44,154,163, 70,221,153,101,155,167, 43,172,9,
            129,22,39,253, 19,98,108,110,79,113,224,232,178,185, 112,104,218,246,97,228,
            251,34,242,193,238,210,144,12,191,179,162,241, 81,51,145,235,249,14,239,107,
            49,192,214, 31,181,199,106,157,184, 84,204,176,115,121,50,45,127, 4,150,254,
            138,236,205,93,222,114,67,29,24,72,243,141,128,195,78,66,215,61,156,180,
            151
        };
        #endregion ==== 全局常量 ====
    }
}