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
// Create Time         :    2017/7/25 10:55:03
// Update Time         :    2017/7/25 10:55:03
// Class Version       :    v1.0.0.0
// Class Description   :    
// ===============================================================================
using System;
// ===============================================================================
namespace FKVoxelEngine.RenderObj
{ 
    public class FKBaseBlock : FKBlock
    {
        public byte Sun { get; set; }
        public byte R { get; set; }
        public byte G { get; set; }
        public byte B { get; set; }
        public ENUM_FKBaseBlockType BaseBlockType { get; set; }

        public new bool Exists
        {
            get { return BaseBlockType != ENUM_FKBaseBlockType.eNone ; }
        }
        public new static FKBaseBlock Empty
        {
            get { return new FKBaseBlock(ENUM_FKBaseBlockType.eNone); }
        }

        public FKBaseBlock()
            :base(ENUM_BlockType.eBase)
        {
            Sun = 16;
            R = G = B = 16;
            BaseBlockType = ENUM_FKBaseBlockType.eNone;
        }

        public FKBaseBlock(ENUM_FKBaseBlockType subType)
            : base(ENUM_BlockType.eBase)
        {
            Sun = 16;
            R = G = B = 16;
            BaseBlockType = subType;
        }

        public static ENUM_FKBaseBlockTextureType GetTextureType(ENUM_FKBaseBlockType blockType, ENUM_BlockFaceDirection faceDir)
        {
            switch (blockType)
            {
                case ENUM_FKBaseBlockType.eBrick:
                    return ENUM_FKBaseBlockTextureType.eBrick;
                case ENUM_FKBaseBlockType.eDirt:
                    return ENUM_FKBaseBlockTextureType.eDirt;
                case ENUM_FKBaseBlockType.eGold:
                    return ENUM_FKBaseBlockTextureType.eGold;
                case ENUM_FKBaseBlockType.eGrass:
                    switch (faceDir)
                    {
                        case ENUM_BlockFaceDirection.eXIncreasing:
                        case ENUM_BlockFaceDirection.eXDecreasing:
                        case ENUM_BlockFaceDirection.eZIncreasing:
                        case ENUM_BlockFaceDirection.eZDecreasing:
                            return ENUM_FKBaseBlockTextureType.eGrassSide;
                        case ENUM_BlockFaceDirection.eYIncreasing:
                            return ENUM_FKBaseBlockTextureType.eGrassTop;
                        case ENUM_BlockFaceDirection.eYDecreasing:
                            return ENUM_FKBaseBlockTextureType.eDirt;
                        default:
                            return ENUM_FKBaseBlockTextureType.eRock;
                    }
                case ENUM_FKBaseBlockType.eIron:
                    return ENUM_FKBaseBlockTextureType.eIron;
                case ENUM_FKBaseBlockType.eLava:
                    return ENUM_FKBaseBlockTextureType.eLava;
                case ENUM_FKBaseBlockType.eLeaves:
                    return ENUM_FKBaseBlockTextureType.eLeaves;
                case ENUM_FKBaseBlockType.eGravel:
                    return ENUM_FKBaseBlockTextureType.eGravel;
                case ENUM_FKBaseBlockType.eRock:
                    return ENUM_FKBaseBlockTextureType.eRock;
                case ENUM_FKBaseBlockType.eSand:
                    return ENUM_FKBaseBlockTextureType.eSand;
                case ENUM_FKBaseBlockType.eSnow:
                    return ENUM_FKBaseBlockTextureType.eSnow;
                case ENUM_FKBaseBlockType.eTree:
                    switch (faceDir)
                    {
                        case ENUM_BlockFaceDirection.eXIncreasing:
                        case ENUM_BlockFaceDirection.eXDecreasing:
                        case ENUM_BlockFaceDirection.eZIncreasing:
                        case ENUM_BlockFaceDirection.eZDecreasing:
                            return ENUM_FKBaseBlockTextureType.eTreeSide;
                        case ENUM_BlockFaceDirection.eYIncreasing:
                        case ENUM_BlockFaceDirection.eYDecreasing:
                            return ENUM_FKBaseBlockTextureType.eTreeTop;
                        default:
                            return ENUM_FKBaseBlockTextureType.eRock;
                    }
                case ENUM_FKBaseBlockType.eWater:
                    return ENUM_FKBaseBlockTextureType.eWater;
                default:
                    return ENUM_FKBaseBlockTextureType.eRock;
            }
        }
    }
}