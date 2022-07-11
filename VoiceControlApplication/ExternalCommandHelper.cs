using VoiceControl.Command.External;

namespace VoiceControlApplication
{
    internal class ExternalCommandHelper
    {
        public static bool ExternalTestFunction(string[] args)
        {
            foreach (var arg in args)
            {
                Console.WriteLine($"This is from an external test function: {arg}");
            }
            return false;
        }
    }
}