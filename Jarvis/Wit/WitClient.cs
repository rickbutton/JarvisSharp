using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Jarvis.Wit
{
    class WitClient
    {
        private string _token;
        private const string UrlBase = "https://api.wit.ai/message?v=20141022&q=";

        public WitClient(string token)
        {
            _token = token;
        }

        public WitResponse GetIntent(string query)
        {
            var url = UrlBase + Uri.EscapeDataString(query);

            var request = (HttpWebRequest)WebRequest.Create(url);
            request.Method = "GET";
            request.Headers.Add("Authorization", "Bearer " + _token);

            using (var response = (HttpWebResponse) request.GetResponse())
            {
                var data = response.GetResponseStream();
                var str = new StreamReader(data, Encoding.UTF8).ReadToEnd();
                return JsonConvert.DeserializeObject<WitResponse>(str);
            }
        }
    }
}
