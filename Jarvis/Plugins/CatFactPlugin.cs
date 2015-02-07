using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Jarvis.Plugins
{
    public class CatFactPlugin : IIntentHandler
    {
        public class CatFactResponse
        {
            public ICollection<string> Facts;
            public bool Success;
        }
        public JarvisResponse Handle(JarvisIntent intent)
        {
            var request = WebRequest.Create("http://catfacts-api.appspot.com/api/facts");
            var response = request.GetResponse();
            var stream = response.GetResponseStream();
            var bytes = new StreamReader(stream).ReadToEnd();
            var cf = JsonConvert.DeserializeObject<CatFactResponse>(bytes);

            var fact = cf.Facts.First();
            return new JarvisResponse(fact);
        }

        public ICollection<string> IntentsToListen { get { return new List<string>() { "catfact" };} }
    }
}
