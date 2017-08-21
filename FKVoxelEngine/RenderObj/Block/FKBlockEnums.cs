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
// Create Time         :    2017/7/24 19:59:46
// Update Time         :    2017/7/24 19:59:46
// Class Version       :    v1.0.0.0
// Class Description   :    
// ===============================================================================
using System;
// ===============================================================================
namespace FKVoxelEngine.RenderObj
{
    /// <summary>
    /// 枚举格子类型
    /// </summary>
    public enum ENUM_BlockType
    {
        eNone           = 0,            // 空格子/空气
        eBase           = 1,            // 标准的类似Minacraft类型的    [主要用于地形，可破坏对象]
        eMagicaVoxel    = 2,            // MagicaVoxel体素对象          [主要用于静态对象]
        eVoxelshop      = 3,            // Voxelshop体素对象，带骨骼    [主要用于动态对象]

        eMaximum
    }

    /// <summary>
    /// 基本Block面朝向
    /// </summary>
    public enum ENUM_BlockFaceDirection
    {
        eXIncreasing = 1,
        eXDecreasing = 2,
        eYIncreasing = 4,
        eYDecreasing = 8,
        eZIncreasing = 16,
        eZDecreasing = 32,
    }
}