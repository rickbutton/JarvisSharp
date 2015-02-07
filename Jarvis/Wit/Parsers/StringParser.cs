using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Jarvis.Wit.Entities;
using Newtonsoft.Json.Linq;

namespace Jarvis.Wit.Parsers
{
    public class StringParser : AbstractParser<StringEntity, string>
    {
        protected override StringEntity ParseEntity(string entity, JToken o)
        {
            var str = o["value"].ToString();
            return new StringEntity(entity, str);
        }
    }
}
