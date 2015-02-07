using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jarvis
{
    public class JarvisResponse
    {
        public static JarvisResponse Unknown = new JarvisResponse("I don't know what you mean.");
        public static JarvisResponse None = new JarvisResponse("");
        public JarvisResponse(string response)
        {
            Response = response;
        }

        public string Response { get; private set; }
    }
}
