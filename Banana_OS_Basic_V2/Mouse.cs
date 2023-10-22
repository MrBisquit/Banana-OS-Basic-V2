﻿using Cosmos.System;
using Cosmos.System.Graphics;
using IL2CPU.API.Attribs;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Banana_OS_Basic_V2
{
    public static class Mouse
    {
        [ManifestResourceStream(ResourceName = "Banana_OS_Basic_V2.Assets.Cursors.Normal.bmp")]
        static byte[] NormalRaw;
        public static void DisplayMouse(Canvas canvas)
        {
            /*Pen pen = new Pen(Color.Red);

            canvas.DrawLine(pen, (int)MouseManager.X, (int)MouseManager.Y, (int)MouseManager.X + 5, (int)MouseManager.Y);
            canvas.DrawLine(pen, (int)MouseManager.X, (int)MouseManager.Y, (int)MouseManager.X, (int)MouseManager.Y - 5);
            canvas.DrawLine(pen, (int)MouseManager.X, (int)MouseManager.Y, (int)MouseManager.X + 5, (int)MouseManager.Y - 5);*/

            canvas.DrawImageAlpha(new Bitmap(NormalRaw), (int)MouseManager.X, (int)MouseManager.Y);
        }
    }
}
