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
        }
    }
}