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
// Create Time         :    2017/8/13 16:02:01
// Update Time         :    2017/8/13 16:02:01
// Class Version       :    v1.0.0.0
// Class Description   :    
// ===============================================================================
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
// ===============================================================================
namespace FKVoxelEngine.Graphics
{
    public enum Alignment
    {
        None = 0,

        Left = 1,
        Right = 2,
        HorizontalCenter = 4,

        Top = 8,
        Bottom = 16,
        VerticalCenter = 32,

        // 组合
        TopLeft = Top | Left,
        TopRight = Top | Right,
        TopCenter = Top | HorizontalCenter,

        BottomLeft = Bottom | Left,
        BottomRight = Bottom | Right,
        BottomCenter = Bottom | HorizontalCenter,

        CenterLeft = VerticalCenter | Left,
        CenterRight = VerticalCenter | Right,
        Center = VerticalCenter | HorizontalCenter
    }
    public struct FKLayout
    {
        public Rectangle ClientArea;
        public Rectangle SafeArea;

        public FKLayout(Rectangle clientArea, Rectangle safeArea)
        {
            ClientArea = clientArea;
            SafeArea = safeArea;
        }

        public FKLayout(Rectangle clientArea)
            : this(clientArea, clientArea)
        {

        }

        public FKLayout(Viewport viewport)
        {
            ClientArea = new Rectangle((int)viewport.X, (int)viewport.Y, (int)viewport.Width, (int)viewport.Height);
            SafeArea = viewport.TitleSafeArea;
        }

        public Vector2 Place(Vector2 size, float horizontalMargin, float verticalMargin, Alignment alignment)
        {
            Rectangle rc = new Rectangle(0, 0, (int)size.X, (int)size.Y);
            rc = Place(rc, horizontalMargin, verticalMargin, alignment);
            return new Vector2(rc.X, rc.Y);
        }

        public Rectangle Place(Rectangle region, float horizontalMargin,
                                    float verticalMargine, Alignment alignment)
        {
            // 横向布局
            if ((alignment & Alignment.Left) != 0)
            {
                region.X = ClientArea.X + (int)(ClientArea.Width * horizontalMargin);
            }
            else if ((alignment & Alignment.Right) != 0)
            {
                region.X = ClientArea.X +
                            (int)(ClientArea.Width * (1.0f - horizontalMargin)) -
                            region.Width;
            }
            else if ((alignment & Alignment.HorizontalCenter) != 0)
            {
                region.X = ClientArea.X + (ClientArea.Width - region.Width) / 2 +
                            (int)(horizontalMargin * ClientArea.Width);
            }
            else
            {
                // 不处理
            }

            // 纵向布局
            if ((alignment & Alignment.Top) != 0)
            {
                region.Y = ClientArea.Y + (int)(ClientArea.Height * verticalMargine);
            }
            else if ((alignment & Alignment.Bottom) != 0)
            {
                region.Y = ClientArea.Y +
                            (int)(ClientArea.Height * (1.0f - verticalMargine)) -
                            region.Height;
            }
            else if ((alignment & Alignment.VerticalCenter) != 0)
            {
                region.Y = ClientArea.Y + (ClientArea.Height - region.Height) / 2 +
                            (int)(verticalMargine * ClientArea.Height);
            }
            else
            {
                // 不处理
            }

            // 保证布局区域为安全区域（不包含Title的区域）
            if (region.Left < SafeArea.Left)
                region.X = SafeArea.Left;

            if (region.Right > SafeArea.Right)
                region.X = SafeArea.Right - region.Width;

            if (region.Top < SafeArea.Top)
                region.Y = SafeArea.Top;

            if (region.Bottom > SafeArea.Bottom)
                region.Y = SafeArea.Bottom - region.Height;

            return region;
        }
    }
}