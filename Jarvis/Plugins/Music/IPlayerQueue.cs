using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Torshify;

namespace Jarvis.Plugins.Music
{
    public interface IPlayerQueue
    {
        ITrack this[int i] { get; }
        int Count { get; }
    }
}
