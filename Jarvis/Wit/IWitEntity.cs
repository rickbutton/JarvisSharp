using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jarvis.Wit
{
    public interface IWitEntity<out T>
    {
        string Name { get; }
        T Value { get; }
    }
}
