using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Jarvis.Wit.Entities.Abstract;
using Jarvis.Wit.Parsers;

namespace Jarvis.Wit.Entities
{
    public class DateTimeEntity : BasicEntity<DateTime>
    {
        public DateTimeEntity(string name, DateTime value) : base(name, value) { }
    }
}
