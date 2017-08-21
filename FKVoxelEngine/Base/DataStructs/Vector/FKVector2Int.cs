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
// Create Time         :    2017/7/24 17:37:39
// Update Time         :    2017/7/24 17:37:39
// Class Version       :    v1.0.0.0
// Class Description   :    
// ===============================================================================
using System;
// ===============================================================================
namespace FKVoxelEngine.Base
{
    public class FKVector2Int
    {
        public int X;
        public int Z;

        public FKVector2Int(int x, int z)
        {
            this.X = x;
            this.Z = z;
        }

        public override bool Equals(object obj)
        {
            if (obj is FKVector2Int) return this.Equals((FKVector2Int)obj);
            else return false;
        }

        public bool Equals(FKVector2Int other)
        {
            return ((this.X == other.X) && (this.Z == other.Z));
        }

        public static bool operator ==(FKVector2Int value1, FKVector2Int value2)
        {
            return ((value1.X == value2.X) && (value1.Z == value2.Z));
        }

        public static bool operator !=(FKVector2Int value1, FKVector2Int value2)
        {
            if (value1.X == value2.X) return value1.Z != value2.Z;
            return true;
        }

        public override int GetHashCode()
        {
            return (this.X.GetHashCode() + this.Z.GetHashCode());
        }

        public override string ToString()
        {
            return string.Format("{{X:{0} Z:{1}}}", this.X, this.Z);
        }
    }
}