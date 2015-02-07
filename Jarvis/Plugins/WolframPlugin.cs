using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using WolframAlphaNET;
using WolframAlphaNET.Enums;
using WolframAlphaNET.Misc;
using WolframAlphaNET.Objects;

namespace Jarvis.Plugins
{
    class WolframPlugin : IIntentHandler
    {
        public JarvisResponse Handle(JarvisIntent intent)
        {
            var o = intent.Intent.BestOutcome;
            var query = o.GetFirstString("wolfram_search_query").Value;
            var w = new WolframAlpha("8GXWYP-Q8TLWGUH4H");
            w.Formats = new List<Format>()
            {
                Format.Plaintext
            };
            w.ScanTimeout = 0.1f;

            var results = w.Query(query);

            if (results != null)
            {
                var result = results.Pods.Find((p) => p.Title == "Result");
                if (result != null)
                {
                    return new JarvisResponse(ProcessOutput(result.SubPods.First().Plaintext));
                }
                else
                {
                    var pods = results.RecalculateResults();
                    var newResult = pods.Find((p) => p.Title == "Result");
                    if (newResult != null)
                        return new JarvisResponse(ProcessOutput(newResult.SubPods.First().Plaintext));
                }
            }
            return JarvisResponse.Unknown;
        }

        private string ProcessOutput(string output)
        {
            return Regex.Replace(output, "\\([^\\(]*\\)", "");
        }

        public ICollection<string> IntentsToListen { get {return new List<string>() { "wolfram" };}}
    }
}
