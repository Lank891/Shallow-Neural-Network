using Common;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Training
{
    public interface ITrainingSetReader
    {
        List<TrainingElement> ReadTrainingSet(string path, int numberOfInputParameters, int numberOfClasses);
    }

    public class ClassificationTrainingSetReader : ITrainingSetReader
    {
        private readonly bool _classStartFromZero;

        /// <summary>
        /// Assumes input in form: param1\tparam2\tparam3\tclass
        /// </summary>
        public ClassificationTrainingSetReader(bool classStartFromZero)
        {
            _classStartFromZero = classStartFromZero;
        }

        public List<TrainingElement> ReadTrainingSet(string path, int numberOfInputParameters, int numberOfClasses)
        {
            var trainingSet = new List<TrainingElement>();
            var lines = File.ReadAllLines(path);
            foreach (var line in lines)
            {
                var parts = line.Split('\t');
                
                if(parts.Length != numberOfInputParameters + 1)
                    throw new Exception("Invalid training set format - no. of parameters in one element does not match number of input parameters + number of classes");
                
                List<double> inputParameters = parts[0..^1].Select(x => double.Parse(x)).ToList();
                inputParameters.Normalize();
                
                List<double> outputParameters = new();
                for (int i = 0; i < numberOfClasses; i++)
                    outputParameters.Add(0);

                int expectedClass = int.Parse(parts.Last());
                outputParameters[_classStartFromZero ? expectedClass : expectedClass - 1] = 1;

                trainingSet.Add(new TrainingElement(inputParameters, outputParameters));
            }
            return trainingSet;
        }
    }
}
