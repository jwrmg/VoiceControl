using System.Collections.ObjectModel;
using System.Speech.Recognition;

namespace VoiceControl.Speech
{
    public static class LanguageCodes
    {
        // English US.
        public const string EnUs = "en-US";
    }

    public class SpeechEngine
    {
        // members
        private SpeechRecognitionEngine internalEngine;

        public List<Word> words = new();

        // delegates
        public delegate void HandleWord(Word word);

        // events
        public event HandleWord? onWordRecognized;

        public SpeechEngine(string langCode)
        {
            internalEngine = CreateSpeechEngine(langCode);
            internalEngine.SpeechRecognized += InternalEngineOnSpeechRecognized;
        }

        public void Run() => internalEngine.RecognizeAsync(RecognizeMode.Multiple);

        public void Init()
        {
            InitializeGrammar();
            internalEngine.SetInputToDefaultAudioDevice();
        }

        private void InitializeGrammar()
        {
            Choices choices = new Choices();

            foreach (var word in words)
            {
                choices.Add(word.Text);
            }

            internalEngine.LoadGrammar(new Grammar(new GrammarBuilder(choices)));
        }

        private static SpeechRecognitionEngine CreateSpeechEngine(string langCode)
        {
            ReadOnlyCollection<RecognizerInfo> recognizers = SpeechRecognitionEngine.InstalledRecognizers();
            foreach (RecognizerInfo config in recognizers)
            {
                if (string.Equals(config.Culture.ToString(), langCode, StringComparison.CurrentCultureIgnoreCase))
                {
                    try
                    {
                        return new SpeechRecognitionEngine(langCode);
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex);
                        break;
                    }
                }
            }
            return new SpeechRecognitionEngine(recognizers[0]);
        }

        private void InternalEngineOnSpeechRecognized(object? sender, SpeechRecognizedEventArgs e) => onWordRecognized?.Invoke(new Word() { Text = e.Result.Text });
    }
}