using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;

namespace Jarvis.Wit
{
    public interface IParser<out T, TP> where T : IWitEntity<TP>
    {
        T Parse(string entity, JArray array);
        T Parse(string entity, JArray array, int index);
    }
}
