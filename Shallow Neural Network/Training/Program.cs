using System;
using System.IO;
using System.Text.Json;

namespace Training
{
    internal class Program
    {
        static void Main(string[] args)
        {
            if (args.Length != 1)
            {
                Console.WriteLine("Provide 1 argument - path to settings file.");
                return;
            }

            string path = args[0];
            if (!File.Exists(path))
            {
                Console.WriteLine("Input file does not exist or you do not have permission to read it.");
                return;
            }

            string settingsFileContent = File.ReadAllText(path);

            var jsonOptions = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true,
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                WriteIndented = true,
                ReadCommentHandling = JsonCommentHandling.Skip,
                AllowTrailingCommas = true
            };
            Settings settings = JsonSerializer.Deserialize<Settings>(settingsFileContent, jsonOptions);

            Console.WriteLine(settings.Momentum);
        }
    }
}
