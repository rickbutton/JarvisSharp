using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Jarvis.Wit.Entities.Abstract;

namespace Jarvis.Wit.Entities
{
    public class ProductEntity : UnitEntity<double>
    {
        private readonly string _product;

        public ProductEntity(string name, string unit, string product, double value) : base(name, unit, value)
        {
            _product = product;
        }

        public string Product { get { return _product; } }
    }
}
