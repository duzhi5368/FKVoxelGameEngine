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
// Create Time         :    2017/8/8 16:11:27
// Update Time         :    2017/8/8 16:11:27
// Class Version       :    v1.0.0.0
// Class Description   :    
// ===============================================================================
using FKVoxelEngine.Framework;
using System;
// ===============================================================================
namespace FKVoxelEngine.Debug
{
    [FKCommand("fog", "Sets fog mode.\nusage: fog [off|near|far]")]
    public class FKCommand_Fog : FKCommand
    {
        private IFKFoggerService _Fogger;

        public FKCommand_Fog()
        {
            _Fogger = (IFKFoggerService)FKEngine.GetInstance.Game.Services.GetService(typeof(IFKFoggerService));
        }


        [FKDefaultCommand]
        public string Default(string[] @params)
        {
            return string.Format("Fog is currently set to {0} mode.\nusage: fog [off|near|far]",
                                 this._Fogger.State.ToString().ToLower());
        }

        [FKSubCommand("off", "Sets fog to off.")]
        public string Off(string[] @params)
        {
            this._Fogger.State = ENUM_FogState.eNone;
            return "Fog is off.";
        }

        [FKSubCommand("near", "Sets fog to near.")]
        public string Near(string[] @params)
        {
            this._Fogger.State = ENUM_FogState.eNear;
            return "Fog is near.";
        }

        [FKSubCommand("far", "Sets fog to far.")]
        public string Far(string[] @params)
        {
            this._Fogger.State = ENUM_FogState.eFar;
            return "Fog is far.";
        }
    }
}