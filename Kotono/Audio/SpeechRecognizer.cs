using System.Speech.Recognition;
using System.Threading;

namespace Kotono.Audio
{
    public sealed class SpeechRecognizer
    {
        private SpeechRecognizer() { }

        private static readonly SpeechRecognitionEngine _recognizer = new(new System.Globalization.CultureInfo("en-US"));

        public static string Text { get; private set; } = string.Empty;

        // int instead of bool for IsSpeechRecognized's Interlocked.Exchange() that doesn't support bool
        private static int _isSpeechRecognized = 0;

        // Sets _isSpeechRecognized to 0 and returns its old value, if it was 1 then IsSpeechRecognized returns true
        public static bool IsSpeechRecognized => Interlocked.Exchange(ref _isSpeechRecognized, 0) == 1;

        public static void Init(GrammarBuilder grammarBuilder)
        {
            var servicesGrammar = new Grammar(grammarBuilder);

            // Create and load a dictation grammar.  
            _recognizer.LoadGrammarAsync(servicesGrammar);

            // Add a handler for the speech recognized event.  
            _recognizer.SpeechRecognized += OnSpeechRecognized;

            // Configure input to the speech _recognizer.  
            _recognizer.SetInputToDefaultAudioDevice();

            // Start asynchronous, continuous speech recognition.  
            _recognizer.RecognizeAsync(RecognizeMode.Multiple);

        }

        private static void OnSpeechRecognized(object? sender, SpeechRecognizedEventArgs e)
        {
            Text = e.Result.Text;
            _isSpeechRecognized = 1;
        }
    }
}
