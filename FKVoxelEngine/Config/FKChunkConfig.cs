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
// Create Time         :    2017/7/26 14:30:25
// Update Time         :    2017/7/26 14:30:25
// Class Version       :    v1.0.0.0
// Class Description   :    
// ===============================================================================
using System;
// ===============================================================================
namespace FKVoxelEngine.Config
{
    public class FKChunkConfig
    {
        public byte WidthInBlocks { get; set; }                     // Chunk宽度 x（用Block作为单位）
        public byte HeightInBlocks { get; set; }                    // Chunk高度 y（用Block作为单位）
        public byte LengthInBlocks { get; set; }                    // Chunk长度 z（用Block作为单位）

        public int VolumeInBlocks { get; private set; }            // Chunk体积（用Block作为单位，即一个Chunk中的Block个数）
        public byte MaxWidthIndexInBlocks { get; private set; }     // Chunk最大宽度索引
        public byte MaxHeightIndexInBlocks { get; private set; }    // Chunk最大高度索引
        public byte MaxLengthIndexInBlocks { get; private set; }    // Chunk最大深度索引

        internal FKChunkConfig()
        {
            WidthInBlocks = 16;
            HeightInBlocks = 128;
            LengthInBlocks = 16;
        }

        internal bool Setup()
        {
            VolumeInBlocks = WidthInBlocks * HeightInBlocks * LengthInBlocks;
            MaxHeightIndexInBlocks = (byte)(HeightInBlocks - 1);
            MaxWidthIndexInBlocks = (byte)(WidthInBlocks - 1);
            MaxLengthIndexInBlocks = (byte)(LengthInBlocks - 1);

            if (WidthInBlocks <= 0 || HeightInBlocks <= 0 || LengthInBlocks <= 0)
                throw new FKChunkConfigException("Chunk config is not validate.");

            return true;
        }

        public override string ToString()
        {
            string ret = string.Empty;

            ret += $"WidthInBlocks = {WidthInBlocks}\n";
            ret += $"HeightInBlocks = {HeightInBlocks}\n";
            ret += $"LengthInBlocks = {LengthInBlocks}\n";
            ret += $"VolumeInBlocks = {VolumeInBlocks}\n";
            ret += $"MaxWidthIndexInBlocks = {MaxWidthIndexInBlocks}\n";
            ret += $"MaxHeightIndexInBlocks = {MaxHeightIndexInBlocks}\n";
            ret += $"MaxLengthIndexInBlocks = {MaxLengthIndexInBlocks}\n";

            return ret;
        }
    }

    public class FKChunkConfigException : Exception
    {
        public FKChunkConfigException(string message)
            : base(message)
        {

        }
    }
}