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
// Create Time         :    2017/8/2 11:57:01
// Update Time         :    2017/8/2 11:57:01
// Class Version       :    v1.0.0.0
// Class Description   :    
// ===============================================================================
using FKVoxelEngine.Base;
using System;
// ===============================================================================
namespace FKVoxelEngine.RenderObj
{
    class FKTerrainGenerator_Mountain : FKTerrainGenerator_Biomed
    {
        private FKSimpleNoise _Noise;
        public FKTerrainGenerator_Mountain(FKTerrainBiomeGenerator tbg)
            : base(tbg)
        {
            _Noise = new FKSimpleNoise();
        }

        protected override float GetRockHeight(int blockX, int blockZ)
        {
            blockX += Seed;

            int minimumGoundHeight = (int)(FKBaseChunk.HeightInBlocks * 0.25f);
            int maximunGoundDepth = (int)(FKBaseChunk.HeightInBlocks * 0.7f);

            float octave1 = _Noise.Noise2D(blockX * 0.0001f, blockZ * 0.0001f) * 0.5f;
            float octave2 = _Noise.Noise2D(blockX * 0.0005f, blockZ * 0.0005f) * 0.25f;
            float octave3 = _Noise.Noise2D(blockX * 0.005f, blockZ * 0.005f) * 0.12f;
            float octave4 = _Noise.Noise2D(blockX * 0.01f, blockZ * 0.01f) * 0.12f;
            float octave5 = _Noise.Noise2D(blockX * 0.03f, blockZ * 0.03f) * octave4;

            float lowerGroundHeight = octave1 + octave2 + octave3 + octave4 + octave5;
            lowerGroundHeight = lowerGroundHeight * maximunGoundDepth + minimumGoundHeight;
            return lowerGroundHeight;
        }
    }
}