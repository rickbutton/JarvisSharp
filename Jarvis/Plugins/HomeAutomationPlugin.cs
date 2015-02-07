using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Jarvis.Wit;

namespace Jarvis.Plugins
{
    public class HomeAutomationPlugin : IIntentHandler
    {
        public JarvisResponse Handle(JarvisIntent intent)
        {
            var i = intent.Intent;

            var thing = i.BestOutcome.GetFirstString("ha_thing").Value;

            switch (thing)
            {
                case "lights":
                case "light":
                    return HandleLights(i);
            }
            return JarvisResponse.Unknown;
        }

        public ICollection<string> IntentsToListen { get { return new List<string>() { "ha" }; } }

        private JarvisResponse HandleLights(WitResponse i)
        {
            var place = i.BestOutcome.GetFirstString("ha_place").ToString();
            var action = i.BestOutcome.GetFirstString("on_off").ToString();
            return JarvisResponse.Unknown;
        }
    }
}
