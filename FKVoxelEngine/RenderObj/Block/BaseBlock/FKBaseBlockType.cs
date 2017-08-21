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
// Create Time         :    2017/7/25 11:01:37
// Update Time         :    2017/7/25 11:01:37
// Class Version       :    v1.0.0.0
// Class Description   :    
// ===============================================================================
using System;
// ===============================================================================
namespace FKVoxelEngine.RenderObj
{
    /// <summary>
    /// 基本地形单元类型
    /// </summary>
    public enum ENUM_FKBaseBlockType
    {
        eNone   = 0,        // 空气
        eBrick,             // 砖块
        eDirt,              // 土块
        eGold,              // 金块
        eGrass,             // 草
        eIron,              // 铁块
        eLava,              // 熔岩块
        eLeaves,            // 树叶块
        eGravel,            // 碎石块
        eRock,              // 岩石块
        eSand,              // 沙块
        eSnow,              // 雪块
        eTree,              // 树块
        eWater,             // 水块

        eMaximum
    }
    /// <summary>
    /// WARNING! 该顺序和贴图纹理位置相同，不要随意修改
    /// </summary>
    public enum ENUM_FKBaseBlockTextureType
    {
        eBrick  = 0,        // 砖块
        eDirt,              // 土壤
        eGold,              // 金矿
        eGrassSide,         // 草地侧边
        eGrassTop,          // 草地上边
        eIron,              // 铁矿
        eLava,              // 熔岩
        eLeaves,            // 树叶
        eGravel,            // 碎石
        eRock,              // 岩石
        eSand,              // 沙子
        eSnow,              // 雪
        eTreeTop,           // 树干上边
        eTreeSide,          // 树干侧边
        eWater,             // 水

        eMaximum
    }
}