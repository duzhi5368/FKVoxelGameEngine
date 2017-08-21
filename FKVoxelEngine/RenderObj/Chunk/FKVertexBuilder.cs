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
// Create Time         :    2017/8/8 16:19:17
// Update Time         :    2017/8/8 16:19:17
// Class Version       :    v1.0.0.0
// Class Description   :    
// ===============================================================================
using FKVoxelEngine.Base;
using FKVoxelEngine.Framework;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Graphics.PackedVector;
using System;
// ===============================================================================
namespace FKVoxelEngine.RenderObj
{
    public class FKVertexBuilder : GameComponent, IFKVertexBuilderService
    {
        private IFKBaseChunkCacheService _ChunkCache;
        private IFKBaseBlockStorageService _BlockStorage;
        private static readonly FKLogger Logger = FKLogManager.CreateLogger("FKVertexBuilder");

        public FKVertexBuilder(Game game)
            : base(game)
        {
            Game.Services.AddService(typeof(IFKVertexBuilderService), this);
        }

        public override void Initialize()
        {
            Logger.Trace("Init()");

            _ChunkCache = (IFKBaseChunkCacheService)Game.Services.GetService(typeof(IFKBaseChunkCacheService));
            _BlockStorage = (IFKBaseBlockStorageService)Game.Services.GetService(typeof(IFKBaseBlockStorageService));

            base.Initialize();
        }

        public void Build(FKBaseChunk chunk)
        {
            if (chunk.ChunkState != ENUM_FKBaseChunkState.eAwaitingBuild)
                return;

            chunk.ChunkState = ENUM_FKBaseChunkState.eBuilding;
            {
                chunk.CalulateHeightIndexes();
                chunk.BoundingBox = new BoundingBox(
                    new Vector3(chunk.WorldPosition.X, chunk.LowestEmptyBlockOffset, chunk.WorldPosition.Z),
                    new Vector3(chunk.WorldPosition.X + FKBaseChunk.WidthInBlocks, chunk.HeightestSolidBlockOffset,
                        chunk.WorldPosition.Z + FKBaseChunk.LengthInBlocks)
                    );
                BuildVertexList(chunk);
            }
            chunk.ChunkState = ENUM_FKBaseChunkState.eReady;
        }

        private void BuildVertexList(FKBaseChunk chunk)
        {
            if (chunk.Disposed)
                return;

            chunk.Clear();

            for (byte x = 0; x < FKBaseChunk.WidthInBlocks; x++)
            {
                for (byte z = 0; z < FKBaseChunk.LengthInBlocks; z++)
                {
                    int offset = FKBaseBlockStorage.GetBlockIndexByRelativePosition(chunk, x, z);

                    for (byte y = chunk.LowestEmptyBlockOffset; y <= chunk.HeightestSolidBlockOffset; y++)
                    {
                        var blockIndex = offset + y;

                        var block = FKBaseBlockStorage.Blocks[blockIndex];

                        if (block.BaseBlockType == ENUM_FKBaseBlockType.eNone)
                            continue;

                        var position = new FKVector3Int(chunk.WorldPosition.X + x, y, chunk.WorldPosition.Z + z);
                        BuildBlockVertices(chunk, blockIndex, position);
                    }
                }
            }

            var vertices = chunk.VertexList.ToArray();
            var indices = chunk.IndexList.ToArray();

            if (vertices.Length == 0 || indices.Length == 0)
                return;

            chunk.VertexBuffer = new VertexBuffer(Game.GraphicsDevice, typeof(FKBlockVertex), vertices.Length, BufferUsage.WriteOnly);
            chunk.VertexBuffer.SetData(vertices);

            chunk.IndexBuffer = new IndexBuffer(Game.GraphicsDevice, IndexElementSize.SixteenBits, indices.Length, BufferUsage.WriteOnly);
            chunk.IndexBuffer.SetData(indices);
        }


        private void BuildBlockVertices(FKBaseChunk chunk, int blockIndex, FKVector3Int worldPosition)
        {
            FKBaseBlock block = FKBaseBlockStorage.Blocks[blockIndex]; // get the block to process.

            FKBaseBlock blockTopNW, blockTopN, blockTopNE, blockTopW, blockTopM, blockTopE, blockTopSW, blockTopS, blockTopSE;
            FKBaseBlock blockMidNW, blockMidN, blockMidNE, blockMidW, /*blockMidM,*/ blockMidE, blockMidSW, blockMidS, blockMidSE;
            FKBaseBlock blockBotNW, blockBotN, blockBotNE, blockBotW, blockBotM, blockBotE, blockBotSW, blockBotS, blockBotSE;

            blockTopNW  = _BlockStorage.GetBlockAt(worldPosition.X - 1, worldPosition.Y + 1, worldPosition.Z + 1);
            blockTopN   = _BlockStorage.GetBlockAt(worldPosition.X, worldPosition.Y + 1, worldPosition.Z + 1);
            blockTopNE  = _BlockStorage.GetBlockAt(worldPosition.X + 1, worldPosition.Y + 1, worldPosition.Z + 1);
            blockTopW   = _BlockStorage.GetBlockAt(worldPosition.X - 1, worldPosition.Y + 1, worldPosition.Z);
            blockTopM   = _BlockStorage.GetBlockAt(worldPosition.X, worldPosition.Y + 1, worldPosition.Z);
            blockTopE   = _BlockStorage.GetBlockAt(worldPosition.X + 1, worldPosition.Y + 1, worldPosition.Z);
            blockTopSW  = _BlockStorage.GetBlockAt(worldPosition.X - 1, worldPosition.Y + 1, worldPosition.Z - 1);
            blockTopS   = _BlockStorage.GetBlockAt(worldPosition.X, worldPosition.Y + 1, worldPosition.Z - 1);
            blockTopSE  = _BlockStorage.GetBlockAt(worldPosition.X + 1, worldPosition.Y + 1, worldPosition.Z - 1);

            blockMidNW  = _BlockStorage.GetBlockAt(worldPosition.X - 1, worldPosition.Y, worldPosition.Z + 1);
            blockMidN   = _BlockStorage.GetBlockAt(worldPosition.X, worldPosition.Y, worldPosition.Z + 1);
            blockMidNE  = _BlockStorage.GetBlockAt(worldPosition.X + 1, worldPosition.Y, worldPosition.Z + 1);
            blockMidW   = _BlockStorage.GetBlockAt(worldPosition.X - 1, worldPosition.Y, worldPosition.Z);

            blockMidE   = _BlockStorage.GetBlockAt(worldPosition.X + 1, worldPosition.Y, worldPosition.Z);
            blockMidSW  = _BlockStorage.GetBlockAt(worldPosition.X - 1, worldPosition.Y, worldPosition.Z - 1);
            blockMidS   = _BlockStorage.GetBlockAt(worldPosition.X, worldPosition.Y, worldPosition.Z - 1);
            blockMidSE  = _BlockStorage.GetBlockAt(worldPosition.X + 1, worldPosition.Y, worldPosition.Z - 1);

            blockBotNW  = _BlockStorage.GetBlockAt(worldPosition.X - 1, worldPosition.Y - 1, worldPosition.Z + 1);
            blockBotN   = _BlockStorage.GetBlockAt(worldPosition.X, worldPosition.Y - 1, worldPosition.Z + 1);
            blockBotNE  = _BlockStorage.GetBlockAt(worldPosition.X + 1, worldPosition.Y - 1, worldPosition.Z + 1);
            blockBotW   = _BlockStorage.GetBlockAt(worldPosition.X - 1, worldPosition.Y - 1, worldPosition.Z);
            blockBotM   = _BlockStorage.GetBlockAt(worldPosition.X, worldPosition.Y - 1, worldPosition.Z);
            blockBotE   = _BlockStorage.GetBlockAt(worldPosition.X + 1, worldPosition.Y - 1, worldPosition.Z);
            blockBotSW  = _BlockStorage.GetBlockAt(worldPosition.X - 1, worldPosition.Y - 1, worldPosition.Z - 1);
            blockBotS   = _BlockStorage.GetBlockAt(worldPosition.X, worldPosition.Y - 1, worldPosition.Z - 1);
            blockBotSE  = _BlockStorage.GetBlockAt(worldPosition.X + 1, worldPosition.Y - 1, worldPosition.Z - 1);

            float sunTR, sunTL, sunBR, sunBL;
            float redTR, redTL, redBR, redBL;
            float grnTR, grnTL, grnBR, grnBL;
            float bluTR, bluTL, bluBR, bluBL;
            Color localTR, localTL, localBR, localBL;

            localTR = Color.Black;
            localTL = Color.Yellow;
            localBR = Color.Green;
            localBL = Color.Blue;

            // -X face.

            if (!blockMidW.Exists && !(block.BaseBlockType == ENUM_FKBaseBlockType.eWater && blockMidW.BaseBlockType == ENUM_FKBaseBlockType.eWater))
            {
                sunTL = (1f / FKBaseChunk.MaxSunValue) * ((blockTopNW.Sun + blockTopW.Sun + blockMidNW.Sun + blockMidW.Sun) / 4);
                sunTR = (1f / FKBaseChunk.MaxSunValue) * ((blockTopSW.Sun + blockTopW.Sun + blockMidSW.Sun + blockMidW.Sun) / 4);
                sunBL = (1f / FKBaseChunk.MaxSunValue) * ((blockBotNW.Sun + blockBotW.Sun + blockMidNW.Sun + blockMidW.Sun) / 4);
                sunBR = (1f / FKBaseChunk.MaxSunValue) * ((blockBotSW.Sun + blockBotW.Sun + blockMidSW.Sun + blockMidW.Sun) / 4);

                redTL = (1f / FKBaseChunk.MaxSunValue) * ((blockTopNW.R + blockTopW.R + blockMidNW.R + blockMidW.R) / 4);
                redTR = (1f / FKBaseChunk.MaxSunValue) * ((blockTopSW.R + blockTopW.R + blockMidSW.R + blockMidW.R) / 4);
                redBL = (1f / FKBaseChunk.MaxSunValue) * ((blockBotNW.R + blockBotW.R + blockMidNW.R + blockMidW.R) / 4);
                redBR = (1f / FKBaseChunk.MaxSunValue) * ((blockBotSW.R + blockBotW.R + blockMidSW.R + blockMidW.R) / 4);

                grnTL = (1f / FKBaseChunk.MaxSunValue) * ((blockTopNW.G + blockTopW.G + blockMidNW.G + blockMidW.G) / 4);
                grnTR = (1f / FKBaseChunk.MaxSunValue) * ((blockTopSW.G + blockTopW.G + blockMidSW.G + blockMidW.G) / 4);
                grnBL = (1f / FKBaseChunk.MaxSunValue) * ((blockBotNW.G + blockBotW.G + blockMidNW.G + blockMidW.G) / 4);
                grnBR = (1f / FKBaseChunk.MaxSunValue) * ((blockBotSW.G + blockBotW.G + blockMidSW.G + blockMidW.G) / 4);

                bluTL = (1f / FKBaseChunk.MaxSunValue) * ((blockTopNW.B + blockTopW.B + blockMidNW.B + blockMidW.B) / 4);
                bluTR = (1f / FKBaseChunk.MaxSunValue) * ((blockTopSW.B + blockTopW.B + blockMidSW.B + blockMidW.B) / 4);
                bluBL = (1f / FKBaseChunk.MaxSunValue) * ((blockBotNW.B + blockBotW.B + blockMidNW.B + blockMidW.B) / 4);
                bluBR = (1f / FKBaseChunk.MaxSunValue) * ((blockBotSW.B + blockBotW.B + blockMidSW.B + blockMidW.B) / 4);

                localTL = new Color(redTL, grnTL, bluTL);
                localTR = new Color(redTR, grnTR, bluTR);
                localBL = new Color(redBL, grnBL, bluBL);
                localBR = new Color(redBR, grnBR, bluBR);

                BuildFaceVertices(chunk, worldPosition, block.BaseBlockType, ENUM_BlockFaceDirection.eXDecreasing, sunTL, sunTR, sunBL, sunBR, localTL, localTR, localBL, localBR);
            }
            // +X face.
            if (!blockMidE.Exists && !(block.BaseBlockType == ENUM_FKBaseBlockType.eWater && blockMidE.BaseBlockType == ENUM_FKBaseBlockType.eWater))
            {
                sunTL = (1f / FKBaseChunk.MaxSunValue) * ((blockTopSE.Sun + blockTopE.Sun + blockMidSE.Sun + blockMidE.Sun) / 4);
                sunTR = (1f / FKBaseChunk.MaxSunValue) * ((blockTopNE.Sun + blockTopE.Sun + blockMidNE.Sun + blockMidE.Sun) / 4);
                sunBL = (1f / FKBaseChunk.MaxSunValue) * ((blockBotSE.Sun + blockBotE.Sun + blockMidSE.Sun + blockMidE.Sun) / 4);
                sunBR = (1f / FKBaseChunk.MaxSunValue) * ((blockBotNE.Sun + blockBotE.Sun + blockMidNE.Sun + blockMidE.Sun) / 4);

                redTL = (1f / FKBaseChunk.MaxSunValue) * ((blockTopSE.R + blockTopE.R + blockMidSE.R + blockMidE.R) / 4);
                redTR = (1f / FKBaseChunk.MaxSunValue) * ((blockTopNE.R + blockTopE.R + blockMidNE.R + blockMidE.R) / 4);
                redBL = (1f / FKBaseChunk.MaxSunValue) * ((blockBotSE.R + blockBotE.R + blockMidSE.R + blockMidE.R) / 4);
                redBR = (1f / FKBaseChunk.MaxSunValue) * ((blockBotNE.R + blockBotE.R + blockMidNE.R + blockMidE.R) / 4);

                grnTL = (1f / FKBaseChunk.MaxSunValue) * ((blockTopSE.G + blockTopE.G + blockMidSE.G + blockMidE.G) / 4);
                grnTR = (1f / FKBaseChunk.MaxSunValue) * ((blockTopNE.G + blockTopE.G + blockMidNE.G + blockMidE.G) / 4);
                grnBL = (1f / FKBaseChunk.MaxSunValue) * ((blockBotSE.G + blockBotE.G + blockMidSE.G + blockMidE.G) / 4);
                grnBR = (1f / FKBaseChunk.MaxSunValue) * ((blockBotNE.G + blockBotE.G + blockMidNE.G + blockMidE.G) / 4);

                bluTL = (1f / FKBaseChunk.MaxSunValue) * ((blockTopSE.B + blockTopE.B + blockMidSE.B + blockMidE.B) / 4);
                bluTR = (1f / FKBaseChunk.MaxSunValue) * ((blockTopNE.B + blockTopE.B + blockMidNE.B + blockMidE.B) / 4);
                bluBL = (1f / FKBaseChunk.MaxSunValue) * ((blockBotSE.B + blockBotE.B + blockMidSE.B + blockMidE.B) / 4);
                bluBR = (1f / FKBaseChunk.MaxSunValue) * ((blockBotNE.B + blockBotE.B + blockMidNE.B + blockMidE.B) / 4);

                localTL = new Color(redTL, grnTL, bluTL);
                localTR = new Color(redTR, grnTR, bluTR);
                localBL = new Color(redBL, grnBL, bluBL);
                localBR = new Color(redBR, grnBR, bluBR);

                BuildFaceVertices(chunk, worldPosition, block.BaseBlockType, ENUM_BlockFaceDirection.eXIncreasing, sunTL, sunTR, sunBL, sunBR, localTL, localTR, localBL, localBR);
            }
            // -Y face.
            if (!blockBotM.Exists && !(block.BaseBlockType == ENUM_FKBaseBlockType.eWater && blockBotM.BaseBlockType == ENUM_FKBaseBlockType.eWater))
            {
                sunBL = (1f / FKBaseChunk.MaxSunValue) * ((blockBotSW.Sun + blockBotS.Sun + blockBotM.Sun + blockTopW.Sun) / 4);
                sunBR = (1f / FKBaseChunk.MaxSunValue) * ((blockBotSE.Sun + blockBotS.Sun + blockBotM.Sun + blockTopE.Sun) / 4);
                sunTL = (1f / FKBaseChunk.MaxSunValue) * ((blockBotNW.Sun + blockBotN.Sun + blockBotM.Sun + blockTopW.Sun) / 4);
                sunTR = (1f / FKBaseChunk.MaxSunValue) * ((blockBotNE.Sun + blockBotN.Sun + blockBotM.Sun + blockTopE.Sun) / 4);

                redBL = (1f / FKBaseChunk.MaxSunValue) * ((blockBotSW.R + blockBotS.R + blockBotM.R + blockTopW.R) / 4);
                redBR = (1f / FKBaseChunk.MaxSunValue) * ((blockBotSE.R + blockBotS.R + blockBotM.R + blockTopE.R) / 4);
                redTL = (1f / FKBaseChunk.MaxSunValue) * ((blockBotNW.R + blockBotN.R + blockBotM.R + blockTopW.R) / 4);
                redTR = (1f / FKBaseChunk.MaxSunValue) * ((blockBotNE.R + blockBotN.R + blockBotM.R + blockTopE.R) / 4);

                grnBL = (1f / FKBaseChunk.MaxSunValue) * ((blockBotSW.G + blockBotS.G + blockBotM.G + blockTopW.G) / 4);
                grnBR = (1f / FKBaseChunk.MaxSunValue) * ((blockBotSE.G + blockBotS.G + blockBotM.G + blockTopE.G) / 4);
                grnTL = (1f / FKBaseChunk.MaxSunValue) * ((blockBotNW.G + blockBotN.G + blockBotM.G + blockTopW.G) / 4);
                grnTR = (1f / FKBaseChunk.MaxSunValue) * ((blockBotNE.G + blockBotN.G + blockBotM.G + blockTopE.G) / 4);

                bluBL = (1f / FKBaseChunk.MaxSunValue) * ((blockBotSW.B + blockBotS.B + blockBotM.B + blockTopW.B) / 4);
                bluBR = (1f / FKBaseChunk.MaxSunValue) * ((blockBotSE.B + blockBotS.B + blockBotM.B + blockTopE.B) / 4);
                bluTL = (1f / FKBaseChunk.MaxSunValue) * ((blockBotNW.B + blockBotN.B + blockBotM.B + blockTopW.B) / 4);
                bluTR = (1f / FKBaseChunk.MaxSunValue) * ((blockBotNE.B + blockBotN.B + blockBotM.B + blockTopE.B) / 4);

                localTL = new Color(redTL, grnTL, bluTL);
                localTR = new Color(redTR, grnTR, bluTR);
                localBL = new Color(redBL, grnBL, bluBL);
                localBR = new Color(redBR, grnBR, bluBR);

                BuildFaceVertices(chunk, worldPosition, block.BaseBlockType, ENUM_BlockFaceDirection.eYDecreasing, sunTL, sunTR, sunBL, sunBR, localTL, localTR, localBL, localBR);
            }
            // +Y face.
            if (!blockTopM.Exists && !(block.BaseBlockType == ENUM_FKBaseBlockType.eWater && blockTopM.BaseBlockType == ENUM_FKBaseBlockType.eWater))
            {
                sunTL = (1f / FKBaseChunk.MaxSunValue) * ((blockTopNW.Sun + blockTopN.Sun + blockTopW.Sun + blockTopM.Sun) / 4);
                sunTR = (1f / FKBaseChunk.MaxSunValue) * ((blockTopNE.Sun + blockTopN.Sun + blockTopE.Sun + blockTopM.Sun) / 4);
                sunBL = (1f / FKBaseChunk.MaxSunValue) * ((blockTopSW.Sun + blockTopS.Sun + blockTopW.Sun + blockTopM.Sun) / 4);
                sunBR = (1f / FKBaseChunk.MaxSunValue) * ((blockTopSE.Sun + blockTopS.Sun + blockTopE.Sun + blockTopM.Sun) / 4);

                redTL = (1f / FKBaseChunk.MaxSunValue) * ((blockTopNW.R + blockTopN.R + blockTopW.R + blockTopM.R) / 4);
                redTR = (1f / FKBaseChunk.MaxSunValue) * ((blockTopNE.R + blockTopN.R + blockTopE.R + blockTopM.R) / 4);
                redBL = (1f / FKBaseChunk.MaxSunValue) * ((blockTopSW.R + blockTopS.R + blockTopW.R + blockTopM.R) / 4);
                redBR = (1f / FKBaseChunk.MaxSunValue) * ((blockTopSE.R + blockTopS.R + blockTopE.R + blockTopM.R) / 4);

                grnTL = (1f / FKBaseChunk.MaxSunValue) * ((blockTopNW.G + blockTopN.G + blockTopW.G + blockTopM.G) / 4);
                grnTR = (1f / FKBaseChunk.MaxSunValue) * ((blockTopNE.G + blockTopN.G + blockTopE.G + blockTopM.G) / 4);
                grnBL = (1f / FKBaseChunk.MaxSunValue) * ((blockTopSW.G + blockTopS.G + blockTopW.G + blockTopM.G) / 4);
                grnBR = (1f / FKBaseChunk.MaxSunValue) * ((blockTopSE.G + blockTopS.G + blockTopE.G + blockTopM.G) / 4);

                bluTL = (1f / FKBaseChunk.MaxSunValue) * ((blockTopNW.B + blockTopN.B + blockTopW.B + blockTopM.B) / 4);
                bluTR = (1f / FKBaseChunk.MaxSunValue) * ((blockTopNE.B + blockTopN.B + blockTopE.B + blockTopM.B) / 4);
                bluBL = (1f / FKBaseChunk.MaxSunValue) * ((blockTopSW.B + blockTopS.B + blockTopW.B + blockTopM.B) / 4);
                bluBR = (1f / FKBaseChunk.MaxSunValue) * ((blockTopSE.B + blockTopS.B + blockTopE.B + blockTopM.B) / 4);

                localTL = new Color(redTL, grnTL, bluTL);
                localTR = new Color(redTR, grnTR, bluTR);
                localBL = new Color(redBL, grnBL, bluBL);
                localBR = new Color(redBR, grnBR, bluBR);

                BuildFaceVertices(chunk, worldPosition, block.BaseBlockType, ENUM_BlockFaceDirection.eYIncreasing, sunTL, sunTR, sunBL, sunBR, localTL, localTR, localBL, localBR);
            }
            // -Z face.
            if (!blockMidS.Exists && !(block.BaseBlockType == ENUM_FKBaseBlockType.eWater && blockMidS.BaseBlockType == ENUM_FKBaseBlockType.eWater))
            {
                sunTL = (1f / FKBaseChunk.MaxSunValue) * ((blockTopSW.Sun + blockTopS.Sun + blockMidSW.Sun + blockMidS.Sun) / 4);
                sunTR = (1f / FKBaseChunk.MaxSunValue) * ((blockTopSE.Sun + blockTopS.Sun + blockMidSE.Sun + blockMidS.Sun) / 4);
                sunBL = (1f / FKBaseChunk.MaxSunValue) * ((blockBotSW.Sun + blockBotS.Sun + blockMidSW.Sun + blockMidS.Sun) / 4);
                sunBR = (1f / FKBaseChunk.MaxSunValue) * ((blockBotSE.Sun + blockBotS.Sun + blockMidSE.Sun + blockMidS.Sun) / 4);

                redTL = (1f / FKBaseChunk.MaxSunValue) * ((blockTopSW.R + blockTopS.R + blockMidSW.R + blockMidS.R) / 4);
                redTR = (1f / FKBaseChunk.MaxSunValue) * ((blockTopSE.R + blockTopS.R + blockMidSE.R + blockMidS.R) / 4);
                redBL = (1f / FKBaseChunk.MaxSunValue) * ((blockBotSW.R + blockBotS.R + blockMidSW.R + blockMidS.R) / 4);
                redBR = (1f / FKBaseChunk.MaxSunValue) * ((blockBotSE.R + blockBotS.R + blockMidSE.R + blockMidS.R) / 4);

                grnTL = (1f / FKBaseChunk.MaxSunValue) * ((blockTopSW.G + blockTopS.G + blockMidSW.G + blockMidS.G) / 4);
                grnTR = (1f / FKBaseChunk.MaxSunValue) * ((blockTopSE.G + blockTopS.G + blockMidSE.G + blockMidS.G) / 4);
                grnBL = (1f / FKBaseChunk.MaxSunValue) * ((blockBotSW.G + blockBotS.G + blockMidSW.G + blockMidS.G) / 4);
                grnBR = (1f / FKBaseChunk.MaxSunValue) * ((blockBotSE.G + blockBotS.G + blockMidSE.G + blockMidS.G) / 4);

                bluTL = (1f / FKBaseChunk.MaxSunValue) * ((blockTopSW.B + blockTopS.B + blockMidSW.B + blockMidS.B) / 4);
                bluTR = (1f / FKBaseChunk.MaxSunValue) * ((blockTopSE.B + blockTopS.B + blockMidSE.B + blockMidS.B) / 4);
                bluBL = (1f / FKBaseChunk.MaxSunValue) * ((blockBotSW.B + blockBotS.B + blockMidSW.B + blockMidS.B) / 4);
                bluBR = (1f / FKBaseChunk.MaxSunValue) * ((blockBotSE.B + blockBotS.B + blockMidSE.B + blockMidS.B) / 4);

                localTL = new Color(redTL, grnTL, bluTL);
                localTR = new Color(redTR, grnTR, bluTR);
                localBL = new Color(redBL, grnBL, bluBL);
                localBR = new Color(redBR, grnBR, bluBR);

                BuildFaceVertices(chunk, worldPosition, block.BaseBlockType, ENUM_BlockFaceDirection.eZDecreasing, sunTL, sunTR, sunBL, sunBR, localTL, localTR, localBL, localBR);
            }
            // +Z face.
            if (!blockMidN.Exists && !(block.BaseBlockType == ENUM_FKBaseBlockType.eWater && blockMidN.BaseBlockType == ENUM_FKBaseBlockType.eWater))
            {
                sunTL = (1f / FKBaseChunk.MaxSunValue) * ((blockTopNE.Sun + blockTopN.Sun + blockMidNE.Sun + blockMidN.Sun) / 4);
                sunTR = (1f / FKBaseChunk.MaxSunValue) * ((blockTopNW.Sun + blockTopN.Sun + blockMidNW.Sun + blockMidN.Sun) / 4);
                sunBL = (1f / FKBaseChunk.MaxSunValue) * ((blockBotNE.Sun + blockBotN.Sun + blockMidNE.Sun + blockMidN.Sun) / 4);
                sunBR = (1f / FKBaseChunk.MaxSunValue) * ((blockBotNW.Sun + blockBotN.Sun + blockMidNW.Sun + blockMidN.Sun) / 4);

                redTL = (1f / FKBaseChunk.MaxSunValue) * ((blockTopNE.R + blockTopN.R + blockMidNE.R + blockMidN.R) / 4);
                redTR = (1f / FKBaseChunk.MaxSunValue) * ((blockTopNW.R + blockTopN.R + blockMidNW.R + blockMidN.R) / 4);
                redBL = (1f / FKBaseChunk.MaxSunValue) * ((blockBotNE.R + blockBotN.R + blockMidNE.R + blockMidN.R) / 4);
                redBR = (1f / FKBaseChunk.MaxSunValue) * ((blockBotNW.R + blockBotN.R + blockMidNW.R + blockMidN.R) / 4);

                grnTL = (1f / FKBaseChunk.MaxSunValue) * ((blockTopNE.G + blockTopN.G + blockMidNE.G + blockMidN.G) / 4);
                grnTR = (1f / FKBaseChunk.MaxSunValue) * ((blockTopNW.G + blockTopN.G + blockMidNW.G + blockMidN.G) / 4);
                grnBL = (1f / FKBaseChunk.MaxSunValue) * ((blockBotNE.G + blockBotN.G + blockMidNE.G + blockMidN.G) / 4);
                grnBR = (1f / FKBaseChunk.MaxSunValue) * ((blockBotNW.G + blockBotN.G + blockMidNW.G + blockMidN.G) / 4);

                bluTL = (1f / FKBaseChunk.MaxSunValue) * ((blockTopNE.B + blockTopN.B + blockMidNE.B + blockMidN.B) / 4);
                bluTR = (1f / FKBaseChunk.MaxSunValue) * ((blockTopNW.B + blockTopN.B + blockMidNW.B + blockMidN.B) / 4);
                bluBL = (1f / FKBaseChunk.MaxSunValue) * ((blockBotNE.B + blockBotN.B + blockMidNE.B + blockMidN.B) / 4);
                bluBR = (1f / FKBaseChunk.MaxSunValue) * ((blockBotNW.B + blockBotN.B + blockMidNW.B + blockMidN.B) / 4);

                localTL = new Color(redTL, grnTL, bluTL);
                localTR = new Color(redTR, grnTR, bluTR);
                localBL = new Color(redBL, grnBL, bluBL);
                localBR = new Color(redBR, grnBR, bluBR);

                BuildFaceVertices(chunk, worldPosition, block.BaseBlockType, ENUM_BlockFaceDirection.eZIncreasing, sunTL, sunTR, sunBL, sunBR, localTL, localTR, localBL, localBR);
            }
        }

        private static void BuildFaceVertices(FKBaseChunk chunk, FKVector3Int position, ENUM_FKBaseBlockType blockType, ENUM_BlockFaceDirection faceDir,
                                                float sunLightTL, float sunLightTR, float sunLightBL, float sunLightBR,
                                                Color localLightTL, Color localLightTR, Color localLightBL, Color localLightBR)
        {
            if (chunk.Disposed)
                return;

            ENUM_FKBaseBlockTextureType texture = FKBaseBlock.GetTextureType(blockType, faceDir);

            int faceIndex = 0;
            HalfVector2[] textureUVMappings = FKBaseBlockTextureHelper.BlockTextureMappings[(int)texture * 6 + faceIndex];

            switch (faceDir)
            {
                case ENUM_BlockFaceDirection.eXIncreasing:
                    {
                        //TR,TL,BR,BR,TL,BL
                        AddVertex(chunk, position, new Vector3(1, 1, 1), textureUVMappings[0], sunLightTR, localLightTR);
                        AddVertex(chunk, position, new Vector3(1, 1, 0), textureUVMappings[1], sunLightTL, localLightTL);
                        AddVertex(chunk, position, new Vector3(1, 0, 1), textureUVMappings[2], sunLightBR, localLightBR);
                        AddVertex(chunk, position, new Vector3(1, 0, 0), textureUVMappings[5], sunLightBL, localLightBL);
                        AddIndex(chunk, 0, 1, 2, 2, 1, 3);
                    }
                    break;

                case ENUM_BlockFaceDirection.eXDecreasing:
                    {
                        //TR,TL,BL,TR,BL,BR
                        AddVertex(chunk, position, new Vector3(0, 1, 0), textureUVMappings[0], sunLightTR, localLightTR);
                        AddVertex(chunk, position, new Vector3(0, 1, 1), textureUVMappings[1], sunLightTL, localLightTL);
                        AddVertex(chunk, position, new Vector3(0, 0, 0), textureUVMappings[5], sunLightBR, localLightBR);
                        AddVertex(chunk, position, new Vector3(0, 0, 1), textureUVMappings[2], sunLightBL, localLightBL);
                        AddIndex(chunk, 0, 1, 3, 0, 3, 2);
                    }
                    break;

                case ENUM_BlockFaceDirection.eYIncreasing:
                    {
                        //BL,BR,TR,BL,TR,TL
                        AddVertex(chunk, position, new Vector3(1, 1, 1), textureUVMappings[0], sunLightTR, localLightTR);
                        AddVertex(chunk, position, new Vector3(0, 1, 1), textureUVMappings[2], sunLightTL, localLightTL);
                        AddVertex(chunk, position, new Vector3(1, 1, 0), textureUVMappings[4], sunLightBR, localLightBR);
                        AddVertex(chunk, position, new Vector3(0, 1, 0), textureUVMappings[5], sunLightBL, localLightBL);
                        AddIndex(chunk, 3, 2, 0, 3, 0, 1);
                    }
                    break;

                case ENUM_BlockFaceDirection.eYDecreasing:
                    {
                        //TR,BR,TL,TL,BR,BL
                        AddVertex(chunk, position, new Vector3(1, 0, 1), textureUVMappings[0], sunLightTR, localLightTR);
                        AddVertex(chunk, position, new Vector3(0, 0, 1), textureUVMappings[2], sunLightTL, localLightTL);
                        AddVertex(chunk, position, new Vector3(1, 0, 0), textureUVMappings[4], sunLightBR, localLightBR);
                        AddVertex(chunk, position, new Vector3(0, 0, 0), textureUVMappings[5], sunLightBL, localLightBL);
                        AddIndex(chunk, 0, 2, 1, 1, 2, 3);
                    }
                    break;

                case ENUM_BlockFaceDirection.eZIncreasing:
                    {
                        //TR,TL,BL,TR,BL,BR
                        AddVertex(chunk, position, new Vector3(0, 1, 1), textureUVMappings[0], sunLightTR, localLightTR);
                        AddVertex(chunk, position, new Vector3(1, 1, 1), textureUVMappings[1], sunLightTL, localLightTL);
                        AddVertex(chunk, position, new Vector3(0, 0, 1), textureUVMappings[5], sunLightBR, localLightBR);
                        AddVertex(chunk, position, new Vector3(1, 0, 1), textureUVMappings[2], sunLightBL, localLightBL);
                        AddIndex(chunk, 0, 1, 3, 0, 3, 2);
                    }
                    break;

                case ENUM_BlockFaceDirection.eZDecreasing:
                    {
                        //TR,TL,BR,BR,TL,BL
                        AddVertex(chunk, position, new Vector3(1, 1, 0), textureUVMappings[0], sunLightTR, localLightTR);
                        AddVertex(chunk, position, new Vector3(0, 1, 0), textureUVMappings[1], sunLightTL, localLightTL);
                        AddVertex(chunk, position, new Vector3(1, 0, 0), textureUVMappings[2], sunLightBR, localLightBR);
                        AddVertex(chunk, position, new Vector3(0, 0, 0), textureUVMappings[5], sunLightBL, localLightBL);
                        AddIndex(chunk, 0, 1, 2, 2, 1, 3);
                    }
                    break;
            }
        }

        private static void AddVertex(FKBaseChunk chunk, FKVector3Int position, Vector3 addition, HalfVector2 textureCoordinate,
            float sunlight, Color localLight)
        {
            chunk.VertexList.Add(new FKBlockVertex(position.AsVector3() + addition, textureCoordinate, sunlight));        
        }

        private static void AddIndex(FKBaseChunk chunk, short i1, short i2, short i3, short i4, short i5, short i6)
        {
            chunk.IndexList.Add((short)(chunk.Index + i1));
            chunk.IndexList.Add((short)(chunk.Index + i2));
            chunk.IndexList.Add((short)(chunk.Index + i3));
            chunk.IndexList.Add((short)(chunk.Index + i4));
            chunk.IndexList.Add((short)(chunk.Index + i5));
            chunk.IndexList.Add((short)(chunk.Index + i6));
            chunk.Index += 4;
        }
    }
}