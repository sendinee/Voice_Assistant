using System.Globalization;
using System;
using System.Threading.Tasks;
using Microsoft.CognitiveServices.Speech;

namespace voice_assistant {
    class Agent {
        private static Agent instance = null;
        private SpeechRecognizer recognizer;
        private SpeechSynthesizer synthesizer;
        public delegate void onSpeechHandler(string text);
        public onSpeechHandler onSpeech;

        private Agent() {}

        private Agent(String apiKey, String apiLocation) {
            SpeechConfig config = SpeechConfig.FromSubscription(apiKey, apiLocation);
            config.SpeechSynthesisLanguage = "fr-FR";
            config.SpeechRecognitionLanguage = "fr-FR";
            config.SetProfanity(ProfanityOption.Raw);
            recognizer = new SpeechRecognizer(config);
            recognizer.Recognized += RecognizedEventHandler;
            synthesizer = new SpeechSynthesizer(config);
        }

        public static Agent getInstance(String apiKey, String apiLocation) {
            if (instance == null) {
                instance = new Agent(apiKey, apiLocation);
            }
            return instance;
        }

        public void startListening() {
            Console.WriteLine("Listening ...");
            recognizer.StartContinuousRecognitionAsync();
        }

        public async Task stopListening() {
            await recognizer.StopContinuousRecognitionAsync();
        }

        private void RecognizedEventHandler(object sender, SpeechRecognitionEventArgs e) {
            if (!string.IsNullOrEmpty(e.Result.Text)) {
                Console.WriteLine($"recognised : {e.Result.Text}");
                onSpeech(e.Result.Text);
            }
        }

        public async Task SynthesisToSpeakerAsync(String text) {

            using (var result = await synthesizer.SpeakTextAsync(text)) {
                if (result.Reason == ResultReason.SynthesizingAudioCompleted) {
                    Console.WriteLine($"Speech synthesized to speaker for text [{text}]");
                }
                else if (result.Reason == ResultReason.Canceled) {
                    var cancellation = SpeechSynthesisCancellationDetails.FromResult(result);
                    Console.WriteLine($"CANCELED: Reason={cancellation.Reason}");

                    if (cancellation.Reason == CancellationReason.Error) {
                        Console.WriteLine($"CANCELED: ErrorCode={cancellation.ErrorCode}");
                        Console.WriteLine($"CANCELED: ErrorDetails=[{cancellation.ErrorDetails}]");
                        Console.WriteLine($"CANCELED: Did you update the subscription info?");
                    }
                }
            }
        }
    }
}
