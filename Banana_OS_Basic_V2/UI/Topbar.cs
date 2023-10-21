using Cosmos.System.Graphics;
using Cosmos.System.Graphics.Fonts;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace Banana_OS_Basic_V2.UI
{
    public static class Topbar
    {
        public static bool clockSeconds = false;
        public static bool clock24Hour = false;

        public static bool setupMode = false;
        public static bool hidden = false;
        public static void RenderTopbar(Canvas canvas, int screenWidth)
        {
            if(hidden) return;
            Color c = Color.FromArgb(64, 0, 0, 0);
            canvas.DrawFilledRectangle(new Pen(c), new Cosmos.System.Graphics.Point(0, 0), screenWidth, 15);
            canvas.DrawString(clock(), PCScreenFont.Default, new Pen(Color.White), new Cosmos.System.Graphics.Point((screenWidth / 2) - clock().Length * 4, 0));
        }
        public static string clock()
        {
            DateTime now = DateTime.Now;
            if (clock24Hour)
            {
                return $"{now.ToString("dd/MM/yyyy")} {(clockSeconds ? now.ToString("HH:mm:ss") : now.ToString("HH:mm"))}";
            }
            else
            {
                return $"{now.ToString("dd/MM/yyyy")} {(clockSeconds ? now.ToString("hh:mm:ss") : now.ToString("hh:mm"))} {(now.Hour / 12 == 0 ? "AM" : "PM")}";
            }
        }
    }
}
