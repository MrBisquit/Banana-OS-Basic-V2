using System;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Reflection.Metadata;
using Banana_OS_Basic_V2.Window;
using Cosmos.Core.IOGroup;
using Cosmos.System;
using Cosmos.System.Graphics;
using Cosmos.System.Graphics.Fonts;
using Sys = Cosmos.System;

namespace Banana_OS_Basic_V2
{
    public class Kernel : Sys.Kernel
    {
        Canvas canvas;
        //public static Mouse m = new Mouse();

        // Screen properties
        public int screenWidth = 800;
        public int screenHeight = 600;

        public bool isSetupMode = false;

        public int tick = 0;

        public int fps = 0;
        Sys.FileSystem.CosmosVFS fs = new Sys.FileSystem.CosmosVFS();
        protected override void BeforeRun()
        {
            System.Console.Write("Registering FileSystem.");
            Sys.FileSystem.VFS.VFSManager.RegisterVFS(fs);
            System.Console.Write("\rRegistered FileSystem.\n");

            System.Console.WriteLine("Doing file checks...");
            if(Directory.Exists(@"0:\Users\"))
            {
                System.Console.WriteLine("User folder exists.");
            } else
            {
                System.Console.WriteLine("User folder does not exist. Setup will be run.");
                isSetupMode = true;
            }
            System.Console.WriteLine("Finished file checks.");
            //System.Console.WriteLine("Cosmos booted successfully. Type a line of text to get it echoed back.");

            //System.Console.WriteLine("Banana OS Basic booted successfully. Press any key to boot into the GUI.");
            //System.Console.ReadKey();

            canvas = FullScreenCanvas.GetFullScreenCanvas();
            canvas.Mode = new Mode(screenWidth, screenHeight, ColorDepth.ColorDepth32);
            MouseManager.ScreenWidth = (uint)screenWidth;
            MouseManager.ScreenHeight = (uint)screenHeight;

            //isSetupMode = true;
        }

        protected override void Run()
        {
            WindowManager.CreateWindow(WindowType.User_Window, "Test", "Another test");
            tick++;

            /*if(tick > 150)
            {
                isSetupMode = true;
            } else if(tick == 170)
            {
                UI.Topbar.hidden = true;
            }*/

            try
            {
                canvas.DrawString(tick.ToString(), PCScreenFont.Default, new Pen(Color.Black), 15, 15);

                UI.Topbar.setupMode = isSetupMode;
                UI.Taskbar.setupMode = isSetupMode;

                canvas.Clear(Color.SkyBlue);
                //canvas.DrawFilledRectangle(new Pen(Color.White), new Sys.Graphics.Point(10, 10), screenWidth - 20, screenHeight - 20);
                UI.Topbar.RenderTopbar(canvas, screenWidth);
                UI.Taskbar.RenderTaskBar(canvas, this);

                canvas.DrawString($"FPS: {tick / (DateTime.Now.Second + 1)}", PCScreenFont.Default, new Pen(Color.White), new Cosmos.System.Graphics.Point(0, 0));

                if(isSetupMode)
                {
                    Setup.Master.Render(canvas, this);
                }

                Mouse.DisplayMouse(canvas);

                canvas.Display();
            }
            catch (Exception e)
            {
                mDebugger.Send("Exception occurred: " + e.Message);
                mDebugger.Send(e.Message);
                canvas.DrawString(e.Message, PCScreenFont.Default, new Pen(Color.Black), new Cosmos.System.Graphics.Point((screenWidth / 2) - e.Message.Length * 4, 115));

                System.Console.Clear();
                //Stop();
            }
        }

        public void ResizeScreen(int width, int height)
        {
            screenWidth = width;
            screenHeight = height;

            canvas = FullScreenCanvas.GetFullScreenCanvas(new Mode(screenWidth, screenHeight, ColorDepth.ColorDepth32));
            canvas.Mode = new Mode(screenWidth, screenHeight, ColorDepth.ColorDepth32);
            MouseManager.ScreenWidth = (uint)screenWidth;
            MouseManager.ScreenHeight = (uint)screenHeight;
        }
    }
}
