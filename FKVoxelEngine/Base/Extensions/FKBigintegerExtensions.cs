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
// Create Time         :    2017/7/21 17:51:50
// Update Time         :    2017/7/21 17:51:50
// Class Version       :    v1.0.0.0
// Class Description   :    
// ===============================================================================
using System;
using System.Numerics;
// ===============================================================================
namespace FKVoxelEngine.Base
{
    public static class FKBigintegerExtensions
    {
        public static BigInteger ToBigInteger(this byte[] src)
        {
            var dst = new byte[src.Length + 1];
            Array.Copy(src, dst, src.Length);
            return new BigInteger(dst);
        }

        public static byte[] ToArray(this BigInteger b)
        {
            var result = b.ToByteArray();
            if (result[result.Length - 1] == 0 && (result.Length % 0x10) != 0)
                Array.Resize(ref result, result.Length - 1);
            return result;
        }

        public static byte[] ToArray(this BigInteger b, int size)
        {
            byte[] result = b.ToArray();
            if (result.Length > size)
                throw new ArgumentOutOfRangeException("size", size, "must be large enough to convert the BigInteger");

            if (result.Length == size)
                return result;

            int n = size - result.Length;
            Array.Resize(ref result, size);

            while (n > 0)
            {
                result[size - n] = 0x00;
                n--;
            }

            return result;
        }
    }
}