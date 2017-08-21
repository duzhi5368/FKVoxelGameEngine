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
// Create Time         :    2017/8/2 18:59:22
// Update Time         :    2017/8/2 18:59:22
// Class Version       :    v1.0.0.0
// Class Description   :    
// ===============================================================================
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
// ===============================================================================
namespace FKVoxelEngine.Graphics
{
    public class FKPrimitiveBatch : IDisposable
    {
        private const int DefaultBufferSize = 500;
        private BasicEffect _BaseEffect;
        private GraphicsDevice _Device;
        private bool _HasBegun;
        private VertexPositionColor[] _LineVertices;
        private VertexPositionColor[] _TriangleVertices;
        private int _LineVerticesCount;
        private int _TriangleVerticesCount;
        private bool _IsDisposed;

        public FKPrimitiveBatch(GraphicsDevice gd, int bufferSize)
        {
            if (gd == null)
                throw new ArgumentNullException("GraphicsDevice");

            _Device = gd;
            _TriangleVertices = new VertexPositionColor[bufferSize - bufferSize % 3];
            _LineVertices = new VertexPositionColor[bufferSize - bufferSize % 2];
            _BaseEffect = new BasicEffect(gd) { VertexColorEnabled = true };
        }

        public void Begin(Matrix projection, Matrix view)
        {
            if (_HasBegun)
                throw new InvalidOperationException("End() must be called before Begin() can be called again.");

            _BaseEffect.Projection = projection;
            _BaseEffect.View = view;
            _BaseEffect.CurrentTechnique.Passes[0].Apply();

            _HasBegun = true;
        }

        public bool IsReady()
        {
            return _HasBegun;
        }

        public void AddVertex(Vector2 vertex, Color color, PrimitiveType primitiveType)
        {
            if (!_HasBegun)
                throw new InvalidOperationException("Begin() must be called before AddVertex can be called.");
            //if (primitiveType != PrimitiveType.TriangleList && primitiveType != PrimitiveType.LineStrip)
            //    throw new NotSupportedException("The spectified primitive type is not supported by Primitive batch.");
            if (primitiveType == PrimitiveType.LineStrip || primitiveType == PrimitiveType.TriangleStrip)
                throw new NotSupportedException("The specified primitiveType is not supported by PrimitiveBatch.");

            if (primitiveType == PrimitiveType.TriangleList)
            {
                if (_TriangleVerticesCount >= _TriangleVertices.Length)
                    FlushTriangles();

                _TriangleVertices[_TriangleVerticesCount].Position = new Vector3(vertex, -0.1f);
                _TriangleVertices[_TriangleVerticesCount].Color = color;
                _TriangleVerticesCount++;
            }

            if (primitiveType == PrimitiveType.LineList)
            {
                if (_LineVerticesCount >= _LineVertices.Length)
                    FlushLines();

                _LineVertices[_LineVerticesCount].Position = new Vector3(vertex, -0.0f);
                _LineVertices[_LineVerticesCount].Color = color;
                _LineVerticesCount++;
            }
        }

        public void End()
        {
            if (!_HasBegun)
                throw new InvalidOperationException("Begin() must be called before End() can be called.");

            FlushTriangles();
            FlushLines();

            _HasBegun = false;
        }

        private void FlushTriangles()
        {
            if (!_HasBegun)
                throw new InvalidOperationException("Begin() must be called before Flush() can be called.");

            if(_TriangleVerticesCount >= 3)
            {
                int primitiveCount = _TriangleVerticesCount / 3;

                _Device.SamplerStates[0] = SamplerState.AnisotropicClamp;
                _Device.DrawUserPrimitives(PrimitiveType.TriangleList, _TriangleVertices, 0, primitiveCount);
                _TriangleVerticesCount -= primitiveCount * 3;
            }
        }

        private void FlushLines()
        {
            if (!_HasBegun)
                throw new InvalidOperationException("Begin() must be called before Flush() can be called.");

            if(_LineVerticesCount >= 2)
            {
                int primitiveCount = _LineVerticesCount / 2;

                _Device.SamplerStates[0] = SamplerState.AnisotropicClamp;
                _Device.DrawUserPrimitives(PrimitiveType.LineList, _LineVertices, 0, primitiveCount);
                _LineVerticesCount -= primitiveCount * 2;
            }
        }

        protected virtual void Dispose(bool disposing)
        {
            if(disposing && !_IsDisposed)
            {
                if (_BaseEffect != null)
                    _BaseEffect.Dispose();

                _IsDisposed = true;
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}