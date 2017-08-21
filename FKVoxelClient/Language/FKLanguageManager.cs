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
// Create Time         :    2017/8/18 19:44:15
// Update Time         :    2017/8/18 19:44:15
// Class Version       :    v1.0.0.0
// Class Description   :    
// ===============================================================================
using FKVoxelClient.Base;
using FKVoxelEngine.Base;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.IO;
// ===============================================================================
namespace FKVoxelClient.Language
{
    public class FKLanguageManager : GameComponent, IFKLanguageManagerService
    {
        private string curLanguage;
        private FKTranslatedString[] strings;
        private int stringsMax;
        private int stringsCount;
        private string[] loadedLanguages;
        private int loadedLanguagesMax;
        private int loadedLanguagesCount;

        private static readonly FKLogger _Logger = FKLogManager.CreateLogger("FKLanguageManager");
        public FKLanguageManager(Game game)
            : base(game)
        {
            stringsMax = 1024 * 32;
            stringsCount = 0;
            strings = new FKTranslatedString[stringsMax];
            loadedLanguagesCount = 0;
            loadedLanguagesMax = 32;
            loadedLanguages = new string[loadedLanguagesMax];

            Game.Services.AddService(typeof(IFKLanguageManagerService), this);
        }

        public override void Initialize()
        {
            _Logger.Trace("Init()");

            LoadTranslations();

            base.Initialize();
        }
        public void Add(string strLanguage, string id, string translated)
        {
            if (IsNewLanguage(strLanguage))
            {
                if(loadedLanguagesCount < loadedLanguagesMax)
                {
                    loadedLanguages[loadedLanguagesCount] = strLanguage;
                    loadedLanguagesCount++;
                }
            }

            if (stringsCount > stringsMax)
                return;
            if (IsContainsTranslation(strLanguage, id))
                return;

            FKTranslatedString s = new FKTranslatedString();
            s.language = strLanguage;
            s.id = id;
            s.translated = translated;
            strings[stringsCount++] = s;
        }

        public void Override(string strLanguage, string id, string translated)
        {
            if (IsNewLanguage(strLanguage))
            {
                if(loadedLanguagesCount < loadedLanguagesMax)
                {
                    loadedLanguages[loadedLanguagesCount] = strLanguage;
                    loadedLanguagesCount++;
                }
            }

            if(!IsContainsTranslation(strLanguage, id))
            {
                Add(strLanguage, id, translated);
            }
            else
            {
                int replaceIndex = -1;
                for(int i = 0; i < stringsCount; ++i)
                {
                    if (strings[i] == null)
                        continue;
                }

                if(replaceIndex != -1)
                {
                    FKTranslatedString s = new FKTranslatedString();
                    s.language = strLanguage;
                    s.id = id;
                    s.translated = translated;
                    strings[replaceIndex] = s;
                }
            }
        }

        public FKTranslatedString[] AllStrings()
        {
            throw new NotImplementedException();
        }

        public string Get(string id)
        {
            string currentLanguage = GetUsingLanguage();

            for (int i = 0; i < stringsCount; ++i)
            {
                if (strings[i] == null)
                    continue;

                if (strings[i].id == id && strings[i].language == currentLanguage)
                {
                    return strings[i].translated;
                }
            }

            // 没办法了，找英语
            for (int i = 0; i < stringsCount; ++i)
            {
                if (strings[i] == null)
                    continue;

                if (strings[i].id == id && strings[i].language == "en")
                {
                    return strings[i].translated;
                }
            }

            // 彻底误解
            return id;
        }

        public string GetUsingLanguage()
        {
            string strCurrentLanguage = "en";
            if (!string.IsNullOrEmpty(curLanguage))
            {
                strCurrentLanguage = curLanguage;
            }
            else
            {
                strCurrentLanguage = FKSystemFuncs.GetLanguageIso6391();
            }
            return strCurrentLanguage;
        }

        public void LoadTranslations()
        {
            List<string> fileList = FKFileFuncs.GetFilesByExtensionRecursive(FKPathFuncs.PathLocalization, ".txt");
            for(int i = 0; i < fileList.Count; ++i)
            {
                string[] lines = File.ReadAllLines(fileList[i]);
                for(int j = 0; j < lines.Length; ++j)
                {
                    if (string.IsNullOrEmpty(lines[j]))
                        continue;

                    string[] ret = lines[j].Split(new char[] { '=' });
                    if(ret.Length >= 2)
                    {
                        Add(lines[0], ret[0], ret[1]);
                    }
                }
            }

            // 无论如何，英文保底
            FKLanguage_Default.AddDefault();
        }

        public string NextLanguage()
        {
            if (curLanguage == null)
                curLanguage = "en";

            int languageIndex = -1;
            for(int i = 0; i < loadedLanguagesMax; ++i)
            {
                if (loadedLanguages[i] == null)
                    continue;
                if (loadedLanguages[i] == curLanguage)
                    languageIndex = i;
            }

            if (languageIndex < 0)
                languageIndex = 0;

            languageIndex++;
            if (languageIndex >= loadedLanguagesMax || languageIndex >= loadedLanguagesCount)
                languageIndex = 0;

            curLanguage = loadedLanguages[languageIndex];
            return curLanguage;
        }

        private bool IsNewLanguage(string language)
        {
            for(int i = 0; i < loadedLanguagesMax; ++i)
            {
                if (loadedLanguages[i] == null)
                    continue;
                if (loadedLanguages[1] == language)
                    return false;
            }
            return true;
        }

        private bool IsContainsTranslation(string language, string id)
        {
            for(int i = 0; i < stringsCount; ++i)
            {
                if (strings[i] == null)
                    continue;
                if (strings[i].language == language)
                {
                    if (strings[i].id == id)
                        return true;
                }
            }
            return false;
        }
    }
}