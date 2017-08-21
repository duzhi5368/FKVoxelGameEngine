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
// Create Time         :    2017/7/24 11:51:22
// Update Time         :    2017/7/24 11:51:22
// Class Version       :    v1.0.0.0
// Class Description   :    
// ===============================================================================
using System;
// ===============================================================================
namespace FKVoxelEngine.Base
{
    public static class FKMathFuncs
    {
        /// <summary>
        /// 取正
        /// </summary>
        /// <param name="i"></param>
        /// <returns></returns>
        public static int Abs(int i)
        {
            return (i >= 0) ? i : -i;
        }
        /// <summary>
        /// 向下取整
        /// </summary>
        /// <param name="d"></param>
        /// <returns></returns>
        public static double Floor(double d)
        {
            int i = (int)d;
            return (d < 0 && d != i) ? i - 1 : i;
        }
        /// <summary>
        /// 向下取整
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        public static int Floor(float x)
        {
            return x > 0 ? (int)x : (int)x - 1;
        }
        /// <summary>
        /// 点乘
        /// </summary>
        /// <param name="g"></param>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        public static float Dot(int[] g, float x, float y)
        {
            return g[0] * x + g[1] * y;
        }
        /// <summary>
        /// 点乘
        /// </summary>
        /// <param name="g"></param>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        public static float Dot(int[] g, float x, float y, float z)
        {
            return g[0] * x + g[1] * y + g[2] * z;
        }
        /// <summary>
        /// 点乘
        /// </summary>
        /// <param name="g"></param>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        public static float Dot(int[] g, float x, float y, float z, float w)
        {
            return g[0] * x + g[1] * y + g[2] * z + g[3] * w;
        }
        /// <summary>
        /// 标准化 区域为[0.0, 1.0]
        /// </summary>
        /// <param name="d"></param>
        /// <returns></returns>
        public static double Clamp(double d)
        {
            if (d > 1.0)
                return 1.0;
            if (d < 0.0)
                return 0.0;
            return d;
        }
        /// <summary>
        /// 平滑曲线（当噪音频率很低时，看起来不会过于不连续）
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        public static double Smooth(double x)
        {
            return x * x * (3 - 2 * x);
        }
        /// <summary>
        /// 线性差值
        /// </summary>
        /// <param name="x"></param>
        /// <param name="x1"></param>
        /// <param name="x2"></param>
        /// <param name="q00"></param>
        /// <param name="q01"></param>
        /// <returns></returns>
        public static double Lerp(double x, double x1, double x2, double q00, double q01)
        {
            return ((x2 - x) / (x2 - x1)) * q00 + ((x - x1) / (x2 - x1)) * q01;
        }
        /// <summary>
        /// 线性差值
        /// </summary>
        /// <param name="x1"></param>
        /// <param name="x2"></param>
        /// <param name="p"></param>
        /// <returns></returns>
        public static double Lerp(double x1, double x2, double p)
        {
            return x1 * (1.0 - p) + x2 * p;
        }
        /// <summary>
        /// 双线性差值
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="q11"></param>
        /// <param name="q12"></param>
        /// <param name="q21"></param>
        /// <param name="q22"></param>
        /// <param name="x1"></param>
        /// <param name="x2"></param>
        /// <param name="y1"></param>
        /// <param name="y2"></param>
        /// <returns></returns>
        public static double BiLerp(double x, double y, double q11, double q12, double q21, double q22, double x1, double x2, double y1, double y2)
        {
            double r1 = Lerp(x, x1, x2, q11, q21);
            double r2 = Lerp(x, x1, x2, q12, q22);
            return Lerp(y, y1, y2, r1, r2);
        }
        /// <summary>
        /// 三线性差值
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="z"></param>
        /// <param name="q000"></param>
        /// <param name="q001"></param>
        /// <param name="q010"></param>
        /// <param name="q011"></param>
        /// <param name="q100"></param>
        /// <param name="q101"></param>
        /// <param name="q110"></param>
        /// <param name="q111"></param>
        /// <param name="x1"></param>
        /// <param name="x2"></param>
        /// <param name="y1"></param>
        /// <param name="y2"></param>
        /// <param name="z1"></param>
        /// <param name="z2"></param>
        /// <returns></returns>
        public static double TriLerp(double x, double y, double z, double q000, double q001, double q010, double q011, double q100, double q101, double q110, double q111, double x1, double x2, double y1, double y2, double z1, double z2)
        {
            double x00 = Lerp(x, x1, x2, q000, q100);
            double x10 = Lerp(x, x1, x2, q010, q110);
            double x01 = Lerp(x, x1, x2, q001, q101);
            double x11 = Lerp(x, x1, x2, q011, q111);
            double r0 = Lerp(y, y1, y2, x00, x01);
            double r1 = Lerp(y, y1, y2, x10, x11);
            return Lerp(z, z1, z2, r0, r1);
        }
    }
}