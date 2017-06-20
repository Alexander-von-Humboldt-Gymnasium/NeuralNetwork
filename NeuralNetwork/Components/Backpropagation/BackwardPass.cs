using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NeuralNetworkMG.NeuralNetwork.Components.Backpropagation
{
    public class BackwardPass
    {
        public double Epsilon { get; set; }
        public Func<int, int> ActivationEnergy { get; set; }
    }
}
