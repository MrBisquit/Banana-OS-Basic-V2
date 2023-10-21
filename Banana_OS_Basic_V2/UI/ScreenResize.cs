using Cosmos.System.Graphics.Fonts;
using Cosmos.System.Graphics;
using Cosmos.System;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sys = Cosmos.System;

namespace Banana_OS_Basic_V2.UI
{
    public static class ScreenResize
    {
        public static void Render(Canvas canvas, Kernel kernel)
        {
            MouseManager.ScreenWidth = (uint)kernel.screenWidth;
            MouseManager.ScreenHeight = (uint)kernel.screenHeight;
            string text = "Use the left/right arrow keys to resize the screen.";
            canvas.DrawString(text, PCScreenFont.Default, new Pen(Color.Black), new Cosmos.System.Graphics.Point((kernel.screenWidth / 2) - text.Length * 4, 65));
            string text2 = $"Current size: {kernel.screenWidth}x{kernel.screenHeight}";
            canvas.DrawString(text2, PCScreenFont.Default, new Pen(Color.Black), new Cosmos.System.Graphics.Point((kernel.screenWidth / 2) - text2.Length * 4, 85));

            if(System.Console.KeyAvailable)
            {
                ConsoleKeyInfo key = System.Console.ReadKey();

                //try
                //{
                    if (key.Key == ConsoleKey.LeftArrow)
                    {
                        kernel.ResizeScreen(kernel.screenWidth - 100, kernel.screenHeight - 100);
                    }
                    else if (key.Key == ConsoleKey.RightArrow)
                    {
                        kernel.ResizeScreen(kernel.screenWidth + 100, kernel.screenHeight + 100);
                    }
                //} catch
                //{
                    //kernel.isSetupMode = false;
                //}
            }
        }
    }
}
