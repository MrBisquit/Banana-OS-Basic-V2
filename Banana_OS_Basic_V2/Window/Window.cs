using Banana_OS_Basic_V2.Process;
using Banana_OS_Basic_V2.UI;
using Cosmos.System;
using Cosmos.System.Graphics;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Banana_OS_Basic_V2.Window
{
    public class Window
    {
        // Private variables that only functions in this class can edit
        static bool _IsVisible { get; set; } = true;
        static string _WindowName { get; set; } = "";
        static Process.Process _Process { get; set; }
        static bool _CanClose { get; set; } = true;
        static WindowState _WindowState { get; set; } = WindowState.Normal;

        public bool CanMinimize = true;
        public bool CanMaximize = true;
        // System_Window only property
        public bool CanClose { get; } = _CanClose;

        public WindowType WindowType;
        public ResizeMode ResizeMode { get; set; } = ResizeMode.CanResize;
        public WindowState WindowState { get; } = _WindowState;
        public bool IsVisible { get; } = _IsVisible;

        // For task managing
        public string WindowName { get; } = _WindowName;
        // To be displayed to the user
        public string WindowTitle { get; set; }
        public bool AlwaysOnTop { get; set; }
        public Process.Process Process { get; } = _Process;
        public Process.Process[] SubProcesses { get; set; }
        public TitleBarProperties TitleBarProperties { get; set; } = new TitleBarProperties();
        public List<UIElement> Elements { get; set; }
        public Bitmap Icon { get; set; } = new Bitmap(14, 14, ColorDepth.ColorDepth32);

        public int x = 0;
        public int y = 0;
        public int width = 500;
        public int height = 475;

        public bool IsMoving = false;
        public int movedX = 0;
        public int movedY = 0;

        public Window(WindowType type, string name, string title)
        {
            WindowType = type;
            WindowName = name;
            WindowTitle = title;

            Process = ProcessManager.CreateProcess();

            Show();
        }

        public void Show()
        {
            _IsVisible = true;
        }

        public void Hide()
        {
            _IsVisible = false;
        }

        public void Close()
        {

        }

        public void SetWindowState(WindowState state)
        {
            _WindowState = state;
        }

        public void RenderWindow(Canvas canvas, Kernel kernel)
        {
            if(y <= 14)
            {
                y = 15;
            }

            if(y + height >= kernel.screenHeight - 40)
            {
                y = y + height - 40;
            }

            if(x + width >= kernel.screenWidth)
            {
                x = x - width;
            }

            byte[] IconRawData = new byte[Icon.rawData.Length];
            for (int i = 0; i < IconRawData.Length; i++)
            {
                IconRawData[i] = (byte)Icon.rawData[i];
            }

            canvas.DrawFilledRectangle(new Pen(Color.White), new Cosmos.System.Graphics.Point(x, y), width, height);
            canvas.DrawFilledRectangle(new Pen(Color.LightGray), new Cosmos.System.Graphics.Point(x, y), width, 14);
            canvas.DrawImageAlpha(new Bitmap(14, 14, IconRawData, Icon.Depth), x, y);
            canvas.DrawString(WindowTitle, Cosmos.System.Graphics.Fonts.PCScreenFont.Default, new Pen(Color.White), new Cosmos.System.Graphics.Point(x + 14, y));
            Button button = new Button();

            Button.ColorScheme colorScheme = new Button.ColorScheme()
            {
                Normal = Color.LightGray,
                Hovered = Color.Gray,
                Clicked = Color.DarkGray,
                Text = Color.White
            };

            button.RenderButton(canvas, (x + width) - 14, y, 14, 14, "X", () =>
            {
                WindowManager.CloseWindow(this);
            }, () => { }, colorScheme);

            // TODO: Fix not having the ability to move the window twice in a row.

            bool hovered = Mouse.IsPointInsideRectangle((int)MouseManager.X, (int)MouseManager.Y, x, y, x + width, y + height);
            bool hoverednc = Mouse.IsPointInsideRectangle((int)MouseManager.X, (int)MouseManager.Y, movedX, movedY, movedX + width, movedY + height);

            if (hovered && MouseManager.MouseState == MouseState.Left)
            {
                IsMoving = true;
            } else if(MouseManager.MouseState != MouseState.Left)
            {
                IsMoving = false;
            }

            if(hoverednc && Mouse.LastStateLC && !Mouse.StateLC)
            {
                x = movedX - x;
                y = movedY - y;

                movedX = x;
                movedY = y;
            } else if(hoverednc && Mouse.LastStateLC && Mouse.StateLC)
            {
                movedX = (int)MouseManager.X - x;
                movedY = (int)MouseManager.Y - y;
                canvas.DrawRectangle(new Pen(Color.White), new Cosmos.System.Graphics.Point((int)MouseManager.X - x, (int)MouseManager.Y - y), width, height);
            }

            /*if(IsMoving && MouseManager.MouseState == MouseState.Left)
            {
                canvas.DrawRectangle(new Pen(Color.White), new Cosmos.System.Graphics.Point((int)MouseManager.X - x, (int)MouseManager.Y - y), width, height);
            } else if(!IsMoving && !hovered && MouseManager.MouseState != MouseState.Left)
            {
                x = (int)MouseManager.X - x;
                y = (int)MouseManager.Y - y;
            }*/
        }

        public void SetCanClose(bool CanClose)
        {
            _CanClose = CanClose;
        }
    }

    public class TitleBarProperties
    {
        public static string Title { get; set; } = "";
        public static bool ShowTitleBar { get; set; } = true;
        public static bool Movable { get; set; } = true;
    }

    public enum WindowType
    {
        System_Window = 0,
        User_Window = 1
    }

    public enum ResizeMode
    {
        CanResize = 0,
        NoResize = 1,
        NoFullscreen = 2
    }

    public enum WindowState
    {
        Normal = 0,
        Minimised = 1,
        Maximized = 2,
        Fullscreen = 3
    }
}
