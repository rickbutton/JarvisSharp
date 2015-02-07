using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Jarvis.Wit.Entities;
using Newtonsoft.Json.Linq;

namespace Jarvis.Wit.Parsers
{
    public class TemperatureParser : AbstractParser<TemperatureEntity, double>
    {
        protected override TemperatureEntity ParseEntity(string entity, JToken o)
        {
            var unitStr = o["unit"].ToString();
            var valueStr = o["value"].ToString();
            double value;
            double.TryParse(valueStr, out value);

            TemperatureEntity.TemperatureUnit unit;
            switch (unitStr)
            {
                case "celsius":
                    unit = TemperatureEntity.TemperatureUnit.C;
                    break;
                case "fahrenheit":
                    unit = TemperatureEntity.TemperatureUnit.F;
                    break;
                default:
                    unit = TemperatureEntity.TemperatureUnit.None;
                    break;
            }
            return new TemperatureEntity(entity, unit, value);
        }
    }
}
