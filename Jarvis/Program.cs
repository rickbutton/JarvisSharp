using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Jarvis.Plugins;
using Jarvis.Wit;

namespace Jarvis
{
    class Program
    {
        static void Main(string[] args)
        {
            var input = new ConsoleJarvisInput();
            var output = new ConsoleJarvisOutput();

            var server = new JarvisServer(input, output);

            server.RegisterHandler(new CatFactPlugin());
            server.RegisterHandler(new MusicPlugin());
            server.RegisterHandler(new WolframPlugin());

            server.Start();
        }
    }
}
