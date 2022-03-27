using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Classification
{
    public interface IInputSetReader
    {
        List<List<double>> ReadInput(string path, int numberOfInputParameters);
    }

    public class InputSetReader : IInputSetReader
    {
        public List<List<double>> ReadInput(string path, int numberOfInputParameters)
        {
            List<List<double>> inputSet = new();
            string[] lines = System.IO.File.ReadAllLines(path);
            foreach (string line in lines)
            {
                string[] values = line.Split('\t');
                if (values.Length != numberOfInputParameters)
                {
                    throw new Exception("Invalid input file");
                }
                
                List<double> input = new();
                for (int i = 0; i < numberOfInputParameters; i++)
                {
                    input.Add(Convert.ToDouble(values[i]));
                }
                inputSet.Add(input);
            }
            return inputSet;
        }
    }
}
