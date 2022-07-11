using VoiceControl.Command;
using VoiceControl.Command.External;
using VoiceControl.Speech;

namespace VoiceControlApplication.Application
{
    internal class ApplicationDriver
    {
        private static CommandDatabase _database = null!;
        private static SpeechEngine _speechEngine;

        public static void Init()
        {
            CommandReceipt receipt;

            if (File.Exists("commands.txt"))
            {
                receipt = CommandReceipt.Load("commands.txt");
            }
            else
            {
                receipt = CommandReceipt.Default();
                CommandReceipt.Save("commands.txt", receipt);
            }

            ExternalCommandAssemblies.RegisterAssembly<ExternalCommandHelper>();

            _database = new CommandDatabase(receipt);

            _speechEngine = new SpeechEngine(LanguageCodes.EnUs);

            foreach (var command in _database.commands)
            {
                _speechEngine.words.Add(new Word(command.Key));
            }

            _speechEngine.onWordRecognized += (Word word) =>
            {
                var commandEntry = _database.commands[word.Text];
                commandEntry.command?.Invoke(commandEntry.commandArgs);
            };

            _speechEngine.Init();
            _speechEngine.Run();
        }

        public static bool Run()
        {
            return true;
        }

        public static void Limit(int ms) => Thread.Sleep(ms);
    }
}