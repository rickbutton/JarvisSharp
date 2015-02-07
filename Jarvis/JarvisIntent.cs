using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Jarvis.Wit;

namespace Jarvis
{
    public class JarvisIntent
    {
        public JarvisIntent(WitResponse intent)
        {
            Intent = intent;
        }

        public WitResponse Intent { get; private set; }
    }
}
