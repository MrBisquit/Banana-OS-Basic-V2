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

        public int x = 0;
        public int y = 0;
        public int width = 500;
        public int height = 475;

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

            canvas.DrawFilledRectangle(new Pen(Color.White), new Cosmos.System.Graphics.Point(x, y), width, height);
            canvas.DrawFilledRectangle(new Pen(Color.LightGray), new Cosmos.System.Graphics.Point(x, y), width, 14);
            canvas.DrawString(WindowTitle, Cosmos.System.Graphics.Fonts.PCScreenFont.Default, new Pen(Color.White), new Cosmos.System.Graphics.Point(x, y));
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

            bool hovered = Mouse.IsPointInsideRectangle((int)MouseManager.X, (int)MouseManager.Y, x, y, x + width, y + height);

            if(hovered && MouseManager.LastMouseState == MouseState.Left)
            {
                x = (int)MouseManager.X;
                y = (int)MouseManager.Y;
            }
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
