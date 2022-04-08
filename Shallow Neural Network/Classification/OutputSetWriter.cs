using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Classification
{
    public interface IOutputSetWriter
    {
        public void Write(string path, string allNeuronsPath, List<List<double>> inputSet, List<List<double>> outputSet);
    }

    public class OutputSetWriter : IOutputSetWriter
    {
        public void Write(string path, string allNeuronsPath, List<List<double>> inputSet, List<List<double>> outputSet)
        {
            List<string> outputLines = new List<string>();
            for(int i = 0; i < inputSet.Count; i++)
            {
                int classIndex = PredictedClassIndex(outputSet[i]);
                outputLines.Add(string.Join("\t", inputSet[i]) + "\t" + classIndex.ToString());

            }
            File.WriteAllLines(path, outputLines); /* Outputs ids of classes found for the given inputs */
            File.WriteAllLines(allNeuronsPath, outputSet.Select(x => string.Join("\t", x))); /* Outputs values returned by each output neuron */
        }

        public static int PredictedClassIndex(List<double> listOfOutputsForOneInput)
        {
            return listOfOutputsForOneInput.IndexOf(listOfOutputsForOneInput.Max()) + 1;
        }
    }
}
