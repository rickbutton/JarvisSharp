using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Jarvis.Wit.Entities;
using Newtonsoft.Json.Linq;

namespace Jarvis.Wit.Parsers
{
    class ProductParser : AbstractParser<ProductEntity, double>
    {
        protected override ProductEntity ParseEntity(string entity, JToken o)
        {
            var unit = o["unit"].ToString();
            var product = o["product"].ToString();
            var valueStr = o["value"].ToString();
            double value;
            double.TryParse(valueStr, out value);
            return new ProductEntity(entity, unit, product, value);
        }
    }
}
