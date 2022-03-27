using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common
{
    public class Helpers
    {
        public static List<double> ActivationFunction(string name, List<double> x, bool isDerivative = false)
        {
            switch (name)
            {
                case "sigmoid":
                    return Sigmoid(x, isDerivative);
                case "tanh":
                    return Tanh(x, isDerivative);
                default:
                    return Identity(x);
            }
        }
        public static double ActivationFunction(string name, double x, bool isDerivative = false)
        {
            switch (name)
            {
                case "sigmoid":
                    return Sigmoid(x, isDerivative);
                case "tanh":
                    return Tanh(x, isDerivative);
                default:
                    return Identity(x);
            }
        }
        public static double Sigmoid(double x, bool isDerivative = false)
        {
            if (isDerivative)
            {
                return x*(1.0-x);
            }
            else
            {
                return 1.0/(1.0+Math.Exp(-x));
            }

        }
        public static List<double> Sigmoid(List<double> x, bool isDerivative = false)
        {
            return x.Select(item => Sigmoid(item, isDerivative)).ToList();
        }
        public static double Tanh(double x, bool isDerivative = false)
        {
            if (isDerivative)
            {
                return 1.0/(Math.Cosh(x)*Math.Cosh(x));
            }
            else
            {
                return Math.Tanh(x);
            }
        }
        public static List<double> Tanh(List<double> x, bool isDerivative = false)
        {
            return x.Select(item => Tanh(item, isDerivative)).ToList();
        }
        public static double Identity(double x)
        {
            return x;
        }
        public static List<double> Identity(List<double> x)
        {
            return x;
        }

    }
}
