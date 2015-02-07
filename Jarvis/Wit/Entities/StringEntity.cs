using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Jarvis.Wit.Entities.Abstract;

namespace Jarvis.Wit.Entities
{
    public class StringEntity : BasicEntity<string>
    {
        public StringEntity(string name, string value) : base(name, value) { }
    }
}
