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
// Create Time         :    2017/7/26 18:14:15
// Update Time         :    2017/7/26 18:14:15
// Class Version       :    v1.0.0.0
// Class Description   :    
// ===============================================================================
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
// ===============================================================================
namespace FKVoxelEngine.Debug
{
    internal class FKGameConsoleComponent : DrawableGameComponent
    {
        private readonly FKGameConsole _Console;
        private readonly SpriteBatch _SpriteBatch;
        private readonly FKGameConsoleInputProcessor _InputProcessor;
        private readonly FKGameConsoleRenderer _Renderer;

        public FKGameConsoleComponent(FKGameConsole console, Game game, SpriteBatch spriteBatch)
            :base(game)
        {
            this._Console = console;
            this._SpriteBatch = spriteBatch;
            _InputProcessor = new FKGameConsoleInputProcessor(new FKCommandProcessor());
            _InputProcessor.Open += (s, e) => _Renderer.Open();
            _InputProcessor.Close += (s, e) => _Renderer.Close();
            _Renderer = new FKGameConsoleRenderer(game, spriteBatch, _InputProcessor);
        }

        public override void Draw(GameTime gameTime)
        {
            if (!_Console.Enabled)
                return;
            _SpriteBatch.Begin();
            _Renderer.Draw(gameTime);
            _SpriteBatch.End();
            base.Draw(gameTime);
        }

        public override void Update(GameTime gameTime)
        {
            if (!_Console.Enabled)
                return;
            _Renderer.Update(gameTime);
            base.Update(gameTime);
        }

        public bool IsOpen {
            get { return _Renderer.IsOpen; }
        }
    }
}