using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Jarvis.Wit.Entities;
using Newtonsoft.Json.Linq;

namespace Jarvis.Wit.Parsers
{
    public class DateTimeParser : AbstractParser<DateTimeEntity, DateTime>
    {
        protected override DateTimeEntity ParseEntity(string entity, JToken o)
        {
            var str = o["value"].ToString();
            var date = DateTime.Parse(str);
            return new DateTimeEntity(entity, date);
        }
    }
}
