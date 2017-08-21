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
// Create Time         :    2017/7/26 14:29:52
// Update Time         :    2017/7/26 14:29:52
// Class Version       :    v1.0.0.0
// Class Description   :    
// ===============================================================================
using System;
// ===============================================================================
namespace FKVoxelEngine.Config
{
    public class FKEngineConfig
    {
        public FKChunkConfig Chunk { get; private set; }
        public FKCacheConfig Cache { get; private set; }
        public FKAudioConfig Audio { get; private set; }
        public FKDebugConfig Debug { get; private set; }
        public FKEffectConfig Effect { get; private set; }
        public FKGraphicsConfig Graphics { get; private set; }
        public FKWorldConfig World { get; private set; }

        private bool bIsAllConfigSuccessed = false;

        public FKEngineConfig()
        {
            Chunk = new FKChunkConfig();
            Cache = new FKCacheConfig();
            Audio = new FKAudioConfig();
            Debug = new FKDebugConfig();
            Effect = new FKEffectConfig();
            Graphics = new FKGraphicsConfig();
            World = new FKWorldConfig();
        }

        internal bool Setup()
        {
            if (!Chunk.Setup())
                return false;
            if (!Cache.Setup())
                return false;
            if (!Audio.Setup())
                return false;
            if (!Debug.Setup())
                return false;
            if (!Effect.Setup())
                return false;
            if (!Graphics.Setup())
                return false;
            if (!World.Setup())
                return false;

            bIsAllConfigSuccessed = true;
            return true;
        }

        public override string ToString()
        {
            if (!bIsAllConfigSuccessed)
                return "FKEngine config not all successed.";

            string ret = string.Empty;
            if (Chunk != null)
            {
                ret += $"====== Chunk ======\n";
                ret += Chunk.ToString();
            }
            if (Cache != null)
            {
                ret += $"====== Cache ======\n";
                ret += Cache.ToString();
            }
            if (Audio != null)
            {
                ret += $"====== Audio ======\n";
                ret += Audio.ToString();
            }
            if (Debug != null)
            {
                ret += $"====== Debug ======\n";
                ret += Debug.ToString();
            }
            if (Effect != null)
            {
                ret += $"====== Effect ======\n";
                ret += Effect.ToString();
            }
            if (Graphics != null)
            {
                ret += $"====== Graphics ======\n";
                ret += Graphics.ToString();
            }
            if (World != null)
            {
                ret += $"====== World ======\n";
                ret += World.ToString();
            }
            return ret;
        }
    }
}