using Common;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Classification
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
                AllowTrailingCommas = true,
                Converters = { new JsonStringEnumConverter(JsonNamingPolicy.CamelCase) }
            };
            Settings settings = JsonSerializer.Deserialize<Settings>(settingsFileContent, jsonOptions);

            
            NeuralNetwork neuralNetwork = JsonSerializer.Deserialize<NeuralNetwork>(File.ReadAllText(settings.NetworkFilePath), jsonOptions);
            List<List<double>> inputSet = new InputSetReader().ReadInput(settings.InputFilePath, neuralNetwork.NumberOfInputs);

            List<List<double>> outputSet = new();
            foreach (var input in inputSet)
            {
                var output = neuralNetwork.Predict(input);
                outputSet.Add(output);
            }

            new OutputSetWriter().Write(settings.OutputResultFilePath,settings.OutputAllNeuronsResultFilePath, inputSet,outputSet);
        }
    }
}
