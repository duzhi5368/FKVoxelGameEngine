﻿/* 
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
// Create Time         :    2017/8/7 14:10:45
// Update Time         :    2017/8/7 14:10:45
// Class Version       :    v1.0.0.0
// Class Description   :    
// ===============================================================================
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
// ===============================================================================
namespace FKVoxelEngine.Graphics
{
    public static class FKBasicShapesHelper
    { 
        public static void DrawSolidPolygon(FKPrimitiveBatch primitiveBatch, Vector2[] vertices, int count, float red, float green, float blue)
        {
            DrawSolidPolygon(primitiveBatch, vertices, count, new Color(red, green, blue), true);
        }

        public static void DrawSolidPolygon(FKPrimitiveBatch primitiveBatch, Vector2[] vertices, int count, Color color)
        {
            DrawSolidPolygon(primitiveBatch, vertices, count, color, true);
        }

        public static void DrawSolidPolygon(FKPrimitiveBatch primitiveBatch, Vector2[] vertices, int count, Color color, bool outline)
        {
            if (!primitiveBatch.IsReady())
                throw new InvalidOperationException("BeginCustomDraw must be called before drawing anything.");

            if (count == 2)
            {
                DrawPolygon(primitiveBatch, vertices, count, color);
                return;
            }

            Color colorFill = color * (outline ? 0.5f : 1.0f);

            for (int i = 1; i < count - 1; i++)
            {
                primitiveBatch.AddVertex(vertices[0], colorFill, PrimitiveType.TriangleList);
                primitiveBatch.AddVertex(vertices[i], colorFill, PrimitiveType.TriangleList);
                primitiveBatch.AddVertex(vertices[i + 1], colorFill, PrimitiveType.TriangleList);
            }

            if (outline)
            {
                DrawPolygon(primitiveBatch, vertices, count, color);
            }
        }

        public static void DrawPolygon(FKPrimitiveBatch primitiveBatch, Vector2[] vertices, int count, float red, float green, float blue)
        {
            DrawPolygon(primitiveBatch, vertices, count, new Color(red, green, blue));
        }

        public static void DrawPolygon(FKPrimitiveBatch primitiveBatch, Vector2[] vertices, int count, Color color)
        {
            if (!primitiveBatch.IsReady())
                throw new InvalidOperationException("BeginCustomDraw must be called before drawing anything.");

            for (int i = 0; i < count - 1; i++)
            {
                primitiveBatch.AddVertex(vertices[i], color, PrimitiveType.LineList);
                primitiveBatch.AddVertex(vertices[i + 1], color, PrimitiveType.LineList);
            }

            primitiveBatch.AddVertex(vertices[count - 1], color, PrimitiveType.LineList);
            primitiveBatch.AddVertex(vertices[0], color, PrimitiveType.LineList);
        }

        public static void DrawSegment(FKPrimitiveBatch primitiveBatch, Vector2 start, Vector2 end, float red, float green, float blue)
        {
            DrawSegment(primitiveBatch, start, end, new Color(red, green, blue));
        }

        public static void DrawSegment(FKPrimitiveBatch primitiveBatch, Vector2 start, Vector2 end, Color color)
        {
            if (!primitiveBatch.IsReady())
                throw new InvalidOperationException("BeginCustomDraw must be called before drawing anything.");

            primitiveBatch.AddVertex(start, color, PrimitiveType.LineList);
            primitiveBatch.AddVertex(end, color, PrimitiveType.LineList);
        }
    }
}