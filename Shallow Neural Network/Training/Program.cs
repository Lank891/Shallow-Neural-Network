using Common;
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

            NeuralNetwork neuralNetwork = new NeuralNetwork(settings.Layers, settings.ActivationFunction, settings.Epochs, settings.Momentum, settings.BatchSize,
                settings.LearningRate);
            //read from dataset
            //double X = Helpers.Normalize();
            //double Y = ;
            //neuralNetwork.Train(X,Y);
            //double Y_predicted = neuralNetwork.predict(X);

            /*plotting*/
            //https://github.com/AwokeKnowing/GnuplotCSharp
            //Just put gnuplot.cs in your project, change the first line from C:\gnuplot\bin to the location of gnuplot.exe on your system.
            //GnuPlot.Plot(X, Y);
            //GnuPlot.Plot(X, Y_predicted);

            Console.WriteLine(settings);
        }
    }
}
