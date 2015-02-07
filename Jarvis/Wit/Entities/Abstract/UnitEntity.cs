using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jarvis.Wit.Entities.Abstract
{
    public abstract class UnitEntity<T> : BasicEntity<T>
    {
        private readonly string _unit;

        protected UnitEntity(string name, string unit, T value) : base(name, value)
        {
            _unit = unit;
        }

        public string Unit
        {
            get { return _unit; }
        }
    }
}
