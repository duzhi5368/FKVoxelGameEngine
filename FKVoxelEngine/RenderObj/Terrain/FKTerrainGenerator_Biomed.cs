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
// Create Time         :    2017/7/31 20:32:44
// Update Time         :    2017/7/31 20:32:44
// Class Version       :    v1.0.0.0
// Class Description   :    
// ===============================================================================
using FKVoxelEngine.Base;
using System;
// ===============================================================================
namespace FKVoxelEngine.RenderObj
{
    public class FKTerrainGenerator_Biomed : FKTerrainGenerator
    {
        protected FKTerrainBiomeGenerator BiomeGenerator;
        protected FKPerlinNoise PerlinNoise;

        public FKTerrainGenerator_Biomed(FKTerrainBiomeGenerator bg)
        {
            BiomeGenerator = bg;
            PerlinNoise = new FKPerlinNoise(Guid.NewGuid().GetHashCode());
        }

        protected override void GenerateChunkTerrain(FKBaseChunk chunk)
        {
            for(byte x = 0; x < FKBaseChunk.WidthInBlocks; ++x)
            {
                var worldPositionX = chunk.WorldPosition.X + x;
                for(byte z = 0; z < FKBaseChunk.LengthInBlocks; ++z)
                {
                    int worldPositionZ = chunk.WorldPosition.Z + z;
                    GenerateBlocks(chunk, worldPositionX, worldPositionZ);
                }
            }
        }

        protected virtual void GenerateBlocks(FKBaseChunk chunk, int worldPositionX, int worldPositionZ)
        {
            var rockHeigt = GetRockHeight(worldPositionX, worldPositionZ);
            var dirtHeight = GetDirtHeight(worldPositionX, worldPositionZ, rockHeigt);
            var offset = FKBaseBlockStorage.GetBlockIndexByWorldPosition(worldPositionX, worldPositionZ);
            for(int y = FKBaseChunk.MaxHeightIndexInBlocks; y >= 0; --y)
            {
                if(y > dirtHeight)
                {
                    FKBaseBlockStorage.Blocks[offset + y] = new FKBaseBlock(ENUM_FKBaseBlockType.eNone);
                }
                else if(y > rockHeigt)
                {
                    FKBaseBlockStorage.Blocks[offset + y] = new FKBaseBlock(ENUM_FKBaseBlockType.eDirt);
                }
                else
                {
                    FKBaseBlockStorage.Blocks[offset + y] = new FKBaseBlock(ENUM_FKBaseBlockType.eRock);
                }
            }

            BiomeGenerator.ApplyBiome(chunk, dirtHeight, offset + dirtHeight, worldPositionX, worldPositionZ);
        }

        protected virtual int GetDirtHeight(int blockX, int blockZ, float rockHeight)
        {
            blockX += this.Seed;

            float octave1 = (float)PerlinNoise.Noise3D(blockX * 0.001f, Seed, blockZ * 0.001f) * 0.5f;
            float octave2 = (float)PerlinNoise.Noise3D(blockX * 0.002f, Seed, blockZ * 0.002f) * 0.25f;
            float octave3 = (float)PerlinNoise.Noise3D(blockX * 0.01f, Seed, blockZ * 0.01f) * 0.25f;
            float octaveSum = octave1 + octave2 + octave3;
            return (int)(octaveSum * (FKBaseChunk.HeightInBlocks / 8) + (int)rockHeight);
        }

        protected virtual float GetRockHeight(int blockX, int blockZ)
        {
            blockX += this.Seed;
            int minimumGroundHeight = FKBaseChunk.HeightInBlocks / 2;
            int minimumGroundDepth = (int)(FKBaseChunk.HeightInBlocks * 0.4f);

            float octave1 = (float)PerlinNoise.Noise3D(blockX * 0.004f, Seed, blockZ * 0.004f) * 0.5f;
            float octave2 = (float)PerlinNoise.Noise3D(blockX * 0.003f, Seed, blockZ * 0.003f) * 0.25f;
            float octave3 = (float)PerlinNoise.Noise3D(blockX * 0.02f, Seed, blockZ * 0.02f) * 0.15f;
            float lowerGroundHeight = octave1 + octave2 + octave3;

            lowerGroundHeight = lowerGroundHeight * minimumGroundDepth + minimumGroundHeight;
            return lowerGroundHeight;
        }
    }
}