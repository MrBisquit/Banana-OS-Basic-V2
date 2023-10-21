using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Banana_OS_Basic_V2
{
    public class Session
    {
        public UserLevel UserLevel;
        public string Username;
    }

    public enum UserLevel
    {
        SYSTEM = 0,
        User = 1,
        Administrator = 2
    }
}
