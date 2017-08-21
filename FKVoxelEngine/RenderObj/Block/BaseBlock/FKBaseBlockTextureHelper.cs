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
// Create Time         :    2017/8/8 14:47:21
// Update Time         :    2017/8/8 14:47:21
// Class Version       :    v1.0.0.0
// Class Description   :    
// ===============================================================================
using Microsoft.Xna.Framework.Graphics.PackedVector;
using System;
using System.Collections.Generic;
// ===============================================================================
namespace FKVoxelEngine.RenderObj
{
    public static class FKBaseBlockTextureHelper
    {
        public const int BlockTextureAtlasSize = 8;
        public const int CrackTextureAtlasSize = 3;

        private const float UnitBlockTextureOffset = 1.0f / BlockTextureAtlasSize;
        private const float UnitCrackTextureOffset = 1.0f / CrackTextureAtlasSize;

        public static readonly Dictionary<int, HalfVector2[]> BlockTextureMappings = new Dictionary<int, HalfVector2[]>();
        public static readonly Dictionary<int, HalfVector2[]> CrackTextureMappings = new Dictionary<int, HalfVector2[]>();

        static FKBaseBlockTextureHelper()
        {
            BuildBlockTextureMappings();
        }

        private static void BuildBlockTextureMappings()
        {
            for (int i = 0; i < (int)ENUM_FKBaseBlockTextureType.eMaximum; i++)
            {
                BlockTextureMappings.Add((i * 6),     GetBlockTextureMapping(i, ENUM_BlockFaceDirection.eXIncreasing));
                BlockTextureMappings.Add((i * 6) + 1, GetBlockTextureMapping(i, ENUM_BlockFaceDirection.eXDecreasing));
                BlockTextureMappings.Add((i * 6) + 2, GetBlockTextureMapping(i, ENUM_BlockFaceDirection.eYIncreasing));
                BlockTextureMappings.Add((i * 6) + 3, GetBlockTextureMapping(i, ENUM_BlockFaceDirection.eYDecreasing));
                BlockTextureMappings.Add((i * 6) + 4, GetBlockTextureMapping(i, ENUM_BlockFaceDirection.eZIncreasing));
                BlockTextureMappings.Add((i * 6) + 5, GetBlockTextureMapping(i, ENUM_BlockFaceDirection.eZDecreasing)); 
            }
        }

        private static HalfVector2[] GetBlockTextureMapping(int textureIndex, ENUM_BlockFaceDirection direction)
        {

            int y = textureIndex / BlockTextureAtlasSize;
            int x = textureIndex % BlockTextureAtlasSize;

            float yOffset = y * UnitBlockTextureOffset;
            float xOffset = x * UnitBlockTextureOffset;

            var mapping = new HalfVector2[6];
            switch (direction)
            {
                case ENUM_BlockFaceDirection.eXIncreasing:
                    mapping[0] = new HalfVector2(xOffset, yOffset); // 0,0 // first triangle.
                    mapping[1] = new HalfVector2(xOffset + UnitBlockTextureOffset, yOffset); // 1,0
                    mapping[2] = new HalfVector2(xOffset, yOffset + UnitBlockTextureOffset); // 0,1
                    mapping[3] = new HalfVector2(xOffset, yOffset + UnitBlockTextureOffset); // 0,1 // second triangle.
                    mapping[4] = new HalfVector2(xOffset + UnitBlockTextureOffset, yOffset); // 1,0
                    mapping[5] = new HalfVector2(xOffset + UnitBlockTextureOffset, yOffset + UnitBlockTextureOffset); // 1,1
                    break;

                case ENUM_BlockFaceDirection.eXDecreasing:
                    mapping[0] = new HalfVector2(xOffset, yOffset); // 0,0
                    mapping[1] = new HalfVector2(xOffset + UnitBlockTextureOffset, yOffset); // 1,0
                    mapping[2] = new HalfVector2(xOffset + UnitBlockTextureOffset, yOffset + UnitBlockTextureOffset); // 1,1
                    mapping[3] = new HalfVector2(xOffset, yOffset); // 0,0
                    mapping[4] = new HalfVector2(xOffset + UnitBlockTextureOffset, yOffset + UnitBlockTextureOffset); // 1,1
                    mapping[5] = new HalfVector2(xOffset, yOffset + UnitBlockTextureOffset); // 0,1
                    break;

                case ENUM_BlockFaceDirection.eYIncreasing:
                    mapping[0] = new HalfVector2(xOffset, yOffset + UnitBlockTextureOffset); // 0,1
                    mapping[1] = new HalfVector2(xOffset, yOffset); // 0,0
                    mapping[2] = new HalfVector2(xOffset + UnitBlockTextureOffset, yOffset); // 1,0
                    mapping[3] = new HalfVector2(xOffset, yOffset + UnitBlockTextureOffset); // 0,1
                    mapping[4] = new HalfVector2(xOffset + UnitBlockTextureOffset, yOffset); // 1,0
                    mapping[5] = new HalfVector2(xOffset + UnitBlockTextureOffset, yOffset + UnitBlockTextureOffset); // 1,1
                    break;

                case ENUM_BlockFaceDirection.eYDecreasing:
                    mapping[0] = new HalfVector2(xOffset, yOffset); // 0,0
                    mapping[1] = new HalfVector2(xOffset + UnitBlockTextureOffset, yOffset); // 1,0
                    mapping[2] = new HalfVector2(xOffset, yOffset + UnitBlockTextureOffset); // 0,1
                    mapping[3] = new HalfVector2(xOffset, yOffset + UnitBlockTextureOffset); // 0,1
                    mapping[4] = new HalfVector2(xOffset + UnitBlockTextureOffset, yOffset); // 1,0
                    mapping[5] = new HalfVector2(xOffset + UnitBlockTextureOffset, yOffset + UnitBlockTextureOffset); // 1,1
                    break;

                case ENUM_BlockFaceDirection.eZIncreasing:
                    mapping[0] = new HalfVector2(xOffset, yOffset); // 0,0
                    mapping[1] = new HalfVector2(xOffset + UnitBlockTextureOffset, yOffset); // 1,0
                    mapping[2] = new HalfVector2(xOffset + UnitBlockTextureOffset, yOffset + UnitBlockTextureOffset); // 1,1
                    mapping[3] = new HalfVector2(xOffset, yOffset); // 0,0
                    mapping[4] = new HalfVector2(xOffset + UnitBlockTextureOffset, yOffset + UnitBlockTextureOffset); // 1,1
                    mapping[5] = new HalfVector2(xOffset, yOffset + UnitBlockTextureOffset); // 0,1
                    break;

                case ENUM_BlockFaceDirection.eZDecreasing:
                    mapping[0] = new HalfVector2(xOffset, yOffset); // 0,0
                    mapping[1] = new HalfVector2(xOffset + UnitBlockTextureOffset, yOffset); // 1,0
                    mapping[2] = new HalfVector2(xOffset, yOffset + UnitBlockTextureOffset); // 0,1
                    mapping[3] = new HalfVector2(xOffset, yOffset + UnitBlockTextureOffset); // 0,1
                    mapping[4] = new HalfVector2(xOffset + UnitBlockTextureOffset, yOffset); // 1,0
                    mapping[5] = new HalfVector2(xOffset + UnitBlockTextureOffset, yOffset + UnitBlockTextureOffset); // 1,1
                    break;
            }
            return mapping;
        }
    }
}