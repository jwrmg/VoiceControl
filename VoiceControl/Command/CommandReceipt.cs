using System.Diagnostics;
using Newtonsoft.Json;

namespace VoiceControl.Command
{
    public class CommandReceipt
    {
        public Dictionary<string, CommandBlueprint> commands = new();

        public static void Save(string filePath, CommandReceipt receipt)
        {
            string json = JsonConvert.SerializeObject(receipt, new JsonSerializerSettings
            {
                Formatting = Formatting.Indented,
                NullValueHandling = NullValueHandling.Include
            });
            File.WriteAllText(filePath, json);
        }

        public static CommandReceipt Load(string filePath)
        {
            string json = File.ReadAllText(filePath);
            if (string.IsNullOrEmpty(json))
            {
                // TODO: Log Empty Json.
                return new CommandReceipt();
            }

            CommandReceipt receipt = JsonConvert.DeserializeObject<CommandReceipt>(json, new JsonSerializerSettings
            {
                Formatting = Formatting.Indented,
                NullValueHandling = NullValueHandling.Include
            });
            return receipt;
        }

        public static CommandReceipt Default()
        {
            return null;
        }
    }
}