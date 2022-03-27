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
        public void Write(string path, List<List<double>> outputSet);
    }

    public class OutputSetWriter : IOutputSetWriter
    {
        public void Write(string path, List<List<double>> outputSet)
        {
            File.WriteAllLines(path, outputSet.Select(x => string.Join("\t", x)));
        }
    }
}
