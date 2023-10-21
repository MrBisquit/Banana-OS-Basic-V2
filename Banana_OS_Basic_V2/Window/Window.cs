using Banana_OS_Basic_V2.Process;
using Banana_OS_Basic_V2.UI;
using Cosmos.System.Graphics;
using System;
using System.Collections.Generic;
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
