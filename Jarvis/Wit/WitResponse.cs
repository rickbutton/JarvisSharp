using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using Jarvis.Wit.Entities;
using Jarvis.Wit.Parsers;
using Newtonsoft.Json.Linq;

namespace Jarvis.Wit
{
    public class WitResponse
    {
        public string MsgId;
        public string Text;
        public ICollection<WitOutcome> Outcomes;

        public WitOutcome BestOutcome { get { return Outcomes.First(); } }
    }

    public enum WitEntityType
    {
       Simple, DateTime, Interval, Temperature, Distance
    }

    public class WitOutcome
    {
        public string Text;
        public string Intent;
        public IDictionary<string, JArray> Entities;

        public bool HasEntity(string entity)
        {
            return Entities.ContainsKey(entity); 
        }

        public DateTimeEntity      GetDateTime(string entity, int index) { return new DateTimeParser().Parse(entity, Entities[entity], index); }
        public IntervalEntity      GetInterval(string entity, int index) { return new IntervalParser().Parse(entity, Entities[entity], index); }
        public ProductEntity       GetProduct(string entity, int index)  { return new ProductParser().Parse(entity, Entities[entity], index); }
        public StringEntity        GetString(string entity, int index)   { return new StringParser().Parse(entity, Entities[entity], index); }
        public TemperatureEntity   GetTemperature(string entity, int index)   { return new TemperatureParser().Parse(entity, Entities[entity], index); }

        public DateTimeEntity        GetFirstDateTime(string entity) { return GetDateTime(entity, 0); }
        public IntervalEntity        GetFirstInterval(string entity) { return GetInterval(entity, 0); }
        public ProductEntity         GetFirstProduct(string entity) { return GetProduct(entity, 0); }
        public StringEntity         GetFirstString(string entity) { return GetString(entity, 0); }
        public TemperatureEntity     GetFirstTemperature(string entity) { return GetTemperature(entity, 0); }
    }

    public class WitNumberWithUnit
        {
            public double Value;
            public string Unit;
        }

    public class WitDateTimeInterval
    {
        public DateTime From;
        public DateTime To;
    }

    public enum WitTemperatureUnit
    {
        C, F, None
    }

    public class WitTemperature
    {
        public double Temperature;
        public WitTemperatureUnit Unit;
    }

    public class WitProduct
    {
        public string Unit;
        public double Value;
        public string Product;
    }
}
