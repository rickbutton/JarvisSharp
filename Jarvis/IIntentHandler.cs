using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jarvis
{
    public interface IIntentHandler
    {
        JarvisResponse Handle(JarvisIntent intent);

        ICollection<string> IntentsToListen { get; }
    }
}
