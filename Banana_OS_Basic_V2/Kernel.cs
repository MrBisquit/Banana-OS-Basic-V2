using System;
using System.ComponentModel.Design;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Reflection.Metadata;
using Banana_OS_Basic_V2.Window;
using Cosmos.Core.IOGroup;
using Cosmos.HAL;
using Cosmos.System;
using Cosmos.System.Graphics;
using Cosmos.System.Graphics.Fonts;
using IL2CPU.API.Attribs;
using static Cosmos.Core.INTs;
using Sys = Cosmos.System;

namespace Banana_OS_Basic_V2
{
    public class Kernel : Sys.Kernel
    {
        Canvas canvas;
        //public static Mouse m = new Mouse();

        // Screen properties
        public int screenWidth = 1920;//800;
        public int screenHeight = 1080;//600;

        public bool isSetupMode = false;

        public int tick = 0;

        public int fps = 0;
        Sys.FileSystem.CosmosVFS fs = new Sys.FileSystem.CosmosVFS();

        private bool renderNormal = true;
        private bool IsShuttingDown = false;
        private bool IsRestarting = false;

        private bool IsResizing = false; // Makes the screen go black for a few ticks to the user doesen't see it rerendering.

        public static bool KernelPanic = false;
        public static KernelPanicInfo KPI = new KernelPanicInfo();
        protected override void BeforeRun()
        {
            System.Console.Write("Registering FileSystem.");
            Sys.FileSystem.VFS.VFSManager.RegisterVFS(fs);
            System.Console.Write("\rRegistered FileSystem.\n");

            Boot.DoChecks(this);
            //System.Console.WriteLine("Cosmos booted successfully. Type a line of text to get it echoed back.");

            //System.Console.WriteLine("Banana OS Basic booted successfully. Press any key to boot into the GUI.");
            //System.Console.ReadKey();

            canvas = FullScreenCanvas.GetFullScreenCanvas();
            canvas.Mode = new Mode(screenWidth, screenHeight, ColorDepth.ColorDepth32);
            MouseManager.ScreenWidth = (uint)screenWidth;
            MouseManager.ScreenHeight = (uint)screenHeight;

            //isSetupMode = true;

            Boot.ShowBootScreen(canvas, this);
            canvas.Display();

            Cosmos.Core.CPU.EnableInterrupts();
        }

        // https://github.com/CrystalOSDevelopment/CrystalOS_2.0/blob/main/CrystalOS2/Kernel.cs#L110
        public static int FPS = 0;

        public static int LastS = -1;
        public static int Ticken = 0;

        public static void Update()
        {
            if (LastS == -1)
            {
                LastS = DateTime.UtcNow.Second;
            }
            if (DateTime.UtcNow.Second - LastS != 0)
            {
                if (DateTime.UtcNow.Second > LastS)
                {
                    FPS = Ticken / (DateTime.UtcNow.Second - LastS);
                }
                LastS = DateTime.UtcNow.Second;
                Ticken = 0;
            }
            Ticken++;
        }

        protected override void Run()
        {
            if(KernelPanic)
            {
                //canvas.Clear(Color.DarkRed);
                string[] toprint = new string[] {
                    "Banana OS Basic V2 has Crashed",
                    "Reason: Kernel Panic",
                    $"{KPI.aName}",
                    $"{KPI.aDescription}",
                    $"{KPI.LastKnownAddressValue}",
                    "Press any key to restart Banana OS Basic V2..."
                };
                int biggestLength = 0;
                for (int i = 0; i < toprint.Length; i++)
                {
                    if(toprint[i].Length > biggestLength)
                    {
                        biggestLength = toprint[i].Length;
                    }
                }
                canvas.DrawFilledRectangle(new Pen(Color.Red), new Sys.Graphics.Point(0, 0), biggestLength * 8, 14 * toprint.Length);
                for (int i = 0; i < toprint.Length; i++)
                {
                    canvas.DrawString(toprint[i], PCScreenFont.Default, new Pen(Color.White), new Sys.Graphics.Point(0, 14 * i));
                }
                canvas.Display();

                System.Console.ReadKey();
                Sys.Power.Reboot();

                return;
            }

            Cosmos.Core.Memory.Heap.Collect();

            //WindowManager.CreateWindow(WindowType.User_Window, "Test", "Another test");
            tick++;

            /*if(tick > 50)
            {
                KernelPanic = true;
            }*/

            /*if(tick > 150)
            {
                isSetupMode = true;
            } else if(tick == 170)
            {
                UI.Topbar.hidden = true;
            }*/

            try
            {
                if(!renderNormal)
                {
                    if(IsShuttingDown)
                    {
                        Boot.ShowShuttingDownScreen(canvas, this);
                    } else if(IsRestarting)
                    {
                        Boot.ShowRestartingScreen(canvas, this);
                    } else
                    {
                        Boot.ShowBootScreen(canvas, this);
                    }
                    canvas.Display();
                    return;
                }

                if(IsResizing)
                {
                    if(tick % 10 == 0)
                    {
                        IsResizing = false;
                    }

                    canvas.Clear(Color.Black);
                    canvas.Display();

                    return;
                }
                Update();
                canvas.DrawString(tick.ToString(), PCScreenFont.Default, new Pen(Color.Black), 15, 15);

                UI.Topbar.setupMode = isSetupMode;
                UI.Taskbar.setupMode = isSetupMode;

                canvas.Clear(Color.SkyBlue);
                //canvas.DrawFilledRectangle(new Pen(Color.White), new Sys.Graphics.Point(10, 10), screenWidth - 20, screenHeight - 20);
                UI.Topbar.RenderTopbar(canvas, screenWidth);
                UI.Taskbar.RenderTaskBar(canvas, this);

                canvas.DrawString($"FPS: {FPS}", PCScreenFont.Default, new Pen(Color.White), new Cosmos.System.Graphics.Point(0, 0));

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
                canvas.Clear();
                canvas.DrawString(e.Message, PCScreenFont.Default, new Pen(Color.Black), new Cosmos.System.Graphics.Point((screenWidth / 2) - e.Message.Length * 4, 115));
                canvas.Display();

                //System.Console.Clear();
                //Stop();
            }
        }

        public static void DoKernelPanic(string aName, string aDescription, ref IRQContext ctx, string LastKnownAddressValue = "0")
        {
            KernelPanic = true;
            KPI = new KernelPanicInfo()
            {
                aName = aName,
                aDescription = aDescription,
                ctx = ctx,
                LastKnownAddressValue = LastKnownAddressValue
            };
        }

        public class KernelPanicInfo
        {
            public string aName = "";
            public string aDescription = "";
            public IRQContext ctx;
            public string LastKnownAddressValue = "0";
        }

        public void ResizeScreen(int width, int height)
        {
            canvas.Clear(Color.Black);
            IsResizing = true;

            screenWidth = width;
            screenHeight = height;

            canvas = FullScreenCanvas.GetFullScreenCanvas(new Mode(screenWidth, screenHeight, ColorDepth.ColorDepth32));
            canvas.Mode = new Mode(screenWidth, screenHeight, ColorDepth.ColorDepth32);
            MouseManager.ScreenWidth = (uint)screenWidth;
            MouseManager.ScreenHeight = (uint)screenHeight;
        }

        public void BeginShutdown()
        {
            renderNormal = false;
            IsShuttingDown = true;

            Sys.Power.Shutdown();
        }

        public void BeginRestart()
        {
            renderNormal = false;
            IsRestarting = true;

            Sys.Power.Reboot();
        }

        [Plug(Target = typeof(Cosmos.Core.INTs))]
        public class INTs
        {
            /// <summary>
            /// Handles kernel exceptions (DIVIDE BY ZERO etc.)
            /// </summary>
            /// <param name="eDescription">Exception description</param>
            /// <param name="eName">Name of the exception</param>
            /// <param name="context">Cause of the exception</param>
            /// <param name="LastKnownAddressValue">Last known address in memory (Where in RAM the exception occurred)</param>
            public static void HandleException(uint aEIP, string aDescription, string aName, ref IRQContext ctx, uint LastKnownAddressValue = 0)
            {
                string error = ctx.Interrupt.ToString();
                const string xHex = "0123456789ABCDEF";

                string ctxinterrupt = "";
                ctxinterrupt = ctxinterrupt + xHex[(int)((ctx.Interrupt >> 4) & 0xF)];
                ctxinterrupt = ctxinterrupt + xHex[(int)(ctx.Interrupt & 0xF)];

                string LastKnownAddress = "";

                if (LastKnownAddressValue != 0)
                {
                    LastKnownAddress = LastKnownAddress + xHex[(int)((LastKnownAddressValue >> 28) & 0xF)];
                    LastKnownAddress = LastKnownAddress + xHex[(int)((LastKnownAddressValue >> 24) & 0xF)];
                    LastKnownAddress = LastKnownAddress + xHex[(int)((LastKnownAddressValue >> 20) & 0xF)];
                    LastKnownAddress = LastKnownAddress + xHex[(int)((LastKnownAddressValue >> 16) & 0xF)];
                    LastKnownAddress = LastKnownAddress + xHex[(int)((LastKnownAddressValue >> 12) & 0xF)];
                    LastKnownAddress = LastKnownAddress + xHex[(int)((LastKnownAddressValue >> 8) & 0xF)];
                    LastKnownAddress = LastKnownAddress + xHex[(int)((LastKnownAddressValue >> 4) & 0xF)];
                    LastKnownAddress = LastKnownAddress + xHex[(int)(LastKnownAddressValue & 0xF)];
                }
                //AIC.Main.Bluescreen.Panic(aName, aDescription, LastKnownAddress, ref ctx);
                DoKernelPanic(aName, aDescription, ref ctx, LastKnownAddress);
            }
        }
    }
}