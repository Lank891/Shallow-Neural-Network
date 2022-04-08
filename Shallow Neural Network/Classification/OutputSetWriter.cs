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
        public void Write(string path, string allNeuronsPath, List<List<double>> inputSet,List<List<double>> outputSet);
    }

    public class OutputSetWriter : IOutputSetWriter
    {
        public void Write(string path,string allNeuronsPath, List<List<double>> inputSet, List<List<double>> outputSet)
        {
            List<string> vs = new List<string>();
            for(int i = 0; i < inputSet.Count; i++)
            {
                string classID = OneHot(outputSet[i]);
                vs.Add(string.Join("\t", inputSet[i])+"\t"+classID);

            }
            File.WriteAllLines(path, vs); /*outputs ids of classes found for the given inputs*/
            File.WriteAllLines(allNeuronsPath, outputSet.Select(x => string.Join("\t", x))); /*outputs values returned by each output neuron*/
        }

        public static string OneHot(List<double> listOfOutputsForOneInput)
        {
            return (listOfOutputsForOneInput.IndexOf(listOfOutputsForOneInput.Max())+1).ToString();
        }
    }
}
