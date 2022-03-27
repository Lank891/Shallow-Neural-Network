using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common
{
    public class Neuron
    {
        public Neuron()
        {
            Weights = new List<double>();
            Bias = new Random().NextDouble()* (1 - (-1)) - 1;//gives a double between -1.0 and 1.0
        }

        public List<double> Weights { get; set; }
        public double Bias { get; set; }
        public double Delta { get; set; }

    }
}
