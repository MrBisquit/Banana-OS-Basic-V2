using Banana_OS_Basic_V2.Window;
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

        [ManifestResourceStream(ResourceName = "Banana_OS_Basic_V2.Assets.Inverted.Settings.bmp")]
        static byte[] SettingsRaw;
        //static Bitmap SettingsIcon = new Bitmap(SettingsRaw);
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

            List<Window.Window> windows = WindowManager.GetWindows();

            for (int i = 0; i < windows.Count; i++)
            {
                button.RenderButton(canvas, 40 * (i + 1), kernel.screenHeight - 40, 40, 40, " ", new Action(() => {

                }), new Action(() => { }), colorScheme);
                canvas.DrawImageAlpha(windows[i].Icon, new Cosmos.System.Graphics.Point(40 * (i + 1), kernel.screenHeight - 40));
            }

            if (isMenuOpen)
            {
                canvas.DrawFilledRectangle(new Pen(Color.Black), new Cosmos.System.Graphics.Point(0, kernel.screenHeight - (40 + kernel.screenHeight / 2)), kernel.screenWidth / 3, kernel.screenHeight / 2);

                button.RenderButton(canvas, 0, kernel.screenHeight - (40 * 2), 40, 40, " ", new Action(() => {
                    kernel.BeginShutdown();
                }), new Action(() => { }), colorScheme);
                button.RenderButton(canvas, 0, kernel.screenHeight - (40 * 3), 40, 40, " ", new Action(() => {
                    kernel.BeginRestart();
                }), new Action(() => { }), colorScheme);
                button.RenderButton(canvas, 0, kernel.screenHeight - (40 * 4), 40, 40, " ", new Action(() => {
                    Window.Window window = WindowManager.CreateWindow(WindowType.User_Window, "Settings", "Settings");
                    window.Icon = new Bitmap(SettingsRaw);
                }), new Action(() => { }), colorScheme);

                try
                {
                    canvas.DrawImageAlpha(new Bitmap(ShutdownRaw), new Cosmos.System.Graphics.Point(0, kernel.screenHeight - (40 * 2)));
                    canvas.DrawImageAlpha(new Bitmap(RestartRaw), new Cosmos.System.Graphics.Point(0, kernel.screenHeight - (40 * 3)));
                    canvas.DrawImageAlpha(new Bitmap(SettingsRaw), new Cosmos.System.Graphics.Point(0, kernel.screenHeight - (40 * 4)));

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
