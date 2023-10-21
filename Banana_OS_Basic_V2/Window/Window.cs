using Banana_OS_Basic_V2.Process;
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
        public bool CanMinimize = true;
        public bool CanMaximize = true;
        // System_Window only property
        public bool CanClose { get; } = true;

        public WindowType WindowType;
        public ResizeMode ResizeMode = ResizeMode.CanResize;
        public WindowState WindowState { get; } = WindowState.Normal;

        // For task managing
        public string WindowName { get; }
        // To be displayed to the user
        public string WindowTitle { get; set; }
        public bool AlwaysOnTop { get; set; }
        public Process.Process Process { get; }
        public Process.Process[] SubProcesses { get; set; }
        public TitleBarProperties TitleBarProperties { get; set; } = new TitleBarProperties();
        public UI.UIElement[] Elements { get; set; }

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

        }

        public void Hide()
        {

        }

        public void Close()
        {

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
