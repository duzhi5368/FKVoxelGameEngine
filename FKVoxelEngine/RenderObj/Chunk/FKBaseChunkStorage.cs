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
// Create Time         :    2017/8/2 13:54:34
// Update Time         :    2017/8/2 13:54:34
// Class Version       :    v1.0.0.0
// Class Description   :    
// ===============================================================================
using FKVoxelEngine.Base;
using FKVoxelEngine.Framework;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
// ===============================================================================
namespace FKVoxelEngine.RenderObj
{
    public class FKBaseChunkStorage : GameComponent, IFKBaseChunkStorageService
    {
        private readonly FKDoubleIndexedDictionary<FKBaseChunk> _Dic = new FKDoubleIndexedDictionary<FKBaseChunk>();
        private static readonly FKLogger Logger = FKLogManager.CreateLogger("FKBaseChunkStorage");


        public FKBaseChunkStorage(Game game)
            : base(game)
        {
            Game.Services.AddService(typeof(IFKBaseChunkStorageService), this);
        }

        public override void Initialize()
        {
            Logger.Trace("Init()");

            base.Initialize();
        }

        public int Count
        {
            get
            {
                return _Dic.Count;
            }
        }

        public IEnumerable<FKBaseChunk> Values
        {
            get
            {
                return _Dic.Values;
            }
        }

        public FKVector2Int SouthWestEdge { get; set; }

        public FKVector2Int NorthEastEdge { get; set; }

        public FKBaseChunk this[int x, int z]
        {
            get
            {
                return _Dic[x, z];
            }

            set
            {
                _Dic[x, z] = value;
            }
        }

        public FKBaseChunk Remove(int x, int z)
        {
            return _Dic.Remove(x, z);
        }

        public bool ContainsKey(int x, int z)
        {
            return _Dic.ContainsKey(x, z);
        }
    }
}