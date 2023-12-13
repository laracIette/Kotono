using System;
using System.Speech.Recognition;

namespace Kotono.Audio
{
    public sealed class SpeechRecognizer
    {
        private SpeechRecognizer() { }

        private static readonly SpeechRecognitionEngine _recognizer = new(new System.Globalization.CultureInfo("en-US"));

        public static string Text { get; private set; } = "";

        private static bool _isSpeechRecognized = false;

        public static void Init(GrammarBuilder grammarBuilder)
        {
            var servicesGrammar = new Grammar(grammarBuilder);

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
            Text = e.Result.Text;
            _isSpeechRecognized = true;
        }

        public static bool GetSpeechRecognized()
        {
            if (_isSpeechRecognized)
            {
                _isSpeechRecognized = false;
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
