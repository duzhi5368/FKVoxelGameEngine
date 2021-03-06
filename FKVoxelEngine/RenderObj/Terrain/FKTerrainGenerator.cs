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
// Create Time         :    2017/7/31 19:43:42
// Update Time         :    2017/7/31 19:43:42
// Class Version       :    v1.0.0.0
// Class Description   :    
// ===============================================================================
using System;
// ===============================================================================
namespace FKVoxelEngine.RenderObj
{
    public class FKTerrainGenerator
    {
        public const int DefaultSeed = 51504891;
        protected int Seed { get; set; }
        public FKTerrainGenerator(int seed)
        {
            Seed = seed;
        }

        public FKTerrainGenerator()
            : this(DefaultSeed)
        {

        }

        public void Generate(FKBaseChunk chunk)
        {
            if (chunk.ChunkState != ENUM_FKBaseChunkState.eAwaitingGenerate)
                return;

            chunk.ChunkState = ENUM_FKBaseChunkState.eGenerating;
            GenerateChunkTerrain(chunk);
            chunk.ChunkState = ENUM_FKBaseChunkState.eAwaitingLighting;
        }

        protected virtual void GenerateChunkTerrain(FKBaseChunk chunk)
        {

        }
    }
}