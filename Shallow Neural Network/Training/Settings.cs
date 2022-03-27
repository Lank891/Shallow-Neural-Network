using Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Training
{
    internal class Settings
    {
        public string DataSetPath { get; set; } = null;
        public int NumberOfInputParameters { get; set; }
        public string OutputNetworkFilePath { get; set; } = null;
        public ActivationFunctionType ActivationFunction { get; set; }
        public int Epochs { get; set; }
        public double Momentum { get; set; }
        public int BatchSize { get; set; }
        public double LearningRate { get; set; }
        public int[] Layers { get; set; }
    }
}
 