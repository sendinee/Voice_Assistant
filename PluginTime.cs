using System;
using System.Collections;
using System.Threading.Tasks;

namespace voice_assistant {
    class PluginTime: IPlugin {
        Agent agent;
        public PluginTime(Agent agent) {
            this.agent = agent;
        }

        public async Task doSomething(ArrayList args) {
            DateTime localDate = DateTime.Now;
            string text = $"Il est {localDate.Hour} heure et {localDate.Minute} minutes";
            await agent.SynthesisToSpeakerAsync(text);
        }
    }
}