using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Jarvis.Wit.Entities.Abstract;

namespace Jarvis.Wit.Entities
{
    public class TemperatureEntity : BasicEntity<double>
    {
        public enum TemperatureUnit
        {
            C, F, None
        }

        private readonly TemperatureUnit _unit;
        public TemperatureEntity(string name, TemperatureUnit unit, double value) : base(name,  value)
        {
            _unit = unit;
        }

        public TemperatureUnit Unit { get { return _unit;  } }
    }
}
