using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Banana_OS_Basic_V2
{
    public static class Keyboard
    {
        static bool keyPressed = false;
        static ConsoleKeyInfo key;

        public static void Tick()
        {
            if(Console.KeyAvailable)
            {
                keyPressed = true;
                key = Console.ReadKey();
            }
        }
    }
}
