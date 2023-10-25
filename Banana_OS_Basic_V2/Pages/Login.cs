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

            Input usernameInput = new Input();
            usernameInput.colors = new Input.ColorScheme()
            {
                Normal = Color.LightGray,
                Hovered = Color.Gray,
                Clicked = Color.DarkGray,
                Disabled = Color.DarkSlateGray,

                Text = Color.White
            };
            usernameInput.x = (kernel.screenWidth / 2) - (150 / 2);
            usernameInput.y = (kernel.screenHeight / 2) - (15 * 6);
            usernameInput.width = 150;
            usernameInput.height = 15;
            usernameInput.placeholder = "Username";
            usernameInput.Render(canvas, kernel);

            Button LoginButton = new Button();
            LoginButton.colors = new Button.ColorScheme()
            {
                Normal = Color.LightGray,
                Hovered = Color.Gray,
                Clicked = Color.DarkGray,
                Disabled = Color.DarkSlateGray,

                Text = Color.White
            };
            LoginButton.x = (kernel.screenWidth / 2) - (45 / 2);
            LoginButton.y = (kernel.screenHeight / 2) - (15 * 3);
            LoginButton.width = 45;
            LoginButton.height = 15;
            LoginButton.content = "Login";
            LoginButton.Render(canvas, kernel);
        }
    }
}
