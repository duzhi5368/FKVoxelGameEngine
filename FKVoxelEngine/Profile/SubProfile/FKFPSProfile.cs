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
// Create Time         :    2017/8/7 13:55:57
// Update Time         :    2017/8/7 13:55:57
// Class Version       :    v1.0.0.0
// Class Description   :    
// ===============================================================================
using System;
using Microsoft.Xna.Framework;
using FKVoxelEngine.Framework;
using System.Linq;
using FKVoxelEngine.Graphics;
// ===============================================================================
namespace FKVoxelEngine.Profile
{
    public class FKFPSProfile : FKDebugProfile
    {
        private IFKStatisticsService _StatisticsService;
        public FKFPSProfile(Game game, Rectangle bounds) : base(game, bounds)
        {
        }

        protected override void Initialize()
        {
            _StatisticsService = (IFKStatisticsService)Game.Services.GetService(typeof(IFKStatisticsService));
            base.Initialize();
        }

        public override void Update(GameTime gameTime)
        {
            ProfileValues.Add(_StatisticsService.FPS);

            if (ProfileValues.Count > ValueToProfile + 1)
                ProfileValues.RemoveAt(0);
            if (ProfileValues.Count <= 2)
                return;

            MaxValue = (int)ProfileValues.Max();
            AverageValue = (int)ProfileValues.Average();
            MinValue = (int)ProfileValues.Min();
            CurrentValue = _StatisticsService.FPS;

            if (!AdaptiveLimits)
                return;

            AdaptiveMax = MaxValue;
            AdaptiveMin = 0;
        }

        public override void DrawGraph(GameTime gameTime)
        {
            FKBasicShapesHelper.DrawSolidPolygon(PrimitiveBatch, BackgroundPolygen, 4, Color.Black, true);

            float x = Bounds.X;
            float deltaX = Bounds.Width / (float)ValueToProfile;
            float scaleY = Bounds.Bottom - (float)Bounds.Top;

            if (ProfileValues.Count <= 2)
                return;

            for(var i = ProfileValues.Count - 1; i > 0; --i)
            {
                var y1 = Bounds.Bottom - ((ProfileValues[i] / (AdaptiveMax - AdaptiveMin)) * scaleY);
                var y2 = Bounds.Bottom - ((ProfileValues[i - 1] / (AdaptiveMax - AdaptiveMin)) * scaleY);

                var x1 = new Vector2(MathHelper.Clamp(x, Bounds.Left, Bounds.Right), MathHelper.Clamp(y1, Bounds.Top, Bounds.Bottom));
                var x2 = new Vector2(MathHelper.Clamp(x + deltaX, Bounds.Left, Bounds.Right), MathHelper.Clamp(y2, Bounds.Top, Bounds.Bottom));

                FKBasicShapesHelper.DrawSegment(this.PrimitiveBatch, x1, x2, Color.DeepSkyBlue);

                x += deltaX;
            }
        }

        public override void DrawString(GameTime gameTime)
        {
            SpriteBatch.DrawString(SpriteFont, "FPS:" + CurrentValue, new Vector2(Bounds.Left, Bounds.Bottom), Color.White);
            SpriteBatch.DrawString(SpriteFont, "Max:" + MaxValue, new Vector2(Bounds.Left + 90, Bounds.Bottom), Color.White);
            SpriteBatch.DrawString(SpriteFont, "Avg:" + AverageValue, new Vector2(Bounds.Left + 150, Bounds.Bottom), Color.White);
            SpriteBatch.DrawString(SpriteFont, "Min:" + MinValue, new Vector2(Bounds.Left + 210, Bounds.Bottom), Color.White);
        }
    }
}