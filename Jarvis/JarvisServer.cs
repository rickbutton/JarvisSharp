using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Jarvis.Wit;

namespace Jarvis
{
    public class JarvisServer
    {
        private readonly IDictionary<string, Func<JarvisIntent, JarvisResponse>> _handlers;
        private readonly IJarvisInput _input;
        private readonly IJarvisOutput _output;
        private WitClient _wit;

        public JarvisServer(IJarvisInput input, IJarvisOutput output)
        {
            _handlers = new Dictionary<string, Func<JarvisIntent, JarvisResponse>>();
            _input = input;
            _output = output;
            _wit = new WitClient("ONEX5ZGJ7MST6WCRTDXTE5AO6K3RLZVA");
        }

        public void Start()
        {
            while (true)
            {
                var input = _input.GetInput();
                var intent = _wit.GetIntent(input);
                HandleIntent(intent);
            }     
        }

        public void RegisterHandler(IIntentHandler handler)
        {
            foreach (var intent in handler.IntentsToListen)
            {
                _handlers[intent] = handler.Handle;
            } 
        }

        private void HandleIntent(WitResponse witResponse)
        {
            var intent = witResponse.BestOutcome.Intent;
            if (_handlers.ContainsKey(intent))
            {
                var h = _handlers[intent];
                var response = h(new JarvisIntent(witResponse));
                _output.SendOutput(response.Response);
            }
            else
            {
                throw new InvalidOperationException("Invalid intent: " + intent);
            }
        }
    }
}
