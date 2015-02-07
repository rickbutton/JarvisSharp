using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jarvis
{
    class ConsoleJarvisInput : IJarvisInput
    {
        public string GetInput()
        {
            Console.Write("jasper>");
            return Console.ReadLine();
        }
    }
}
