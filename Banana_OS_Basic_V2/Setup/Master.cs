using Banana_OS_Basic_V2.UI;
using Cosmos.System;
using Cosmos.System.Graphics;
using Cosmos.System.Graphics.Fonts;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sys = Cosmos.System;

namespace Banana_OS_Basic_V2.Setup
{
    public static class Master
    {
        public static Window.Window SetupWindow = new Window.Window(Window.WindowType.System_Window, "Setup", "Banana OS Basic Setup");
        public static void Render(Canvas canvas, Kernel kernel)
        {
            SetupWindow.RenderWindow(canvas, kernel);

            MouseManager.ScreenWidth = (uint)kernel.screenWidth;
            MouseManager.ScreenHeight = (uint)kernel.screenHeight;

            canvas.DrawFilledRectangle(new Pen(Color.White), new Sys.Graphics.Point(20, 20), kernel.screenWidth - 40, kernel.screenHeight - 40);
            canvas.DrawString("Banana OS Basic Setup", PCScreenFont.Default, new Pen(Color.Black), new Cosmos.System.Graphics.Point((kernel.screenWidth / 2) - "Banana OS Basic Setup".Length * 4, 35));

            UI.Button.ColorScheme skipButtonColors = new UI.Button.ColorScheme()
            {
                Normal = Color.LightGray,
                Hovered = Color.DimGray,
                Clicked = Color.DarkGray,
                Text = Color.White
            };

            Action skipButtonLeftClick = new Action(() =>
            {
                canvas.DrawFilledRectangle(new Pen(Color.White), new Sys.Graphics.Point(20, 20), kernel.screenWidth - 40, kernel.screenHeight - 40);
                kernel.isSetupMode = false;
            });

            UI.Button.RenderButton(canvas, 20, kernel.screenHeight - 60, 100, 14, "Skip setup", skipButtonLeftClick, new Action(() => { }), skipButtonColors);

            ScreenResize.Render(canvas, kernel);
        }
    }
}
