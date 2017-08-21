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
// Create Time         :    2017/7/26 16:50:18
// Update Time         :    2017/7/26 16:50:18
// Class Version       :    v1.0.0.0
// Class Description   :    
// ===============================================================================
using FKVoxelEngine.Framework;
using System;
// ===============================================================================
namespace FKVoxelEngine.Config
{
    public class FKCacheConfig
    {
        public byte ViewRange { get; set; }         // 视野区域的Chunk个数
        public byte CacheRange { get; set; }        // 缓冲区域的Chunk个数
        public bool CacheExtraChunks { get; set; }  // 是否允许引擎缓冲额外Chunk


        public int CacheRangeWidthInBlocks { get; private set; }
        public int CacheRangeHeightInBlocks { get; private set; }
        public int CacheRangeLenghtInBlocks { get; private set; }
        public int ViewRangeWidthInBlocks { get; private set; }
        public int ViewRangeHeightInBlocks { get; private set; }
        public int ViewRangeLenghtInBlocks { get; private set; }
        public int ChunksInCacheRange { get; private set; }
        public int ChunksInViewRange { get; private set; }
        public int CacheRangeVolume { get; private set; }
        public int ViewRangeVolume { get; private set; }

        internal FKCacheConfig()
        {
            ViewRange = 3;
            CacheRange = 5;
            CacheExtraChunks = true;
        }

        internal bool Setup()
        {
            if (ViewRange == 0 || CacheRange == 0)
                throw new FKCacheConfigException("Cache config is not validate.");

            ChunksInCacheRange = (CacheRange * 2 + 1) * (CacheRange * 2 + 1);
            ChunksInViewRange = (ViewRange * 2 + 1) * (ViewRange * 2 + 1);

            CacheRangeWidthInBlocks = (CacheRange * 2 + 1) * FKEngine.GetInstance.Config.Chunk.WidthInBlocks;
            CacheRangeHeightInBlocks = FKEngine.GetInstance.Config.Chunk.HeightInBlocks;
            CacheRangeLenghtInBlocks = (CacheRange * 2 + 1) * FKEngine.GetInstance.Config.Chunk.LengthInBlocks;
            CacheRangeVolume = CacheRangeWidthInBlocks * CacheRangeHeightInBlocks * CacheRangeLenghtInBlocks;

            ViewRangeWidthInBlocks = (ViewRange * 2 + 1) * FKEngine.GetInstance.Config.Chunk.WidthInBlocks;
            ViewRangeHeightInBlocks = FKEngine.GetInstance.Config.Chunk.HeightInBlocks;
            ViewRangeLenghtInBlocks = (ViewRange * 2 + 1) * FKEngine.GetInstance.Config.Chunk.LengthInBlocks;
            ViewRangeVolume = ViewRangeWidthInBlocks * ViewRangeHeightInBlocks * ViewRangeLenghtInBlocks;

            return true;
        }

        public override string ToString()
        {
            string ret = string.Empty;

            ret += $"ViewRange = {ViewRange}\n";
            ret += $"CacheRange = {CacheRange}\n";
            ret += $"CacheExtraChunks = {CacheExtraChunks}\n";
            ret += $"CacheRangeWidthInBlocks = {CacheRangeWidthInBlocks}\n";
            ret += $"CacheRangeHeightInBlocks = {CacheRangeHeightInBlocks}\n";
            ret += $"CacheRangeLenghtInBlocks = {CacheRangeLenghtInBlocks}\n";
            ret += $"ViewRangeWidthInBlocks = {ViewRangeWidthInBlocks}\n";
            ret += $"ViewRangeHeightInBlocks = {ViewRangeHeightInBlocks}\n";
            ret += $"ViewRangeLenghtInBlocks = {ViewRangeLenghtInBlocks}\n";
            ret += $"ChunksInCacheRange = {ChunksInCacheRange}\n";
            ret += $"ChunksInViewRange = {ChunksInViewRange}\n";
            ret += $"CacheRangeVolume = {CacheRangeVolume}\n";
            ret += $"ViewRangeVolume = {ViewRangeVolume}\n";

            return ret;
        }
    }

    public class FKCacheConfigException : Exception
    {
        public FKCacheConfigException(string message)
            : base(message)
        { }
    }
}