using System;
using System.Collections.Generic;

namespace Common
{
    public class NeuralNetwork
    {
        public List<Layer> Layers { get; private set; }
        public IActivationFunction ActivationFunction { get; private set; } = null;
        public ActivationFunctionType ActivationFunctionType { get; private set; }
        public int Epochs { get; private set; }
        public double Momentum { get; private set; }
        public int BatchSize { get; private set; }
        public double LearningRate { get; private set; }
        public int NumberOfInputs { get; private set; }
        public int NumberOfOutputs => Layers[^1].Neurons.Count;

        public NeuralNetwork(int numberOfInputs, int[] neuronsInLayers, ActivationFunctionType activationFunctionType, int epochs, double momentum, int batchSize, double learningRate)
        {
            Layers = new List<Layer>();
            NumberOfInputs = numberOfInputs;
            ActivationFunctionType = activationFunctionType;

            if (neuronsInLayers.Length < 2)
            {
                throw new ArgumentException("Neural network must have at least 2 layers (1 hidden and 1 output)");
            }

            for (int i = 0; i < neuronsInLayers.Length; i++)
            {
                Layers.Add(new Layer(neuronsInLayers[i], i == 0 ? numberOfInputs : neuronsInLayers[i - 1]));
            }

            Epochs = epochs;
            Momentum = momentum;
            BatchSize = batchSize;
            LearningRate = learningRate;

            LoadActivationFunction();
        }
        
        public void LoadActivationFunction() => ActivationFunction = ActivationFunctionFactory.Create(ActivationFunctionType);

        public void Train(List<TrainingElement> trainingSet)
        {
            if (BatchSize == 0) { BatchSize = trainingSet.Count; }

            for (int epoch = 0; epoch < Epochs; epoch++)
            {
                trainingSet.Shuffle();

                for (int batchStartIndex = 0; batchStartIndex < trainingSet.Count; batchStartIndex += BatchSize)
                {
                    ResetPrepartedChanges();

                    for (int elementIndex = batchStartIndex; elementIndex < batchStartIndex + BatchSize && elementIndex < trainingSet.Count; elementIndex++)
                    {
                        TrainingElement trainingElement = trainingSet[elementIndex];

                        List<double> output = ForwardPass(trainingElement.Input);
                        BackwardPass(output, trainingElement.ExpectedOutput);
                        UpdatePreparedWeightChanges(trainingElement.Input);
                    }

                    UpdateWeights();
                }

            }
        }

        private List<double> ForwardPass(List<double> input)
        {
            List<double> output = input;
            foreach (Layer layer in Layers)
            {
                output = layer.ForwardPass(output, ActivationFunction);
            }
            return output;
        }

        private void BackwardPass(List<double> output, List<double> expectedOutput)
        {
            List<double> errors = new();
            for (int i = 0; i < output.Count; i++)
            {
                errors.Add(output[i] - expectedOutput[i]);
            }
            
            Layers[^1].BackwardPass(errors, ActivationFunction);
            for (int i = Layers.Count - 2; i >= 0; i--)
            {
                Layers[i].BackwardPass(Layers[i+1], ActivationFunction);
            }
        }

        private void UpdatePreparedWeightChanges(List<double> input)
        {
            Layers[0].UpdatePreparedWeightChanges(input, LearningRate);
            for (int i = 1; i < Layers.Count; i++)
            {
                Layers[i].UpdatePreparedWeightChanges(Layers[i - 1], LearningRate);
            }
        }
        
        private void UpdateWeights() => Layers.ForEach(layer => layer.UpdateWeights());

        private void ResetPrepartedChanges() => Layers.ForEach(layer => layer.ResetPreparedChanges());

        public List<double> Predict(List<double> input) => ForwardPass(input);

    }
}
