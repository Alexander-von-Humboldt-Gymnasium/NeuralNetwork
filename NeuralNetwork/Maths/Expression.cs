using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NeuralNetworkMG.NeuralNetwork.Maths
{
    public static class Expression
    {
        public static Func<double, double> Sigmoid = (x) =>
        {
            return 1.0 / (1.0 + Math.Pow(Math.E, -x));
        };

        public static Func<double, double> DerivativeI_Sigmoid = (x) =>
        {
            return Sigmoid(x) * (1 - Sigmoid(x));
        };

        public static Func<double, double> Linear = (x) =>
        {
            return x;
        };

        public static Func<double, double> Step = (x) =>
        {
            return Math.Log(1.0 + Math.Pow(Math.E, x));
        };

        public static Func<double, double> Clamp = (x) =>
        {
            return MathHelper.Clamp((float)x, 0, (float)1.0);
        };


        public static double Evaluate2d(Func<double, double> func, double x)
        {
            return func(x);
        }
    }
}
