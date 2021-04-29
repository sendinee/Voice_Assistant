namespace voice_assistant {
    class PluginFactory {
        public static IPlugin GetPlugin(Agent agent, Sentence sentence) {
            if (sentence.Plugin.Equals("time")) return new PluginTime(agent);
            else if (sentence.Plugin.Equals("searchweb")) return new PluginSearchWeb(agent);
            else return null;
        }
    }
}