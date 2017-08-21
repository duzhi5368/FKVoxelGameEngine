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
// Create Time         :    2017/7/26 11:57:53
// Update Time         :    2017/7/26 11:57:53
// Class Version       :    v1.0.0.0
// Class Description   :    
// ===============================================================================
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Graphics.PackedVector;
using System;
// ===============================================================================
namespace FKVoxelEngine.RenderObj
{
    [Serializable]
    public struct FKBlockVertex : IVertexType
    {
        private Vector3         _Position;
        private HalfVector2     _TextureCoordinate;
        private float           _SunLight;

        private static readonly VertexDeclaration FKBlockVertexDeclaration = new VertexDeclaration(
            new []
            {
                new VertexElement(0, VertexElementFormat.Vector3, VertexElementUsage.Position, 0),
                new VertexElement(sizeof (float)*3,VertexElementFormat.HalfVector2, VertexElementUsage.TextureCoordinate,0),
                new VertexElement(sizeof (float)*4,VertexElementFormat.Single,VertexElementUsage.Color, 0),
            });


        public FKBlockVertex(Vector3 position, HalfVector2 textureCoordinate, float sunLight)
        {
            _Position = position;
            _TextureCoordinate = textureCoordinate;
            _SunLight = sunLight;
        }

        public VertexDeclaration VertexDeclaration
        {
            get
            {
                return FKBlockVertexDeclaration;
            }
        }

        public Vector3 Position
        {
            get { return _Position; }
            set { _Position = value; }
        }

        public HalfVector2 TextureCoordinate
        {
            get { return _TextureCoordinate; }
            set { _TextureCoordinate = value; }
        }

        public float SunLight
        {
            get { return _SunLight; }
            set { _SunLight = value; }
        }

        public static int SizeInBytes
        {
            get { return sizeof(float) * 5; }
        }
    }
}