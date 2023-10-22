using Banana_OS_Basic_V2.Window;
using Cosmos.System.Graphics;
using Cosmos.System.Graphics.Fonts;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Banana_OS_Basic_V2
{
    public static class Boot
    {
        public static void DoChecks(Kernel kernel)
        {
            Console.Clear();
            ShowInfo("Doing file checks...");

            DriveInfo[] drives = DriveInfo.GetDrives();
            foreach (DriveInfo drive in drives)
            {
                ShowInfo(drive.Name + " Exists.");
            }

            if(Directory.Exists(@"0:\Users\"))
            {
                ShowOK("User folder exists.");
            } else
            {
                ShowFail("User folder does not exist. Setup will be run.");
                kernel.isSetupMode = true;
                Directory.CreateDirectory(@"0:\Users\");
                ShowOK("Created User folder.");
            }

            ShowInfo("Finished file checks.");

            ShowInfo("Checking Kernel...");
            ShowInfo($"Screen Height: {kernel.screenHeight} Screen Width: {kernel.screenWidth}");
            ShowOK("Kernel check completed successfully.");

            ShowOK("Boot checks completed successfully.");

            ShowInfo("Starting UI...");
            //Console.ReadKey();

            /*Console.WriteLine("Doing file checks...");
            if (Directory.Exists(@"0:\Users\"))
            {
                Console.WriteLine("User folder exists.");
            }
            else
            {
                Console.WriteLine("User folder does not exist. Setup will be run.");
                kernel.isSetupMode = true;
            }
            Console.WriteLine("Finished file checks.");*/
        }

        public static void ShowOK(string text)
        {
            Console.ResetColor();
            Console.Write("[  ");
            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write("OK");
            Console.ResetColor();
            Console.WriteLine("  ] " + text);
        }

        public static void ShowFail(string text)
        {
            Console.ResetColor();
            Console.Write("[ ");
            Console.ForegroundColor = ConsoleColor.DarkRed;
            Console.Write("FAIL");
            Console.ResetColor();
            Console.WriteLine(" ] " + text);
        }

        public static void ShowInfo(string text)
        {
            Console.ResetColor();
            Console.Write("[ ");
            Console.ForegroundColor = ConsoleColor.DarkBlue;
            Console.Write("INFO");
            Console.ResetColor();
            Console.WriteLine(" ] " + text);
        }

        public static void ShowBootScreen(Canvas canvas, Kernel kernel)
        {
            canvas.Clear();
            canvas.DrawString($"Banana OS Basic V2", PCScreenFont.Default, new Pen(Color.White), new Cosmos.System.Graphics.Point((kernel.screenWidth / 2) - "Banana OS Basic V2".Length, kernel.screenHeight / 2));

            string bottomtext = "Press F1 to boot into recovery or F2 to reset";
            canvas.DrawString(bottomtext, PCScreenFont.Default, new Pen(Color.White), new Cosmos.System.Graphics.Point((kernel.screenWidth / 2) - (bottomtext.Length * 3), kernel.screenHeight - 14));

            canvas.Display();

            System.Threading.Thread.Sleep(2500);
            if(Console.KeyAvailable)
            {
                ConsoleKey key = Console.ReadKey().Key;
            }

            canvas.Clear();
            canvas.Display();
            System.Threading.Thread.Sleep(500);
        }

        public static void ShowShuttingDownScreen(Canvas canvas, Kernel kernel)
        {
            canvas.Clear();
            canvas.DrawString($"Banana OS Basic V2", PCScreenFont.Default, new Pen(Color.White), new Cosmos.System.Graphics.Point((kernel.screenWidth / 2) - "Banana OS Basic V2".Length, kernel.screenHeight / 2));
            canvas.DrawString($"Shutting down...", PCScreenFont.Default, new Pen(Color.White), new Cosmos.System.Graphics.Point((kernel.screenWidth / 2) - "Shutting down...".Length, (kernel.screenHeight / 2) + 14));
        }

        public static void ShowRestartingScreen(Canvas canvas, Kernel kernel)
        {
            canvas.Clear();
            canvas.DrawString($"Banana OS Basic V2", PCScreenFont.Default, new Pen(Color.White), new Cosmos.System.Graphics.Point((kernel.screenWidth / 2) - "Banana OS Basic V2".Length, kernel.screenHeight / 2));
            canvas.DrawString($"Restarting...", PCScreenFont.Default, new Pen(Color.White), new Cosmos.System.Graphics.Point((kernel.screenWidth / 2) - "Restarting...".Length, (kernel.screenHeight / 2) + 14));
        }
    }
}
