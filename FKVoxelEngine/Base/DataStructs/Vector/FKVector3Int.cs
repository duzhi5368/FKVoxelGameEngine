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
// Create Time         :    2017/7/24 17:38:13
// Update Time         :    2017/7/24 17:38:13
// Class Version       :    v1.0.0.0
// Class Description   :    
// ===============================================================================
using Microsoft.Xna.Framework;
using System;
// ===============================================================================
namespace FKVoxelEngine.Base
{
    public class FKVector3Int
    {
        public int X;
        public int Y;
        public int Z;

        public FKVector3Int(int x, int y, int z)
        {
            this.X = x;
            this.Y = y;
            this.Z = z;
        }

        public FKVector3Int(Vector3 vector)
        {
            this.X = (int)vector.X;
            this.Y = (int)vector.Y;
            this.Z = (int)vector.Z;
        }

        public override bool Equals(object obj)
        {
            if (obj is FKVector3Int) 
                return this.Equals((FKVector3Int)obj);
            else
                return false;
        }

        public bool Equals(FKVector3Int other)
        {
            return (((this.X == other.X) && (this.Y == other.Y)) && (this.Z == other.Z));
        }

        public static bool operator ==(FKVector3Int value1, FKVector3Int value2)
        {
            return (((value1.X == value2.X) && (value1.Y == value2.Y)) && (value1.Z == value2.Z));
        }

        public static bool operator !=(FKVector3Int value1, FKVector3Int value2)
        {
            if ((value1.X == value2.X) && (value1.Y == value2.Y)) return value1.Z != value2.Z;
            return true;
        }

        public override int GetHashCode()
        {
            return ((this.X.GetHashCode() + this.Y.GetHashCode()) + this.Z.GetHashCode());
        }

        public override string ToString()
        {
            return string.Format("{{X:{0} Y:{1} Z:{2}}}", this.X, this.Y, this.Z);
        }

        public Vector3 AsVector3()
        {
            return new Vector3(X, Y, Z);
        }
    }
}