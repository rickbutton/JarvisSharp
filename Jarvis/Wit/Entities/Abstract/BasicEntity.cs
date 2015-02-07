using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jarvis.Wit.Entities.Abstract
{
    public abstract class BasicEntity<T> : IWitEntity<T>
    {
        private readonly string _name;
        private readonly T _value;

        protected BasicEntity(string name, T value)
        {
            _name = name;
            _value = value;
        }

        public string Name
        {
            get { return _name; }
        }

        public T Value
        {
            get { return _value; }
        }
    }
}
