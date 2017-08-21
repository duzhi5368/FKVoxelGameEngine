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
// Create Time         :    2017/8/7 13:20:59
// Update Time         :    2017/8/7 13:20:59
// Class Version       :    v1.0.0.0
// Class Description   :    
// ===============================================================================
using FKVoxelEngine.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
// ===============================================================================
namespace FKVoxelEngine.Profile
{
    public class FKDebugProfile
    {
        protected Game Game { get; private set; }
        protected Rectangle Bounds { get; private set; }
        public bool AdaptiveLimits { get; set; }
        public int ValueToProfile { get; private set; }
        protected readonly List<float> ProfileValues = new List<float>();
        protected readonly Vector2[] BackgroundPolygen = new Vector2[4];
        protected int MaxValue;
        protected int AverageValue;
        protected int MinValue;
        protected int CurrentValue;
        protected int AdaptiveMin;
        protected int AdaptiveMax = 1000;
        protected FKPrimitiveBatch PrimitiveBatch;
        protected SpriteBatch SpriteBatch;
        protected SpriteFont SpriteFont;
        protected Matrix LocalProjection;
        protected Matrix LocalView;

        public FKDebugProfile(Game game, Rectangle bounds)
        {
            Game = game;
            Bounds = bounds;
            AdaptiveLimits = true;
            ValueToProfile = 2500;
        }

        public void AttachGraphics(FKPrimitiveBatch primitiveBatch, SpriteBatch spriteBatch, SpriteFont spriteFont, Matrix localProjection, Matrix localView)
        {
            PrimitiveBatch = primitiveBatch;
            SpriteBatch = spriteBatch;
            SpriteFont = spriteFont;
            LocalProjection = localProjection;
            LocalView = localView;

            Initialize();
            LoadContent();
        }

        protected virtual void Initialize() { }

        public virtual void LoadContent()
        {
            BackgroundPolygen[0] = new Vector2(Bounds.X - 2, Bounds.Y - 2);
            BackgroundPolygen[1] = new Vector2(Bounds.X - 2, Bounds.Y + Bounds.Height + 14);
            BackgroundPolygen[2] = new Vector2(Bounds.X + 2 + Bounds.Width, Bounds.Y + Bounds.Height + 14);
            BackgroundPolygen[3] = new Vector2(Bounds.X + 2 + Bounds.Width, Bounds.Y - 2);
        }

        public virtual void Update(GameTime gameTime) { }

        public virtual void DrawString(GameTime gameTime) { }

        public virtual void DrawGraph(GameTime gameTime) { }
    }
}