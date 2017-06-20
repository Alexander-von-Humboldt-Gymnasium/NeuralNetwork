using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NeuralNetworkMG.NeuralNetwork.Components
{
    public interface INeurone
    {
        List<Connector> Connections { get; set; } // 215
        double Value { get; set; }

        void Set(double value);
        void Fire();

        double Get();
    }
}
