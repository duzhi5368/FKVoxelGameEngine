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
// Create Time         :    2017/8/2 12:03:05
// Update Time         :    2017/8/2 12:03:05
// Class Version       :    v1.0.0.0
// Class Description   :    
// ===============================================================================
using FKVoxelEngine.Base;
using System;
// ===============================================================================
namespace FKVoxelEngine.RenderObj
{
    public class FKTerrainGenerator_Valley : FKTerrainGenerator_Biomed
    {
        private FKSimpleNoise _Noise;
        public FKTerrainGenerator_Valley(FKTerrainBiomeGenerator tbg)
            : base(tbg)
        {
            _Noise = new FKSimpleNoise();
        }

        protected override void GenerateBlocks(FKBaseChunk chunk, int worldPositionX, int worldPositionZ)
        {
            var rockHeight = GetRockHeight(worldPositionX, worldPositionZ);
            var dirtHeight = GetDirtHeight(worldPositionZ, worldPositionZ, rockHeight);

            var offset = FKBaseBlockStorage.GetBlockIndexByWorldPosition(worldPositionX, worldPositionZ);
            for(int y = FKBaseChunk.MaxHeightIndexInBlocks; y >= 0; --y)
            {
                if(y > dirtHeight)
                {
                    FKBaseBlockStorage.Blocks[offset + y] = new FKBaseBlock(ENUM_FKBaseBlockType.eNone);
                }
                else if(y > rockHeight)
                {
                    var valleyNoise = GenarateVallayNoise(worldPositionX, worldPositionZ, y);
                    FKBaseBlockStorage.Blocks[offset + y] = new FKBaseBlock(valleyNoise > 0.2f ? ENUM_FKBaseBlockType.eNone : ENUM_FKBaseBlockType.eDirt);
                }
                else
                {
                    FKBaseBlockStorage.Blocks[offset + y] = new FKBaseBlock(ENUM_FKBaseBlockType.eRock);
                }
            }

            BiomeGenerator.ApplyBiome(chunk, dirtHeight, offset + dirtHeight, worldPositionX + Seed, worldPositionZ);
        }

        protected virtual float GenarateVallayNoise(int worldPositionX, int worldPositionZ, int blockY)
        {
            worldPositionX += Seed;

            float caveNoise = _Noise.Noise3D(worldPositionX * 0.01f, worldPositionZ * 0.01f, blockY * 0.01f) * (0.015f * blockY) + 0.1f;
            caveNoise += _Noise.Noise3D(worldPositionX * 0.01f, worldPositionZ * 0.01f, blockY * 0.1f) * 0.06f + 0.1f;
            caveNoise += _Noise.Noise3D(worldPositionX * 0.2f, worldPositionZ * 0.2f, blockY * 0.2f) * 0.03f + 0.01f;

            return caveNoise;
        }

        protected override int GetDirtHeight(int blockX, int blockZ, float rockHeight)
        {
            blockX += Seed;

            float octave1 = _Noise.Noise2D((blockX + 100) * 0.001f, blockZ * 0.0001f) * 0.5f;
            float octave2 = _Noise.Noise2D((blockX + 100) * 0.002f, blockZ * 0.0002f) * 0.25f;
            float octave3 = _Noise.Noise2D((blockX + 100) * 0.01f, blockZ * 0.01f) * 0.25f;

            float lowerGroundHeight = octave1 + octave2 + octave3;
            lowerGroundHeight = (int)(lowerGroundHeight * FKBaseChunk.HeightInBlocks / 2f + (int)rockHeight);
            return (int)lowerGroundHeight;
        }

        protected override float GetRockHeight(int blockX, int blockZ)
        {
            blockX += Seed;

            int minimumGoundHeight = (int)(FKBaseChunk.HeightInBlocks * 0.25f);
            int maximunGoundDepth = (int)(FKBaseChunk.HeightInBlocks * 0.5f);

            float octave1 = _Noise.Noise2D(blockX * 0.0001f, blockZ * 0.0001f) * 0.5f;
            float octave2 = _Noise.Noise2D(blockX * 0.0005f, blockZ * 0.0005f) * 0.35f;
            float octave3 = _Noise.Noise2D(blockX * 0.02f, blockZ * 0.02f) * 0.15f;

            float lowerGroundHeight = octave1 + octave2 + octave3;
            lowerGroundHeight = lowerGroundHeight * maximunGoundDepth + minimumGoundHeight;
            return lowerGroundHeight;
        }
    }
}