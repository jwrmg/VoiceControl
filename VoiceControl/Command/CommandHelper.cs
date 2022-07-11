namespace VoiceControl.Command
{
    internal static class CommandHelper
    {
        public static bool TestFunction(string[] args)
        {
            foreach (var arg in External.ExternalCommandAssemblies.Assemblies)
            {
                Console.WriteLine(arg.Key);
            }

            return false;
        }

        public static bool TestFunction2(string[] args)
        {
            foreach (var arg in args)
            {
                Console.WriteLine(arg);
                Console.Beep();
            }

            return false;
        }
    }
}