using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common
{
    public enum ActivationFunctionType
    {
        Sigmoid,
        Tanh
    }
    
    public interface IActivationFunction
    {
        double Calculate(double x);
        IEnumerable<double> Calculate(IEnumerable<double> x);
        
        double Derivative(double x);
        IEnumerable<double> Derivative(IEnumerable<double> x);
    }

    public static class ActivationFunctionFactory
    {
        public static IActivationFunction Create(ActivationFunctionType type)
        {
            switch (type)
            {
                case ActivationFunctionType.Sigmoid:
                    return new SigmoidActivationFunction();
                case ActivationFunctionType.Tanh:
                    return new TanhActivationFunction();
                default:
                    throw new ArgumentException("Unknown activation function type");
            }
        }
    }

    public class SigmoidActivationFunction : IActivationFunction
    {
        public double Calculate(double x)
        {
            return 1.0 / (1.0 + Math.Exp(-x));
        }

        public IEnumerable<double> Calculate(IEnumerable<double> x)
        {
            return x.Select(Calculate);
        }

        public double Derivative(double x)
        {
            return Calculate(x) * (1 - Calculate(x));
        }

        public IEnumerable<double> Derivative(IEnumerable<double> x)
        {
            return x.Select(Derivative);
        }
    }
    
    public class TanhActivationFunction : IActivationFunction
    {
        public double Calculate(double x)
        {
            return Math.Tanh(x);
        }

        public IEnumerable<double> Calculate(IEnumerable<double> x)
        {
            return x.Select(Calculate);
        }

        public double Derivative(double x)
        {
            return 1 - Math.Pow(Calculate(x), 2);
        }

        public IEnumerable<double> Derivative(IEnumerable<double> x)
        {
            return x.Select(Derivative);
        }
    }
}
