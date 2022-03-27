using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common
{
    public class Layer
    {
        public Layer(bool isOutputLayer, int n)
        {
            IsOutputLayer=isOutputLayer;
            N=n;
            Neurons = new List<Neuron>();
            for (int i = 0; i < n; i++)
            {
                Neurons.Add(new Neuron());
            }
        }

        //the last layer is the output one
        public bool IsOutputLayer { get; set; }
        //number of neurons in the layer
        public int N { get; set; }

        public List<Neuron> Neurons { get; set; }
    }
}
