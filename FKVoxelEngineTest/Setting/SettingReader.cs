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
// Create Time         :    2017/8/11 10:33:32
// Update Time         :    2017/8/11 10:33:32
// Class Version       :    v1.0.0.0
// Class Description   :    
// ===============================================================================
using Nini.Config;
using System;
// ===============================================================================
namespace FKVoxelEngineTest.Setting
{
    public class SettingReader
    {
        private readonly IConfig _Config; 

        public SettingReader(string sectionName)
        {
            _Config = SettingManager.Section(sectionName) ?? SettingManager.AddSection(sectionName);
        }

        public void Save()
        {
            SettingManager.Save();
        }

        protected bool GetBoolean(string key, bool defaultValue) { return _Config.GetBoolean(key, defaultValue); }
        protected double GetDouble(string key, double defaultValue) { return _Config.GetDouble(key, defaultValue); }
        protected float GetFloat(string key, float defaultValue) { return _Config.GetFloat(key, defaultValue); }
        protected int GetInt(string key, int defaultValue) { return _Config.GetInt(key, defaultValue); }
        protected int GetInt(string key, int defaultValue, bool fromAlias) { return _Config.GetInt(key, defaultValue, fromAlias); }
        protected long GetLong(string key, long defaultValue) { return _Config.GetLong(key, defaultValue); }
        protected string GetString(string key, string defaultValue) { return _Config.Get(key, defaultValue); }
        protected string[] GetEntryKeys() { return _Config.GetKeys(); }
        protected void Set(string key, object value) { _Config.Set(key, value); }
    }
}