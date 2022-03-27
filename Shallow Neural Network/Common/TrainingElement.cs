using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common
{
    public class TrainingElement
    {
        public List<double> Input { get; set; }
        public List<double> ExpectedOutput { get; set; }

        public TrainingElement(List<double> input, List<double> expectedOutput)
        {
            Input = input;
            ExpectedOutput = expectedOutput;
        }
    }
}
