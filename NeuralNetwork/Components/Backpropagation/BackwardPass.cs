using NeuralNetworkMG.NeuralNetwork.Maths;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NeuralNetworkMG.NeuralNetwork.Components.Backpropagation
{
    public class BackwardPass
    {
        public AI NeuralNetwork { get; private set; }
        public double Epsilon { get; private set; }

        public Layer L_Out { get; private set; }
        public Layer L_Hidden { get; private set; }


        public List<double[]> L_Weights_Out { get; private set; }
        public List<double[]> L_Weight_Hidden { get; private set; }

        public BackwardPass(AI nn, double eps, params double[] requestedValues)
        {
            NeuralNetwork = nn;
            Epsilon = eps;


            Layer L2 = new Layer(new INeurone[] { nn.OutputNeurone }, null, null, null);
            Layer L1 = new Layer(nn.HiddenLayer.ToArray(), nn.Bias1, L2, null);
            L2.PreviousLayer = L1;
            Layer L0 = new Layer(nn.InputLayer, nn.Bias0, L1, null);
            L1.PreviousLayer = L0;

            L2.ActivationFunction = Expression.Sigmoid;
            L2.Derivative = Expression.DerivativeI_Sigmoid;

            L1.ActivationFunction = Expression.Sigmoid;
            L1.Derivative = Expression.DerivativeI_Sigmoid;

            L_Out = L2;
            L_Hidden = L1;

            L_Out.SetWantedValues(requestedValues);

 

             
        }

        public void Adjust()
        {
            L_Weights_Out = L_Out.GetDeltaWeights(Epsilon);
            L_Weight_Hidden = L_Hidden.GetDeltaWeights(Epsilon);
        }
        

    }
}
