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
// Create Time         :    2017/7/26 18:05:57
// Update Time         :    2017/7/26 18:05:57
// Class Version       :    v1.0.0.0
// Class Description   :    
// ===============================================================================
using FKVoxelEngine.Asset;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
// ===============================================================================
namespace FKVoxelEngine.Debug
{
    public class FKGameConsole
    {
        public FKGameConsoleOptions Options { get { return FKGameConsoleOptions.GetInstance; } }
        public bool Enabled { get; set; }
        public bool Opened { get { return Console.IsOpen; } }

        private readonly FKGameConsoleComponent Console;


        public FKGameConsole(Game game)
        {
            if(FKGameConsoleOptions.GetInstance.Font == null)
            {
                FKGameConsoleOptions.GetInstance.Font = game.Content.Load<SpriteFont>(FKEngineAssets.Font_ConsoleFont);
            }
            if (FKGameConsoleOptions.GetInstance.RoundedCorner == null)
            {
                FKGameConsoleOptions.GetInstance.RoundedCorner = game.Content.Load<Texture2D>(FKEngineAssets.Texture_RoundedCorner);
            }

            Enabled = true;
            SpriteBatch spriteBatch = new SpriteBatch(game.GraphicsDevice);
            Console = new FKGameConsoleComponent(this, game, spriteBatch);
            game.Services.AddService(typeof(FKGameConsole), this);
            game.Components.Add(Console);
        }
    }
}