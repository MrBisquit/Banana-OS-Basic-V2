using Cosmos.System;
using Cosmos.System.Graphics;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Banana_OS_Basic_V2.UI
{
    public class Button : UIElement
    {
        public void RenderButton(Canvas canvas, int x, int y, int width, int height, string content, Action leftClick, Action rightClick, ColorScheme colors)
        {
            bool hovered = IsPointInsideRectangle((int)MouseManager.X, (int)MouseManager.Y, x, y, x + width, y + height);
            bool leftClicked = hovered && MouseManager.MouseState == MouseState.Left;
            bool rightClicked = hovered && MouseManager.MouseState == MouseState.Right;
            bool clicked = leftClicked || rightClicked;

            Pen pen = new Pen(colors.Normal);

            if(hovered)
            {
                pen = new Pen(colors.Hovered);
            } else if(clicked)
            {
                pen = new Pen(colors.Clicked);
            }

            canvas.DrawFilledRectangle(pen, new Cosmos.System.Graphics.Point(x, y), width, height);
            canvas.DrawString(content, Cosmos.System.Graphics.Fonts.PCScreenFont.Default, new Pen(colors.Text), new Cosmos.System.Graphics.Point(x, y));

            if (leftClicked) leftClick();
            if (rightClicked) rightClick();

            if (clicked)
            {
                System.Threading.Thread.Sleep(100);
            }

            //bool testHover = IsPointInsideRectangle(x + 2, y + 2, x, y, x + width, y + height);
            //canvas.DrawString($"X: {(int)MouseManager.X} Y: {(int)MouseManager.Y} {hovered} {clicked} {testHover}", Cosmos.System.Graphics.Fonts.PCScreenFont.Default, new Pen(colors.Text), new Cosmos.System.Graphics.Point(0, 0));
        }

        public class ColorScheme
        {
            public Color Normal { get; set; }
            public Color Hovered { get; set; }
            public Color Clicked { get; set; }

            public Color Text { get; set; }
        }

        // ChatGPT because I couldn't figure it out.
        static bool IsPointInsideRectangle(int x, int y, int x1, int y1, int x2, int y2)
        {
            return x >= x1 && x <= x2 && y >= y1 && y <= y2;
        }
    }
}
