using System;
using System.Collections.Generic;

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

        public NeuralNetwork(List<int> listWithNumberOfNeuronsInEachLayer,string activationFunction, int epochs,
            double momentum, int batchSize=500, double learningRate=0.001)
        {
            Layers=new List<Layer>();
            int i = 0;
            if (listWithNumberOfNeuronsInEachLayer.Count<2)
            {
                Console.WriteLine("We need at least 1 hidden layer and 1 output layer");
                return;
            }
            for (; i < listWithNumberOfNeuronsInEachLayer.Count-1; i++)
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

        public static List<double> Train(List<double> x, List<double> y)
        {
            ForwardPass(x);
            BackwardPass(x, y);
            return x;
        }
        public static void ForwardPass(List<double> x)
        {

        }
        public static void BackwardPass(List<double> x, List<double> y)
        {
            
        }
        public static List<double> Predict(List<double> x)
        {
            return x;
        }

    }
}
