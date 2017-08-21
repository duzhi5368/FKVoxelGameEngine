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
// Create Time         :    2017/8/21 17:09:13
// Update Time         :    2017/8/21 17:09:13
// Class Version       :    v1.0.0.0
// Class Description   :    
// ===============================================================================
using FKVoxelEngine.Base;
using System;
// ===============================================================================
namespace FKVoxelEngine.RenderObj
{
    public class FKTerrainGenerator_Standard : FKTerrainGenerator
    {
        protected FKTerrainBiomeGenerator BiomeGenerator;
        private FKParamPerlinNoise HighNoise;
        private FKParamPerlinNoise LowNoise;
        private FKParamPerlinNoise BottomNoise;
        private FKParamPerlinNoise CaveNoise;
        private FKClampNoise HighClamp;
        private FKClampNoise LowClamp;
        private FKClampNoise BottomClamp;
        private FKModifyNoise FinalNoise;

        public FKTerrainGenerator_Standard(FKTerrainBiomeGenerator bg)
            : base()
        {
            BiomeGenerator = bg;

            HighNoise = new FKParamPerlinNoise(Seed);
            LowNoise = new FKParamPerlinNoise(Seed);
            BottomNoise = new FKParamPerlinNoise(Seed);
            CaveNoise = new FKParamPerlinNoise(Seed);

            CaveNoise.Octaves = 3;
            CaveNoise.Amplitude = 0.05f;
            CaveNoise.Persistance = 2;
            CaveNoise.Frequency = 0.05f;
            CaveNoise.Lacunarity = 2;

            HighNoise.Persistance = 1;
            HighNoise.Frequency = 0.013f;
            HighNoise.Amplitude = 10;
            HighNoise.Octaves = 2;
            HighNoise.Lacunarity = 2;

            LowNoise.Persistance = 1;
            LowNoise.Frequency = 0.004f;
            LowNoise.Amplitude = 35;
            LowNoise.Octaves = 2;
            LowNoise.Lacunarity = 2.5f;

            BottomNoise.Persistance = 0.5f;
            BottomNoise.Frequency = 0.013f;
            BottomNoise.Amplitude = 5;
            BottomNoise.Octaves = 2;
            BottomNoise.Lacunarity = 1.5f;

            HighClamp = new FKClampNoise(HighNoise);
            HighClamp.MinValue = -30;
            HighClamp.MaxValue = 50;

            LowClamp = new FKClampNoise(LowNoise);
            LowClamp.MinValue = -30;
            LowClamp.MaxValue = 30;

            BottomClamp = new FKClampNoise(BottomNoise);
            BottomClamp.MinValue = -20;
            BottomClamp.MaxValue = 5;

            FinalNoise = new FKModifyNoise(HighClamp, LowClamp, ENUM_NoiseModifier.eNM_Add);
        }

        protected override void GenerateChunkTerrain(FKBaseChunk chunk)
        {
            var worley = new FKCellNoise(Seed);
            for (byte x = 0; x < FKBaseChunk.WidthInBlocks; ++x)
            {
                var worldPositionX = chunk.WorldPosition.X + x;
                for (byte z = 0; z < FKBaseChunk.LengthInBlocks; ++z)
                {
                    int worldPositionZ = chunk.WorldPosition.Z + z;
                    GenerateBlocks(chunk, worldPositionX, worldPositionZ, worley);
                }
            }
        }

        protected virtual void GenerateBlocks(FKBaseChunk chunk, int worldPositionX, int worldPositionZ, FKCellNoise cellNoise)
        {
            var blockX = worldPositionX;
            var blockZ = worldPositionZ;
            const float lowClampRange = 5;
            float lowClampMid = LowClamp.MaxValue - ((LowClamp.MaxValue + LowClamp.MinValue) / 2);
            float lowClampValue = LowClamp. Noise2D(blockX, blockZ);

            if (lowClampValue > lowClampMid - lowClampRange && lowClampValue < lowClampMid + lowClampRange)
            {
                FKInvertNoise NewPrimary = new FKInvertNoise(HighClamp);
                FinalNoise.PrimaryNoise = NewPrimary;
            }
            else
            {
                FinalNoise = new FKModifyNoise(HighClamp, LowClamp, ENUM_NoiseModifier.eNM_Add);
            }
            FinalNoise = new FKModifyNoise(FinalNoise, BottomClamp, ENUM_NoiseModifier.eNM_Subtract);


            int dirtHeight = (int)GetRockHeight(worldPositionX, worldPositionZ, FinalNoise);
            if (dirtHeight < 0)
                dirtHeight = 0;
            var offset = FKBaseBlockStorage.GetBlockIndexByWorldPosition(worldPositionX, worldPositionZ);
            for (int y = FKBaseChunk.MaxHeightIndexInBlocks; y >= 0; --y)
            {
                if (y > dirtHeight)
                {
                    FKBaseBlockStorage.Blocks[offset + y] = new FKBaseBlock(ENUM_FKBaseBlockType.eNone);
                }
                else
                {
                    FKBaseBlockStorage.Blocks[offset + y] = new FKBaseBlock(ENUM_FKBaseBlockType.eRock);
                }
            }
            BiomeGenerator.ApplyBiome(chunk, dirtHeight, offset + dirtHeight, worldPositionX, worldPositionZ);
        }

        protected virtual float GetRockHeight(int blockX, int blockZ, FKModifyNoise noise)
        {
            blockX += this.Seed;
            int minimumGroundHeight = FKBaseChunk.HeightInBlocks / 2;
            int minimumGroundDepth = (int)(FKBaseChunk.HeightInBlocks * 0.4f);

            float lowerGroundHeight = noise.Noise3D(blockX, Seed, blockZ);

            lowerGroundHeight = lowerGroundHeight * minimumGroundDepth + minimumGroundHeight;
            return lowerGroundHeight;
        }
    }
}