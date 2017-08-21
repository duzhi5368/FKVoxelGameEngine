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
// Create Time         :    2017/7/21 18:01:49
// Update Time         :    2017/7/21 18:01:49
// Class Version       :    v1.0.0.0
// Class Description   :    
// ===============================================================================
using System;
using System.Globalization;
using System.Text;
// ===============================================================================
namespace FKVoxelEngine.Base
{
    /// <summary>
    /// stringBuilder添加数字的方式选项
    /// </summary>
    [Flags]
    public enum AppendNumberOptions
    {
        eNormal = 0,        // 普通格式
        ePositiveSign = 1,  // 加符号，例如正数前面添加 '+'
        eNumberGroup = 2,   // 使用数字组分割，每三个数字会添加一个 ',' 做分割
    }
    public static class FKStringBuilderExtensions
    {
        #region ==== 静态变量 ====

        static int[] numberGroupSizes = CultureInfo.CurrentCulture.NumberFormat.NumberGroupSizes;
        static char[] numberString = new char[32];      // 数字格式化转换的静态缓冲区

        #endregion

        #region ==== 外部接口 ====

        public static void AppendNumber(this StringBuilder builder, int number)
        {
            AppendNumbernternal(builder, number, 0, AppendNumberOptions.eNormal);
        }

        public static void AppendNumber(this StringBuilder builder, int number,
                                                            AppendNumberOptions options)
        {
            AppendNumbernternal(builder, number, 0, options);
        }

        public static void AppendNumber(this StringBuilder builder, float number)
        {
            AppendNumber(builder, number, 2, AppendNumberOptions.eNormal);
        }

        public static void AppendNumber(this StringBuilder builder, float number,
                                                            AppendNumberOptions options)
        {
            AppendNumber(builder, number, 2, options);
        }

        public static void AppendNumber(this StringBuilder builder, float number,
                                        int decimalCount, AppendNumberOptions options)
        {
            if (float.IsNaN(number))
            {
                builder.Append("NaN");
            }
            else if (float.IsNegativeInfinity(number))
            {
                builder.Append("-Infinity");
            }
            else if (float.IsPositiveInfinity(number))
            {
                builder.Append("+Infinity");
            }
            else
            {
                int intNumber =
                        (int)(number * (float)System.Math.Pow(10, decimalCount) + 0.5f);

                AppendNumbernternal(builder, intNumber, decimalCount, options);
            }
        }

        #endregion ==== 外部接口 ====

        #region ==== 内部函数 ====

        private static void AppendNumbernternal(StringBuilder builder, int number,
                                        int decimalCount, AppendNumberOptions options)
        {
            NumberFormatInfo nfi = CultureInfo.CurrentCulture.NumberFormat;

            int idx = numberString.Length;
            int decimalPos = idx - decimalCount;

            if (decimalPos == idx)
                decimalPos = idx + 1;

            int numberGroupIdx = 0;
            int numberGroupCount = numberGroupSizes[numberGroupIdx] + decimalCount;

            bool showNumberGroup = (options & AppendNumberOptions.eNumberGroup) != 0;
            bool showPositiveSign = (options & AppendNumberOptions.ePositiveSign) != 0;

            bool isNegative = number < 0;
            number = System.Math.Abs(number);

            do
            {
                if (idx == decimalPos)
                {
                    numberString[--idx] = nfi.NumberDecimalSeparator[0];
                }

                if (--numberGroupCount < 0 && showNumberGroup)
                {
                    numberString[--idx] = nfi.NumberGroupSeparator[0];

                    if (numberGroupIdx < numberGroupSizes.Length - 1)
                        numberGroupIdx++;

                    numberGroupCount = numberGroupSizes[numberGroupIdx] - 1;
                }

                numberString[--idx] = (char)('0' + (number % 10));
                number /= 10;

            } while (number > 0 || decimalPos <= idx);

            if (isNegative)
            {
                numberString[--idx] = nfi.NegativeSign[0];
            }
            else if (showPositiveSign)
            {
                numberString[--idx] = nfi.PositiveSign[0];
            }

            builder.Append(numberString, idx, numberString.Length - idx);
        }

        #endregion ==== 内部函数 ====
    }
}