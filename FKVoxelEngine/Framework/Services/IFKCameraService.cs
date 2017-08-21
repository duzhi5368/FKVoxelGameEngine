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
// Create Time         :    2017/7/26 13:48:08
// Update Time         :    2017/7/26 13:48:08
// Class Version       :    v1.0.0.0
// Class Description   :    
// ===============================================================================
using Microsoft.Xna.Framework;
using FKVoxelEngine.Base;
using FKVoxelEngine.RenderObj;
using System;
// ===============================================================================
namespace FKVoxelEngine.Framework
{
    public enum ENUM_CameraMoveDirection
    {
        eForward,
        eBackward,
        eLeft,
        eRight,
        eUp,
        eDown,
    }
    public interface IFKCameraService
    {
        Matrix Projection { get; }
        Matrix View { get; }
        Matrix World { get; }

        Vector3 Position { get; set; }                  // 摄像机当前位置
        float CameraMoveSpeed { get; set; }             // 摄像机移动速度
        FKBaseChunk CurrentChunk { get; set; }          // 摄像机当前所在Chunk

        /// <summary>
        /// 进行摄像机位移
        /// </summary>
        /// <param name="gameTime"></param>
        /// <param name="eDirction"></param>
        void MoveCamera(GameTime gameTime, ENUM_CameraMoveDirection eDirction);
        /// <summary>
        /// 设置摄像机当前位置
        /// </summary>
        /// <param name="position"></param>
        void SetCamera(FKVector2Int position);
        /// <summary>
        /// 左右旋转摄像机
        /// </summary>
        /// <param name="step"></param>
        void RotateCamera(float step);
        /// <summary>
        /// 上下旋转摄像机
        /// </summary>
        /// <param name="step"></param>
        void ElevateCamera(float step);
        /// <summary>
        /// 摄像机锁定指定目标点
        /// </summary>
        /// <param name="target"></param>
        void LookAt(Vector3 target);
    }
}