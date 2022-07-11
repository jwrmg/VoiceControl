using VoiceControl.Command;
using VoiceControl.Command.External;
using VoiceControlApplication.Application;

namespace VoiceControlApplication
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            ApplicationDriver.Init();
            while (ApplicationDriver.Run())
            {
                ApplicationDriver.Limit(16);
            }

            CommandReceipt receipt = new();
            CommandBlueprint entry = new CommandBlueprint()
            { };


            receipt = CommandReceipt.Load("commands.txt");

            //receipt.commands.Add("test", new CommandBlueprint()
            //{
            //    textToRecognize = "EmptyText",
            //    commandArgs = new[] { "emptyArgs" },
            //    commandFunction = "TestFunction",
            //    commandDelegateNamespace = "VoiceControl.Command.CommandHelper"
            //});
            //
            ////public string textToRecognize = "EmptyText";
            ////public string[] commandArgs = { "emptyArguments" };
            ////public string commandFunction = "EmptyFunction";
            ////public string commandDelegateNamespace = "VoiceControl.Command.CommandHelper";
            //
            //CommandReceipt.Save("commands.txt", receipt);

            CommandDatabase database = new(receipt);

            database.commands["test"].command?.Invoke(new[] { "Hello", "There,", "General Kenobi" });
        }
    }
}