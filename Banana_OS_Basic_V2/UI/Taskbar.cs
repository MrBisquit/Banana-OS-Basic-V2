using Cosmos.System.Graphics;
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
        public static void RenderTaskBar(Canvas canvas, int screenWidth, int screenHeight)
        {
            if (setupMode) return;
            canvas.DrawFilledRectangle(new Pen(Color.Black), new Cosmos.System.Graphics.Point(0, screenHeight - 40), screenWidth, 40);
        }
    }
}
