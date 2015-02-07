using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jarvis.Wit
{
    public class Interval
    {
        private readonly DateTime _to;
        private readonly DateTime _from;

        public Interval(DateTime to, DateTime from)
        {
            _to = to;
            _from = from;
        }

        public DateTime To
        {
            get { return _to; }
        }

        public DateTime From
        {
            get { return _from; }
        }
    }
}
