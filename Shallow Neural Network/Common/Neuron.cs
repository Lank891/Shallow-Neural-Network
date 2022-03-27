using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Common
{
    public class Neuron
    {
        private static readonly Random random = new();

        public Neuron(int numberOfNeuronsInPreviousLayer)
        {
            Weights = new List<double>();
            Bias = random.NextDouble() * 2 - 1; //between -1.0 and 1.0
            for (int i = 0; i < numberOfNeuronsInPreviousLayer; i++)
            {
                Weights.Add(random.NextDouble() * 0.5 - 0.25); //between -0.25 and 0.25
            }

            ResetPreparedChanges();
        }

        [JsonConstructor]
        public Neuron(List<double> weights, List<double> preparedWeightChanges, double bias, double preparedBiasChange, double delta, double output)
        {
            Weights = weights;
            PreparedWeightChanges = preparedWeightChanges;
            Bias = bias;
            PreparedBiasChange = preparedBiasChange;
            Delta = delta;
            Output = output;
        }

        public double ForwardPass(List<double> inputs, IActivationFunction activationFunction)
        {
            double sum = 0;
            for (int i = 0; i < inputs.Count; i++)
            {
                sum += inputs[i] * Weights[i];
            }
            sum += Bias;
            return activationFunction.Calculate(sum);
        }

        public void UpdateWeights()
        {
            for (int i = 0; i < Weights.Count; i++)
            {
                Weights[i] += PreparedWeightChanges[i];
            }
            Bias += PreparedBiasChange;
        }
        
        public void ResetPreparedChanges()
        {
            PreparedWeightChanges = Weights.Select(w => 0.0).ToList();
            PreparedBiasChange = 0;
        }
        
        public List<double> Weights { get; set; }
        public List<double> PreparedWeightChanges { get; set; }
        public double Bias { get; set; }
        public double PreparedBiasChange { get; set; }
        public double Delta { get; set; }
        public double Output { get; set; }

    }
}
