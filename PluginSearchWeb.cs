using System;
using System.Text.RegularExpressions;
using System.Collections;
using System.Threading.Tasks;

namespace voice_assistant {
    class PluginSearchWeb: IPlugin {
        Agent agent;
        private string ChromeBrowserPath = "google-chrome";
        private string urlSearch = "https://fr.wikipedia.org/wiki/";
        public PluginSearchWeb(Agent agent) {
            this.agent = agent;
        }

        public async Task doSomething(ArrayList args) {
            try {
                string searchText = args[0].ToString();
                await agent.SynthesisToSpeakerAsync($"Voici la page wikipédia pour {searchText}");
                string SearchTextForUrl = Regex.Replace(searchText, @"\s", "_");
                System.Diagnostics.Process.Start(ChromeBrowserPath, urlSearch + SearchTextForUrl);
            } catch (System.ComponentModel.Win32Exception noBrowser) {
                Console.WriteLine(noBrowser.Message);
            } catch (System.Exception other) {
                Console.WriteLine(other.Message);
            }
        }
    }
}