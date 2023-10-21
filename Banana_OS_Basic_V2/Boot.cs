using Cosmos.System.Graphics;
using Cosmos.System.Graphics.Fonts;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Banana_OS_Basic_V2
{
    public static class Boot
    {
        public static void ShowBootScreen(Canvas canvas, Kernel kernel)
        {
            canvas.Clear();
            canvas.DrawString($"Banana OS Basic V2", PCScreenFont.Default, new Pen(Color.White), new Cosmos.System.Graphics.Point((kernel.screenWidth / 2) - "Banana OS Basic V2".Length, kernel.screenHeight / 2));
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
