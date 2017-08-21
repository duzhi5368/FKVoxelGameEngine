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
// Create Time         :    2017/7/31 20:31:58
// Update Time         :    2017/7/31 20:31:58
// Class Version       :    v1.0.0.0
// Class Description   :    
// ===============================================================================
using System;
// ===============================================================================
namespace FKVoxelEngine.RenderObj
{
    public class FKTerrainGenerator_Flat : FKTerrainGenerator_Biomed
    {
        private const byte DirtHeight = 1;

        public FKTerrainGenerator_Flat(FKTerrainBiomeGenerator tbg)
            : base(tbg)
        {

        }

        protected override void GenerateBlocks(FKBaseChunk chunk, int worldPositionX, int worldPositionZ)
        {
            var offset = FKBaseBlockStorage.GetBlockIndexByWorldPosition(worldPositionX, worldPositionZ);

            for(int y = FKBaseChunk.MaxHeightIndexInBlocks; y >= 0; --y)
            {
                if (y >= DirtHeight)
                    FKBaseBlockStorage.Blocks[offset + y] = new FKBaseBlock(ENUM_FKBaseBlockType.eNone);
                else
                    FKBaseBlockStorage.Blocks[offset + y] = new FKBaseBlock(ENUM_FKBaseBlockType.eDirt);
            }

            BiomeGenerator.ApplyBiome(chunk, DirtHeight - 1, offset + DirtHeight - 1, worldPositionX + Seed, worldPositionZ);
        }
    }
}