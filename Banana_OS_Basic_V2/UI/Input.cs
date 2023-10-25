using Cosmos.System;
using Cosmos.System.Graphics;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Banana_OS_Basic_V2.UI.Button;

namespace Banana_OS_Basic_V2.UI
{
    public class Input : UIElement
    {
        public StringBuilder input = new StringBuilder();
        public int cursorPos = 0;
        public string placeholder = "";
        public Action LeftClick = delegate { };
        public Action RightClick = delegate { };
        public ColorScheme colors = new ColorScheme();
        public bool Disabled = false;
        public bool IsHiddenChars = false; // Aka password field (Replaces characters with '*')

        private bool selected = false;
        public override void Render(Canvas canvas, Kernel kernel)
        {
            bool hovered = IsPointInsideRectangle((int)MouseManager.X, (int)MouseManager.Y, x, y, x + width, y + height);
            bool clicked = !Mouse.LastStateLC && Mouse.StateLC;

            if(!hovered && clicked)
            {
                selected = true;
            } else if(hovered && clicked)
            {
                selected = true;
            }

            Pen pen = new Pen(colors.Normal);

            if(selected)
            {
                pen = new Pen(colors.Clicked);
            } else if(hovered)
            {
                pen = new Pen(colors.Hovered);
            }

            if(Disabled)
            {
                pen = new Pen(colors.Disabled);
            }

            canvas.DrawFilledRectangle(pen, new Cosmos.System.Graphics.Point(x, y), width, height);

            if(selected && DateTime.Now.Second % 3 == 0)
            {
                canvas.DrawLine(new Pen(colors.Text), new Cosmos.System.Graphics.Point(x + 2 + (cursorPos * 8), y), new Cosmos.System.Graphics.Point(x + 2 + (cursorPos * 8), y + height));
            }
        }
        public class ColorScheme
        {
            public Color Normal { get; set; }
            public Color Hovered { get; set; }
            public Color Clicked { get; set; }
            public Color Disabled { get; set; }

            public Color Text { get; set; }
        }

        // ChatGPT because I couldn't figure it out.
        static bool IsPointInsideRectangle(int x, int y, int x1, int y1, int x2, int y2)
        {
            return x >= x1 && x <= x2 && y >= y1 && y <= y2;
        }
    }
}
