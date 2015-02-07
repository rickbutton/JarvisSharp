using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jarvis
{
    class ConsoleJarvisOutput : IJarvisOutput
    {
        public void SendOutput(string output)
        {
            Console.WriteLine(output);
        }
    }
}
