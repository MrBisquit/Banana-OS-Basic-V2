using Cosmos.System.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Banana_OS_Basic_V2.UI
{
    public abstract class UIElement
    {
        public Action RenderElement;

        public int screenWidth = 600;
        public int screenHeight = 400;

        public int x = 0;
        public int y = 0;

        public int width = 0;
        public int height = 0;

        public abstract void Render(Canvas canvas, Kernel kernel);
    }
}
