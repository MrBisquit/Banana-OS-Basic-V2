using Banana_OS_Basic_V2.UI;
using Cosmos.System.Graphics;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Banana_OS_Basic_V2.Pages
{
    public static class Login
    {
        public static void Render(Canvas canvas, Kernel kernel)
        {
            canvas.DrawFilledRectangle(new Pen(Color.White), new Cosmos.System.Graphics.Point(15 * 2, (int)(15 * 2.5)), kernel.screenWidth - (15 * 4), kernel.screenHeight - (int)(15 * 4.5));
            Button LoginButton = new Button();
            LoginButton.x = 0;
            LoginButton.y = 0;
            //LoginButton.Render();
        }
    }
}
