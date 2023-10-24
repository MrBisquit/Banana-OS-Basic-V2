using Cosmos.System.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Banana_OS_Basic_V2.Window
{
    public static class WindowManager
    {
        static List<Window> windows = new List<Window>();
        public static void RenderWindows(Canvas canvas, Kernel kernel)
        {
            foreach (Window window in windows)
            {
                window.RenderWindow(canvas, kernel);
            }
        }
        public static Window CreateWindow(WindowType WindowType, string name, string title)
        {
            Window window = new Window(WindowType, name, title);

            windows.Add(window);

            return window;
        }

        public static bool CloseAllWindows()
        {
            bool success = false;

            foreach (Window window in windows)
            {
                if(window.CanClose) window.Close();
            }

            return success;
        }

        public static bool MinimiseAllWindows()
        {
            bool success = false;

            foreach (Window window in windows)
            {
                if(!window.AlwaysOnTop && window.CanMinimize) window.Hide();
            }

            return success;
        }

        public static List<Window> GetWindows()
        {
            return windows;
        }

        public static void CloseWindow(Window window)
        {
            windows.Remove(window);
        }

        public static void ShowWindow(int id)
        {
            windows[id].Show();
        }

        public static void HideWindow(int id)
        {
            windows[id].Hide();
        }

        public static Window GetWindow(int id)
        {
            return windows[id];
        }
    }
}
