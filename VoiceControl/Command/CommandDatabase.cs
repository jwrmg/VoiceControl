using System.Reflection;

namespace VoiceControl.Command
{
    public delegate bool CommandDelegate(string[] args);

    public class CommandDatabase
    {
        public struct InternalCommandEntry
        {
            public string textToRecognize;
            public string[] commandArgs;
            public CommandDelegate? command;
        }

        public Dictionary<string, InternalCommandEntry> commands = new();

        public CommandDatabase(CommandReceipt receipt)
        {
            foreach (var command in receipt.commands)
            {
                commands.Add(command.Value.textToRecognize, new InternalCommandEntry()
                {
                    textToRecognize = command.Value.textToRecognize,
                    commandArgs = command.Value.commandArgs,
                    command = CacheReflectionDelegate(command.Value)
                }
                );
            }
        }

        private CommandDelegate? CacheReflectionDelegate(CommandBlueprint command)
        {
            MethodInfo? info = GetMethodFromAssembly(GetType().Assembly, command.commandDelegateNamespace,
                command.commandFunction);
            if (info != null)
            {
                Console.WriteLine($"Successfully registered command: {command.textToRecognize} with function: {command.commandDelegateNamespace}.{command.commandFunction}");
                return (CommandDelegate)Delegate.CreateDelegate(typeof(CommandDelegate), info);
            }

            foreach (var assembly in External.ExternalCommandAssemblies.Assemblies)
            {
                MethodInfo? exInfo = GetMethodFromAssembly(assembly.Value, command.commandDelegateNamespace,
                    command.commandFunction);
                if (exInfo != null)
                {
                    Console.WriteLine($"Successfully registered command: {command.textToRecognize} with function: {command.commandDelegateNamespace}.{command.commandFunction}");
                    return (CommandDelegate)Delegate.CreateDelegate(typeof(CommandDelegate), exInfo);
                }
            }

            return null;
        }

        private static MethodInfo? GetMethodFromAssembly(Assembly assembly, string className, string methodName)
        {
            Type? assemblyType = assembly.GetType(className);
            if (assemblyType != null)
            {
                MethodInfo? assemblyInfo = assemblyType.GetMethod(methodName);
                if (assemblyInfo != null)
                {
                    return assemblyInfo;
                }
            }
            return null;
        }
    }
}