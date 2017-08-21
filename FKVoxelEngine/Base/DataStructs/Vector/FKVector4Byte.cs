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
// Create Time         :    2017/7/24 17:39:15
// Update Time         :    2017/7/24 17:39:15
// Class Version       :    v1.0.0.0
// Class Description   :    
// ===============================================================================
using System;
// ===============================================================================
namespace FKVoxelEngine.Base
{
    public struct FKVector4Byte
    {
        public byte X;
        public byte Y;
        public byte Z;
        public byte W;

        public FKVector4Byte(byte x, byte y, byte z, byte w)
        {
            this.X = x;
            this.Y = y;
            this.Z = z;
            this.W = w;
        }

        public FKVector4Byte(byte x, byte y, byte z)
        {
            this.X = x;
            this.Y = y;
            this.Z = z;
            this.W = 1;
        }

        public FKVector4Byte(int x, int y, int z)
        {
            this.X = (byte)x;
            this.Y = (byte)y;
            this.Z = (byte)z;
            this.W = 1;
        }

        public FKVector4Byte(byte value)
        {
            this.X = this.Y = this.Z = value;
            this.W = 1;
        }


        public static bool operator ==(FKVector4Byte left, FKVector4Byte right)
        {
            return left.Equals(right);
        }

        public static bool operator !=(FKVector4Byte left, FKVector4Byte right)
        {
            return !left.Equals(right);
        }

        public static FKVector4Byte operator +(FKVector4Byte a, FKVector4Byte b)
        {
            FKVector4Byte result;

            result.X = (byte)(a.X + b.X);
            result.Y = (byte)(a.Y + b.Y);
            result.Z = (byte)(a.Z + b.Z);
            result.W = 1;

            return result;
        }

        public static FKVector4Byte operator -(FKVector4Byte a, FKVector4Byte b)
        {
            FKVector4Byte result;

            result.X = (byte)(a.X - b.X);
            result.Y = (byte)(a.Y - b.Y);
            result.Z = (byte)(a.Z - b.Z);
            result.W = 1;

            return result;
        }

        public static FKVector4Byte operator *(FKVector4Byte a, FKVector4Byte b)
        {
            FKVector4Byte result;

            result.X = (byte)(a.X * b.X);
            result.Y = (byte)(a.Y * b.Y);
            result.Z = (byte)(a.Z * b.Z);
            result.W = 1;

            return result;
        }

        public static FKVector4Byte operator /(FKVector4Byte a, FKVector4Byte b)
        {
            FKVector4Byte result;

            result.X = (byte)(a.X / b.X);
            result.Y = (byte)(a.Y / b.Y);
            result.Z = (byte)(a.Z / b.Z);
            result.W = 1;

            return result;
        }

        public bool Equals(FKVector4Byte other)
        {
            return (((this.X == other.X) && (this.Y == other.Y)) &&
                    (this.Z == other.Z) && (this.W == other.W));
        }

        public override bool Equals(object obj)
        {
            bool flag = false;

            if (obj is FKVector4Byte)
            {
                flag = this.Equals((FKVector4Byte)obj);
            }

            return flag;
        }

        public override int GetHashCode()
        {
            unchecked
            {
                return this.X.GetHashCode() + this.Y.GetHashCode() +
                       this.Z.GetHashCode() + this.W.GetHashCode();
            }
        }

        public override string ToString()
        {
            return String.Format("{{X:{0} Y:{1} Z:{2} W:{3}}}", X, Y, Z, W);
        }
    }
}