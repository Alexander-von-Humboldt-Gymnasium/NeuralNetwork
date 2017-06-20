using NeuralNetworkMG.NeuralNetwork.Maths;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NeuralNetworkMG.NeuralNetwork.Components
{
    public class WorkingNeurone : INeurone
    {
        public List<Connector> Connections { get; set; }
        public double Value { get; set; }

        public WorkingNeurone()
        {
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
            foreach (var connection in Connections)
            {
                connection.Fire();
            }
        }
    }
}
