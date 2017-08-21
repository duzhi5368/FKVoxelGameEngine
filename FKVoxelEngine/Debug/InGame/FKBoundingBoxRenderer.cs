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
// Create Time         :    2017/7/31 19:19:38
// Update Time         :    2017/7/31 19:19:38
// Class Version       :    v1.0.0.0
// Class Description   :    
// ===============================================================================
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
// ===============================================================================
namespace FKVoxelEngine.Debug
{
    public static class FKBoundingBoxRenderer
    {
        private static VertexPositionColor[] verts = new VertexPositionColor[8];

        private static int[] indices = new int[]
                                           {
                                               0, 1,
                                               1, 2,
                                               2, 3,
                                               3, 0,
                                               0, 4,
                                               1, 5,
                                               2, 6,
                                               3, 7,
                                               4, 5,
                                               5, 6,
                                               6, 7,
                                               7, 4,
                                           }; 

        private static BasicEffect effect;


        public static void Render(BoundingBox box, GraphicsDevice gd, Matrix view, Matrix proj, Color color)
        {
            if(effect == null)
            {
                effect = new BasicEffect(gd) { TextureEnabled = false, VertexColorEnabled = true, LightingEnabled = false };
            }

            Vector3[] corners = box.GetCorners();
            for(int i = 0; i < 8; ++i)
            {
                verts[i].Position = corners[i];
                verts[i].Color = color;
            }
            effect.View = view;
            effect.Projection = proj;
            foreach(EffectPass pass in effect.CurrentTechnique.Passes)
            {
                pass.Apply();
                gd.DrawUserIndexedPrimitives(PrimitiveType.LineList, verts, 0, 8, indices, 0, indices.Length / 2);
            }
        }
    }
}