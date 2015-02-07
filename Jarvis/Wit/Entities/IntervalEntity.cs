using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Jarvis.Wit.Entities.Abstract;
using Jarvis.Wit.Parsers;

namespace Jarvis.Wit.Entities
{
    public class IntervalEntity : BasicEntity<Interval>
    {
        public IntervalEntity(string name, Interval value) : base(name, value) { }
    }
}
