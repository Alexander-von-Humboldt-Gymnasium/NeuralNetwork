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

        public Func<int, int> ActivationEnergy { get; set; } // BY DEFAULT SIGMOID
        public Func<int, int> ActivationEnergyDerivativeI { get; set; } // BY DEFAULT SIGMOID DERIVATIVE I

        // https://www.youtube.com/watch?v=YIqYBxpv53A (Forward-Pass)
        // https://www.youtube.com/watch?v=EAtQCut6Qno (Backward-Pass)
    }
}
