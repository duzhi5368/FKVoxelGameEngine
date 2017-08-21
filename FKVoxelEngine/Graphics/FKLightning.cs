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
// Create Time         :    2017/8/1 14:34:04
// Update Time         :    2017/8/1 14:34:04
// Class Version       :    v1.0.0.0
// Class Description   :    
// ===============================================================================
using FKVoxelEngine.RenderObj;
using System;
// ===============================================================================
namespace FKVoxelEngine.Graphics
{
    public static class FKLightning
    {
        public static void Process(FKBaseChunk chunk)
        {
            if (chunk.ChunkState != ENUM_FKBaseChunkState.eAwaitingLighting
                && chunk.ChunkState != ENUM_FKBaseChunkState.eAwaitingRelighting)
                return;

            chunk.ChunkState = ENUM_FKBaseChunkState.eLighting;

            SetInitLighting(chunk);
            FluidFillSunLight(chunk);
            FluidFillLightR(chunk);
            FluidFillLightG(chunk);
            FluidFillLightB(chunk);

            chunk.ChunkState = ENUM_FKBaseChunkState.eAwaitingBuild;
        }

        private static void SetInitLighting(FKBaseChunk chunk)
        {
            byte sunValue = FKBaseChunk.MaxSunValue;
            for(byte x = 0; x < FKBaseChunk.WidthInBlocks; ++x)
            {
                for(byte z = 0; z < FKBaseChunk.LengthInBlocks; ++z)
                {
                    int offset = FKBaseBlockStorage.GetBlockIndexByRelativePosition(chunk, x, z);
                    bool bInShade = false;
                    for(byte y = FKBaseChunk.MaxHeightIndexInBlocks; y > 0; --y)
                    {
                        if (!bInShade && FKBaseBlockStorage.Blocks[offset + y].BaseBlockType != ENUM_FKBaseBlockType.eNone)
                            bInShade = true;

                        FKBaseBlockStorage.Blocks[offset + y].Sun = bInShade ? (byte)0 : sunValue;
                        FKBaseBlockStorage.Blocks[offset + y].R = 0;
                        FKBaseBlockStorage.Blocks[offset + y].G = 0;
                        FKBaseBlockStorage.Blocks[offset + y].B = 0;
                    }
                }
            }
        }
        private static void FluidFillSunLight(FKBaseChunk chunk)
        {
            for (byte x = 0; x < FKBaseChunk.WidthInBlocks; ++x)
            {
                for (byte z = 0; z < FKBaseChunk.LengthInBlocks; ++z)
                {
                    int offset = FKBaseBlockStorage.GetBlockIndexByRelativePosition(chunk, x, z);
                    for (byte y = FKBaseChunk.MaxHeightIndexInBlocks; y > 0; --y)
                    {
                        var blockIndex = offset + y;
                        if (FKBaseBlockStorage.Blocks[offset + y].BaseBlockType != ENUM_FKBaseBlockType.eNone)
                            continue;

                        var blockLight = FKBaseBlockStorage.Blocks[blockIndex].Sun;
                        if (blockLight <= 1)
                            continue;// 过黑
                        var PropagatedLight = (byte)((blockLight * 9) / 10);

                        PropagateSunLight(blockIndex + FKBaseBlockStorage.XStep, PropagatedLight);
                        PropagateSunLight(blockIndex - FKBaseBlockStorage.XStep, PropagatedLight);
                        PropagateSunLight(blockIndex + FKBaseBlockStorage.ZStep, PropagatedLight);
                        PropagateSunLight(blockIndex - FKBaseBlockStorage.ZStep, PropagatedLight);

                        PropagateSunLight(blockIndex - 1, PropagatedLight);
                    }
                }
            }
        }

        private static void PropagateSunLight(int blockIndex, byte incomingLight)
        {
            blockIndex = blockIndex % FKBaseBlockStorage.Blocks.Length;
            if (blockIndex < 0)
                blockIndex += FKBaseBlockStorage.Blocks.Length;
            if (incomingLight <= 1)
                return;
            if (FKBaseBlockStorage.Blocks[blockIndex] == null)
                return;
            if (FKBaseBlockStorage.Blocks[blockIndex].BaseBlockType != ENUM_FKBaseBlockType.eNone)
                return;
            if (incomingLight <= FKBaseBlockStorage.Blocks[blockIndex].Sun)
                return;

            FKBaseBlockStorage.Blocks[blockIndex].Sun = incomingLight;
            var PropagatedLight = (byte)((incomingLight * 9) / 10);

            PropagateSunLight(blockIndex + FKBaseBlockStorage.XStep, PropagatedLight);
            PropagateSunLight(blockIndex - FKBaseBlockStorage.XStep, PropagatedLight);
            PropagateSunLight(blockIndex + FKBaseBlockStorage.ZStep, PropagatedLight);
            PropagateSunLight(blockIndex - FKBaseBlockStorage.ZStep, PropagatedLight);

            PropagateSunLight(blockIndex - 1, PropagatedLight);
        }
        private static void FluidFillLightR(FKBaseChunk chunk)
        {
            for (byte x = 0; x < FKBaseChunk.WidthInBlocks; ++x)
            {
                for (byte z = 0; z < FKBaseChunk.LengthInBlocks; ++z)
                {
                    int offset = FKBaseBlockStorage.GetBlockIndexByRelativePosition(chunk, x, z);
                    for (byte y = FKBaseChunk.MaxHeightIndexInBlocks; y > 0; --y)
                    {
                        var blockIndex = offset + y;
                        if (FKBaseBlockStorage.Blocks[offset + y].BaseBlockType != ENUM_FKBaseBlockType.eNone)
                            continue;

                        var blockLight = FKBaseBlockStorage.Blocks[blockIndex].R;
                        if (blockLight <= 1)
                            continue;// 过黑
                        var PropagatedLight = (byte)((blockLight * 9) / 10);

                        PropagateLightR(blockIndex + FKBaseBlockStorage.XStep, PropagatedLight);
                        PropagateLightR(blockIndex - FKBaseBlockStorage.XStep, PropagatedLight);
                        PropagateLightR(blockIndex + FKBaseBlockStorage.ZStep, PropagatedLight);
                        PropagateLightR(blockIndex - FKBaseBlockStorage.ZStep, PropagatedLight);

                        PropagateLightR(blockIndex - 1, PropagatedLight);
                    }
                }
            }
        }
        private static void PropagateLightR(int blockIndex, byte incomingLight)
        {
            blockIndex = blockIndex % FKBaseBlockStorage.Blocks.Length;
            if (blockIndex < 0)
                blockIndex += FKBaseBlockStorage.Blocks.Length;
            if (incomingLight <= 1)
                return;
            if (FKBaseBlockStorage.Blocks[blockIndex].BaseBlockType != ENUM_FKBaseBlockType.eNone)
                return;
            if (incomingLight <= FKBaseBlockStorage.Blocks[blockIndex].R)
                return;

            FKBaseBlockStorage.Blocks[blockIndex].R = incomingLight;
            var PropagatedLight = (byte)((incomingLight * 9) / 10);

            PropagateLightR(blockIndex + FKBaseBlockStorage.XStep, PropagatedLight);
            PropagateLightR(blockIndex - FKBaseBlockStorage.XStep, PropagatedLight);
            PropagateLightR(blockIndex + FKBaseBlockStorage.ZStep, PropagatedLight);
            PropagateLightR(blockIndex - FKBaseBlockStorage.ZStep, PropagatedLight);

            PropagateLightR(blockIndex - 1, PropagatedLight);
        }
        private static void FluidFillLightG(FKBaseChunk chunk)
        {
            for (byte x = 0; x < FKBaseChunk.WidthInBlocks; ++x)
            {
                for (byte z = 0; z < FKBaseChunk.LengthInBlocks; ++z)
                {
                    int offset = FKBaseBlockStorage.GetBlockIndexByRelativePosition(chunk, x, z);
                    for (byte y = FKBaseChunk.MaxHeightIndexInBlocks; y > 0; --y)
                    {
                        var blockIndex = offset + y;
                        if (FKBaseBlockStorage.Blocks[offset + y].BaseBlockType != ENUM_FKBaseBlockType.eNone)
                            continue;

                        var blockLight = FKBaseBlockStorage.Blocks[blockIndex].G;
                        if (blockLight <= 1)
                            continue;// 过黑
                        var PropagatedLight = (byte)((blockLight * 9) / 10);

                        PropagateLightG(blockIndex + FKBaseBlockStorage.XStep, PropagatedLight);
                        PropagateLightG(blockIndex - FKBaseBlockStorage.XStep, PropagatedLight);
                        PropagateLightG(blockIndex + FKBaseBlockStorage.ZStep, PropagatedLight);
                        PropagateLightG(blockIndex - FKBaseBlockStorage.ZStep, PropagatedLight);

                        PropagateLightG(blockIndex - 1, PropagatedLight);
                    }
                }
            }
        }
        private static void PropagateLightG(int blockIndex, byte incomingLight)
        {
            blockIndex = blockIndex % FKBaseBlockStorage.Blocks.Length;
            if (blockIndex < 0)
                blockIndex += FKBaseBlockStorage.Blocks.Length;
            if (incomingLight <= 1)
                return;
            if (FKBaseBlockStorage.Blocks[blockIndex].BaseBlockType != ENUM_FKBaseBlockType.eNone)
                return;
            if (incomingLight <= FKBaseBlockStorage.Blocks[blockIndex].G)
                return;

            FKBaseBlockStorage.Blocks[blockIndex].G = incomingLight;
            var PropagatedLight = (byte)((incomingLight * 9) / 10);

            PropagateLightG(blockIndex + FKBaseBlockStorage.XStep, PropagatedLight);
            PropagateLightG(blockIndex - FKBaseBlockStorage.XStep, PropagatedLight);
            PropagateLightG(blockIndex + FKBaseBlockStorage.ZStep, PropagatedLight);
            PropagateLightG(blockIndex - FKBaseBlockStorage.ZStep, PropagatedLight);
 
            PropagateLightG(blockIndex - 1, PropagatedLight);
        }
        private static void FluidFillLightB(FKBaseChunk chunk)
        {
            for (byte x = 0; x < FKBaseChunk.WidthInBlocks; ++x)
            {
                for (byte z = 0; z < FKBaseChunk.LengthInBlocks; ++z)
                {
                    int offset = FKBaseBlockStorage.GetBlockIndexByRelativePosition(chunk, x, z);
                    for (byte y = FKBaseChunk.MaxHeightIndexInBlocks; y > 0; --y)
                    {
                        var blockIndex = offset + y;
                        if (FKBaseBlockStorage.Blocks[offset + y].BaseBlockType != ENUM_FKBaseBlockType.eNone)
                            continue;

                        var blockLight = FKBaseBlockStorage.Blocks[blockIndex].B;
                        if (blockLight <= 1)
                            continue;// 过黑
                        var PropagatedLight = (byte)((blockLight * 9) / 10);

                        PropagateLightB(blockIndex + FKBaseBlockStorage.XStep, PropagatedLight);
                        PropagateLightB(blockIndex - FKBaseBlockStorage.XStep, PropagatedLight);
                        PropagateLightB(blockIndex + FKBaseBlockStorage.ZStep, PropagatedLight);
                        PropagateLightB(blockIndex - FKBaseBlockStorage.ZStep, PropagatedLight);

                        PropagateLightB(blockIndex - 1, PropagatedLight);
                    }
                }
            }
        }

        private static void PropagateLightB(int blockIndex, byte incomingLight)
        {
            blockIndex = blockIndex % FKBaseBlockStorage.Blocks.Length;
            if (blockIndex < 0)
                blockIndex += FKBaseBlockStorage.Blocks.Length;
            if (incomingLight <= 1)
                return;
            if (FKBaseBlockStorage.Blocks[blockIndex].BaseBlockType != ENUM_FKBaseBlockType.eNone)
                return;
            if (incomingLight <= FKBaseBlockStorage.Blocks[blockIndex].B)
                return;

            FKBaseBlockStorage.Blocks[blockIndex].B = incomingLight;
            var PropagatedLight = (byte)((incomingLight * 9) / 10);

            PropagateLightB(blockIndex + FKBaseBlockStorage.XStep, PropagatedLight);
            PropagateLightB(blockIndex - FKBaseBlockStorage.XStep, PropagatedLight);
            PropagateLightB(blockIndex + FKBaseBlockStorage.ZStep, PropagatedLight);
            PropagateLightB(blockIndex - FKBaseBlockStorage.ZStep, PropagatedLight);

            PropagateLightB(blockIndex - 1, PropagatedLight);
        }
    }
}