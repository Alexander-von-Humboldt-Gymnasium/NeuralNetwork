using NeuralNetworkMG.NeuralNetwork.Maths;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NeuralNetworkMG.NeuralNetwork.Components
{
    public class OutputNeurone : INeurone
    {
        public int Label { get; set; }

        public List<Connector> Connections { get; set; }
        public double Value { get; set; }

        public OutputNeurone(int label)
        {
            Label = label;
            Connections = new List<Connector>();
        }

        public double Get()
        {
            return Expression.Sigmoid(Value);
        }

        public void Set(double value)
        {
            Value += value;
        }

        public void Fire()
        {
            Console.WriteLine("Value = " + Value);
            Console.WriteLine("Result = " + Get());
        }
    }
}
