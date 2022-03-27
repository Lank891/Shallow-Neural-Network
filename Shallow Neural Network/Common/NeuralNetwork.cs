using System;
using System.Collections.Generic;
using Helpers;

namespace Common
{
    public class NeuralNetwork
    {
        //includes at least 1 hidden layer and exactly 1 output layer 
        public List<Layer> Layers { get; set; }
        public string ActivationFunction {get; set; }
        public int Epochs { get; set; }
        public double Momentum { get; set; }
        public int BatchSize { get; set; }
        public double LearningRate { get; set; }

        public NeuralNetwork(int[] listWithNumberOfNeuronsInEachLayer,string activationFunction, int epochs,
            double momentum, int batchSize=500, double learningRate=0.001)
        {
            Layers=new List<Layer>();
            int i = 0;
            if (listWithNumberOfNeuronsInEachLayer.Length<2)
            {
                Console.WriteLine("We need at least 1 hidden layer and 1 output layer");
                return;
            }
            for (; i < listWithNumberOfNeuronsInEachLayer.Length-1; i++)
            {
                Layers.Add(new Layer(false, listWithNumberOfNeuronsInEachLayer[i]));
            }
            Layers.Add(new Layer(true, listWithNumberOfNeuronsInEachLayer[i]));
            ActivationFunction=activationFunction;
            Epochs=epochs;
            Momentum=momentum;
            BatchSize=batchSize;
            LearningRate=learningRate;
        }
        /*from lecture slides*/
        /*Initialize weights W randomly; // small values
        while ¬StopCondition do
        ∆𝑊 = 0;
        for (𝑋, 𝑌) ∈ TrainingSet do
        𝑌෠= ForwardPass (𝑊, 𝑋);
        ∆𝑊 = BackwardPass (𝑌෠, 𝑌);
        𝑊 = 𝑊 + ∆𝑊;
        Elements from the traing set (𝑋, 𝑌) are usually selected in radom order*/
        public void Train(List<double> x, List<double> y)
        {
            if (BatchSize==0) { BatchSize = x.Count; }
            int i = 0;
            while (i<Epochs)
            {
                ForwardPass(x);
                BackwardPass(x, y);
                i++;
                for (int j=0; j< x.Count; j+=BatchSize)
                {
                    List<double> currentBatchWithX = x.GetRange(j, j+BatchSize);
                    List<double> currentBatchWithY = y.GetRange(j, j+BatchSize);
                    ForwardPass(currentBatchWithX);
                    BackwardPass(currentBatchWithX,currentBatchWithY);

                /*code for momentum
                    for i, weight in enumerate(self.weights):    
                        tmp_w[i] = tmp_w[i]*momentum + delta_w[i]
                        self.weights[i] = weight + learning_rate*tmp_w[i]
                    for i, bias in enumerate(self.biases):
                        tmp_b[i] = tmp_b[i]*momentum + delta_b[i]
                        self.biases[i] = bias + learning_rate*tmp_b[i]*/
                }
            }
            //return x;
        }
        public List<double> ForwardPass(List<double> x)
        {
            var layersCount = Layers.Count;
            List<double> temp = x;
            for (int i = 0; i < Layers[layersCount - 1].Neurons.Count; i++)
            {
                Neuron neuron = Layers[layersCount - 1].Neurons[i];
                neuron.Delta = neuron.A * (1 - neuron.A) * (x[i] - neuron.A);

                for (int j = layersCount - 2; j > 2; j--)
                {
                    /*code from lab 5*/
                    /*
                        outputs = a @ self.weights[i] + self.biases[i]
                        self.clean_outputs.append(outputs)
                        a = self.f_aktywacji(outputs)
                        self.A.append(a)
            
                        out = a @ self.weights[-1] + self.biases[-1]
  
            
                        self.clean_outputs.append(out)
                        self.A.append(out)
                        return out
                     * 
                     */
                }
            }
            return temp;
        }
        public void BackwardPass(List<double> x, List<double> y)
        {
            for (int i = Layers.Count - 1; i > 1; i--)
            {
                foreach (Neuron n in Layers[i].Neurons)
                {
                    n.A = Helpers.Helpers.ActivationFunction("sigmoid",n.A + n.Bias);
                    n.Bias += (LearningRate * n.Delta);
                        for (int j = 0; j < n.Weights.Count; j++)
                        n.Weights[j] += (LearningRate * Layers[i - 1].Neurons[j].A * n.Delta);
                }
            }

        }
        public List<double> Predict(List<double> x)
        {
            return ForwardPass(x);
        }

        }
        }
