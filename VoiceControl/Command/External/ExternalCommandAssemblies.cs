using System.Reflection;

namespace VoiceControl.Command.External
{
    public class ExternalCommandAssemblies
    {
        public static Dictionary<string, Assembly> Assemblies { get; set; } = new();

        public static void RegisterAssembly<T>() => RegisterAssembly(typeof(T).Assembly);

        public static void RegisterAssembly(Assembly assembly)
        {
            if (assembly.FullName != null)
            {
                Assemblies.Add(assembly.FullName, assembly);
            }
        }

        public static void RemoveAssembly(Assembly assembly) => RemoveAssembly(assembly.FullName);

        public static void RemoveAssembly(string? key)
        {
            if (key != null && Assemblies.ContainsKey(key))
            {
                Assemblies.Remove(key);
            }
        }
    }
}