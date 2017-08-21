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
// Create Time         :    2017/7/26 14:17:22
// Update Time         :    2017/7/26 14:17:22
// Class Version       :    v1.0.0.0
// Class Description   :    
// ===============================================================================
using FKVoxelEngine.Base;
using FKVoxelEngine.Debug;
using FKVoxelEngine.Framework;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
// ===============================================================================
namespace FKVoxelEngine.RenderObj
{
    public sealed class FKBaseChunk : IFKInGameDebuggerable
    {
        public static byte MaxSunValue = 16;   // 最大光照强度
        public static byte WidthInBlocks = FKEngine.GetInstance.Config.Chunk.WidthInBlocks;
        public static byte MaxWidthIndexInBlocks = FKEngine.GetInstance.Config.Chunk.MaxWidthIndexInBlocks;
        public static byte LengthInBlocks = FKEngine.GetInstance.Config.Chunk.LengthInBlocks;
        public static byte MaxLengthIndexInBlocks = FKEngine.GetInstance.Config.Chunk.MaxLengthIndexInBlocks;
        public static byte HeightInBlocks = FKEngine.GetInstance.Config.Chunk.HeightInBlocks;
        public static byte MaxHeightIndexInBlocks = FKEngine.GetInstance.Config.Chunk.MaxHeightIndexInBlocks;
        public FKVector2Int WorldPosition { get; private set; }
        public FKVector2Int RelativePosition { get; private set; }
        public BoundingBox BoundingBox { get; set; }
        public ENUM_FKBaseChunkState ChunkState { get; set; }
        public VertexBuffer VertexBuffer { get; set; }
        public IndexBuffer IndexBuffer { get; set; }
        public short Index { get; set; }
        public List<FKBlockVertex> VertexList { get; set; }
        public List<short> IndexList { get; set; }
        public byte HeightestSolidBlockOffset { get; set; }
        public byte LowestEmptyBlockOffset = HeightInBlocks;
        public bool Disposed = false;

        public FKBaseChunk(FKVector2Int relativePosition)
        {
            ChunkState = ENUM_FKBaseChunkState.eAwaitingGenerate;
            RelativePosition = relativePosition;
            VertexList = new List<FKBlockVertex>();
            IndexList = new List<short>();
            WorldPosition = new FKVector2Int(RelativePosition.X * WidthInBlocks, relativePosition.Z * LengthInBlocks);
            BoundingBox = new BoundingBox(new Vector3(WorldPosition.X, 0, WorldPosition.Z),
                new Vector3(WorldPosition.X + WidthInBlocks, HeightInBlocks, WorldPosition.Z + LengthInBlocks));
        }

        public bool IsInBounds(int x, int z)
        {
            if (x < BoundingBox.Min.X || z < BoundingBox.Min.Z
               || x >= BoundingBox.Max.X || z >= BoundingBox.Max.Z)
                return false;
            return true;
        }

        public override string ToString()
        {
            return string.Format("{0} {1}", RelativePosition, ChunkState);
        }

        public void CalulateHeightIndexes()
        {
            for(byte x = 0; x < FKBaseChunk.WidthInBlocks; ++x)
            {
                var worldPositionX = WorldPosition.X + x;
                for(byte z = 0; z < FKBaseChunk.LengthInBlocks; ++z)
                {
                    var worldPositionZ = WorldPosition.Z + z;
                    var offset = FKBaseBlockStorage.GetBlockIndexByWorldPosition(worldPositionX, worldPositionZ);
                    for(int y = FKBaseChunk.MaxHeightIndexInBlocks; y >= 0; y--)
                    {
                        if((y > HeightestSolidBlockOffset) && (FKBaseBlockStorage.Blocks[offset + y].Exists))
                        {
                            HeightestSolidBlockOffset = (byte)y;
                        }
                        else if((LowestEmptyBlockOffset > y) && (!FKBaseBlockStorage.Blocks[offset + y].Exists))
                        {
                            LowestEmptyBlockOffset = (byte)y;
                        }
                    }
                }
            }

            this.LowestEmptyBlockOffset--;
        }

        public void DrawInGameDebugVisual(GraphicsDevice g, IFKCameraService camera, SpriteBatch sb, SpriteFont sf)
        {
            var position = RelativePosition + " " + ChunkState;
            var positionSize = sf.MeasureString(position);
            var projected = g.Viewport.Project(Vector3.Zero, camera.Projection, camera.View,
                Matrix.CreateTranslation(new Vector3(WorldPosition.X + WidthInBlocks / 2, 
                HeightestSolidBlockOffset - 1,
                WorldPosition.Z + LengthInBlocks / 2)));
            sb.DrawString(sf, position, new Vector2(projected.X - positionSize.X / 2, projected.Y - positionSize.Y / 2), Color.Yellow);
            FKBoundingBoxRenderer.Render(BoundingBox, g, camera.View, camera.Projection, Color.DarkRed);
        }

        private void Dispose(bool disposing)
        {
            if (Disposed)
                return;

            if (disposing)
            {
                IndexList.Clear();
                IndexList = null;
                VertexList.Clear();
                VertexList = null;

                if (VertexBuffer != null)
                    VertexBuffer.Dispose();
                if (IndexBuffer != null)
                    IndexBuffer.Dispose();
            }

            Disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        public void Clear()
        {
            VertexList.Clear();
            IndexList.Clear();
            Index = 0;
        }

        ~FKBaseChunk()
        {
            Dispose(false);
        }

        public enum ENUM_Edges
        {
            XDecreasing = 0,
            XIncreasing = 1,
            ZDecreasing = 2,
            ZIncreasing = 3,
        }
    }
}