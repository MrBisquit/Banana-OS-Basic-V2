using Cosmos.System;
using Cosmos.System.Graphics;
using Cosmos.System.Graphics.Fonts;
using IL2CPU.API.Attribs;
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
        public static bool isMenuOpen = false;
        public static bool isContextMenuOpen = false;

        [ManifestResourceStream(ResourceName = "Banana_OS_Basic_V2.Assets.Inverted.Shutdown.bmp")]
        static byte[] ShutdownRaw;
        //static Bitmap ShutdownIcon = new Bitmap(ShutdownRaw);

        [ManifestResourceStream(ResourceName = "Banana_OS_Basic_V2.Assets.Inverted.Restart.bmp")]
        static byte[] RestartRaw;
        //static Bitmap RestartIcon = new Bitmap(RestartRaw);
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

            if(System.Console.KeyAvailable)
            {
                ConsoleKeyInfo key = System.Console.ReadKey();
                if(key.Key == ConsoleKey.LeftWindows || key.Key == ConsoleKey.RightWindows)
                {
                    isMenuOpen = !isMenuOpen;
                } else if (key.Key == ConsoleKey.C)
                {
                    Cosmos.Core.INTs.IRQContext c = new Cosmos.Core.INTs.IRQContext();
                    Kernel.DoKernelPanic("0x0", "0x0", ref c, "0x00215");
                    Cosmos.Debug.Kernel.Debugger.SendKernelPanic(id: 0);
                }
            }

            button.RenderButton(canvas, 0, kernel.screenHeight - 40, 40, kernel.screenHeight - 40, " ", new Action(() => {
                isMenuOpen = !isMenuOpen;
            }), new Action(() => {
                isContextMenuOpen = !isContextMenuOpen;
            }), colorScheme);

            if(isMenuOpen)
            {
                canvas.DrawFilledRectangle(new Pen(Color.Black), new Cosmos.System.Graphics.Point(0, kernel.screenHeight - (40 + kernel.screenHeight / 2)), kernel.screenWidth / 3, kernel.screenHeight / 2);

                button.RenderButton(canvas, 0, kernel.screenHeight - (40 * 2), 40, 40, " ", new Action(() => {
                    kernel.BeginShutdown();
                }), new Action(() => { }), colorScheme);
                button.RenderButton(canvas, 0, kernel.screenHeight - (40 * 3), 40, 40, " ", new Action(() => {
                    kernel.BeginRestart();
                }), new Action(() => { }), colorScheme);

                try
                {
                    canvas.DrawImage(new Bitmap(ShutdownRaw, ColorOrder.BGR), new Cosmos.System.Graphics.Point(0, kernel.screenHeight - (40 * 2)));
                    canvas.DrawImage(new Bitmap(40, 40, RestartRaw, ColorDepth.ColorDepth32), new Cosmos.System.Graphics.Point(0, kernel.screenHeight - (40 * 3)));

                    /*canvas.DrawImage(new Bitmap(10, 10, ColorDepth.ColorDepth32), 10, 10, 16, 16);

                    canvas.Clear();
                    canvas.DrawString(ShutdownRaw.Length.ToString(), PCScreenFont.Default, new Pen(Color.White), new Cosmos.System.Graphics.Point(0, 12));
                    canvas.DrawString(RestartRaw.Length.ToString(), PCScreenFont.Default, new Pen(Color.White), new Cosmos.System.Graphics.Point(0, 12 * 2));*/
                } catch(Exception ex)
                {
                    canvas.Clear();
                    canvas.DrawString(ex.Message, PCScreenFont.Default, new Pen(Color.White), new Cosmos.System.Graphics.Point(0, 0));
                    canvas.Display();

                    return;
                }
            }
        }
    }
}
