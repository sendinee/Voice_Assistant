using System;

namespace voice_assistant {
    class Program {
        static Agent myAgent = Agent.getInstance("", "francecentral");
        static void Main(string[] args) {
            myAgent.onSpeech += CallbackVoice;
            myAgent.startListening();
            Console.WriteLine("Please Press <Return> to continue");
            Console.ReadLine();
        }

        static async void CallbackVoice(string text) {
            await myAgent.stopListening();
            Sentence sentence = Sentence.searchSentence(text);
            if (sentence != null) {
                IPlugin plugin = PluginFactory.GetPlugin(myAgent, sentence);
                if (plugin != null) await plugin.doSomething(sentence.Args);
            }
            myAgent.startListening();
        }
    }
}
