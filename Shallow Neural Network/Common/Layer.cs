using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Common
{
    public class Layer
    {
        public Layer(int numberOfNeurons, int numberOfNeuronsInPreviousLayer)
        {
            Neurons = new List<Neuron>();
            for (int i = 0; i < numberOfNeurons; i++)
            {
                Neurons.Add(new Neuron(numberOfNeuronsInPreviousLayer));
            }
        }

        [JsonConstructor]
        public Layer(List<Neuron> neurons)
        {
            Neurons = neurons;
        }

        public List<double> ForwardPass(List<double> inputs, IActivationFunction activationFunction)
        {
            List<double> outputs = new ();
            foreach (var neuron in Neurons)
            {
                outputs.Add(neuron.ForwardPass(inputs, activationFunction));
            }
            return outputs;
        }

        public void BackwardPass(Layer nextLayer, IActivationFunction activationFunction)
        {
            for (int i = 0; i < Neurons.Count; i++)
            {
                double error = 0.0;
                foreach(var neuron in nextLayer.Neurons)
                {
                    error += neuron.Weights[i] * neuron.Delta;
                }
                Neurons[i].Delta = error * activationFunction.Derivative(Neurons[i].Output);
            }
        }

        public void BackwardPass(List<double> errors, IActivationFunction activationFunction)
        {
            for (int i = 0; i < Neurons.Count; i++)
            {
                Neurons[i].Delta = errors[i] * activationFunction.Derivative(Neurons[i].Output);
            }
        }

        public void UpdatePreparedWeightChanges(Layer previousLayer, double learningRate)
        {
            for (int neuronNumber = 0; neuronNumber < Neurons.Count; neuronNumber++)
            {
                for (int inputNumber = 0; inputNumber < previousLayer.Neurons.Count; inputNumber++)
                {
                    Neurons[neuronNumber].PreparedWeightChanges[inputNumber] -= learningRate * previousLayer.Neurons[inputNumber].Output * Neurons[neuronNumber].Delta;
                }
                Neurons[neuronNumber].PreparedBiasChange -= learningRate * Neurons[neuronNumber].Delta;
            }
        }

        public void UpdatePreparedWeightChanges(List<double> input, double learningRate)
        {
            for (int neuronNumber = 0; neuronNumber < Neurons.Count; neuronNumber++)
            {
                for (int inputNumber = 0; inputNumber < input.Count; inputNumber++)
                {
                    Neurons[neuronNumber].PreparedWeightChanges[inputNumber] -= learningRate * input[inputNumber] * Neurons[neuronNumber].Delta;
                }
                Neurons[neuronNumber].PreparedBiasChange -= learningRate * Neurons[neuronNumber].Delta;
            }
        }

        public void UpdateWeights()
        {
            foreach (var neuron in Neurons)
            {
                neuron.UpdateWeights();
            }
        }
        
        public void ResetPreparedChanges()
        {
            foreach (var neuron in Neurons)
            {
                neuron.ResetPreparedChanges();
            }
        }
        
        public List<Neuron> Neurons { get; private set; }
    }
}
