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
    public static class Taskbar
    {
        public static bool setupMode = false;
        public static void RenderTaskBar(Canvas canvas, Kernel kernel)
        {
            if (setupMode) return;
            canvas.DrawFilledRectangle(new Pen(Color.Black), new Cosmos.System.Graphics.Point(0, kernel.screenHeight - 40), kernel.screenWidth, 40);

            Button button = new Button();

            Button.ColorScheme colorScheme = new Button.ColorScheme();
            colorScheme.Text = Color.White;
            colorScheme.Normal = Color.LightGray;
            colorScheme.Hovered = Color.Gray;
            colorScheme.Clicked = Color.DarkGray;

            button.RenderButton(canvas, 0, kernel.screenHeight - 40, 40, kernel.screenHeight - 40, " ", new Action(() => { }), new Action(() => { }), colorScheme);
        }
    }
}
