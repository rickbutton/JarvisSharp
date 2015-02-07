using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Jarvis.Wit.Entities;
using Newtonsoft.Json.Linq;

namespace Jarvis.Wit.Parsers
{
    public class IntervalParser : AbstractParser<IntervalEntity, Interval>
    {
        protected override IntervalEntity ParseEntity(string entity, JToken o)
        {
            var fromStr = o["from"]["value"].ToString();
            var toStr = o["to"]["value"].ToString();
            var from = DateTime.Parse(fromStr);
            var to = DateTime.Parse(toStr);
            return new IntervalEntity(entity, new Interval(to, from));
        }
    }
}
