using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Jarvis.Wit.Entities;
using Newtonsoft.Json.Linq;

namespace Jarvis.Wit
{
    public abstract class AbstractParser<T, TP> : IParser<T, TP> where T : IWitEntity<TP>
    {
        public T Parse(string entity, JArray array)
        {
            return Parse(entity, array, 0);
        }

        public T Parse(string entity, JArray array, int index)
        {
            return ParseEntity(entity, array[index]);
        }

        protected abstract T ParseEntity(string entity, JToken o);
    }
}
