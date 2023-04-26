using System.Speech.Recognition;
using System;

namespace Kotono.Audio
{
    public sealed class SpeechRecognizer
    {
        private SpeechRecognizer() { }

        private static readonly SpeechRecognitionEngine _recognizer = new(new System.Globalization.CultureInfo("en-US"));

        public static void Init()
        {
            var services = new Choices(new string[] { "restaurant", "hotel", "gas station", "restroom" });
            var cities = new Choices(new string[] { "Seattle", "Paris", "New York" });

            var findServices = new GrammarBuilder("Find");
            findServices.Append(services);
            findServices.Append("near");
            findServices.Append(cities);

            var servicesGrammar = new Grammar(findServices);

            // Create and load a dictation grammar.  
            _recognizer.LoadGrammarAsync(servicesGrammar);

            // Add a handler for the speech recognized event.  
            _recognizer.SpeechRecognized +=
              new EventHandler<SpeechRecognizedEventArgs>(OnSpeechRecognized);

            // Configure input to the speech _recognizer.  
            _recognizer.SetInputToDefaultAudioDevice();

            // Start asynchronous, continuous speech recognition.  
            _recognizer.RecognizeAsync(RecognizeMode.Multiple);

        }

        private static void OnSpeechRecognized(object sender, SpeechRecognizedEventArgs e)
        {
            KT.Print(e.Result.Text);
        }
    }
}
