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
// Create Time         :    2017/8/18 14:33:00
// Update Time         :    2017/8/18 14:33:00
// Class Version       :    v1.0.0.0
// Class Description   :    
// ===============================================================================
using FKVoxelEngine.Base;
using System;
using System.IO;
// ===============================================================================
namespace FKVoxelClient.Base
{
    public static class FKPathFuncs
    {
        public static string PathConfig = Path.Combine(FKSystemFuncs.GetStorePath(), "Config");
        public static string PathSaves = Path.Combine(FKSystemFuncs.GetStorePath(), "Saves");
        public static string PathBackup = Path.Combine(FKSystemFuncs.GetStorePath(), "Backup");
        public static string PathResource = Path.Combine(FKSystemFuncs.GetStorePath(), "Content");
        public static string PathCrash = Path.Combine(FKSystemFuncs.GetStorePath(), "Crash");
        public static string PathLocalization = Path.Combine(FKSystemFuncs.GetStorePath(), "Localization");
    }
}