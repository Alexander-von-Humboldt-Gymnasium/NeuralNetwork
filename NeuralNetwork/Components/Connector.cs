using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NeuralNetworkMG.NeuralNetwork.Components
{
    public class Connector
    {
        public INeurone NeuroneA { get; set; }
        public INeurone NeuroneB { get; set; }

        public double Weight { get; set; }

        public Connector(INeurone a, INeurone b, double weight)
        {
            NeuroneA = a;
            NeuroneB = b;
            Weight = weight;
        }

        public void Fire()
        {
            NeuroneB.Set(NeuroneA.Get() * Weight);
        }


    }
}
