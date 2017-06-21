using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NeuralNetworkMG.NeuralNetwork.Components
{
    public class Layer
    {
        public Layer NextLayer { get; set; }
        public Layer PreviousLayer { get; set; }

        public INeurone Bias { get; private set; }
        public INeurone[] Neurones { get; private set; }

        public double[] Values { get; private set; }
        public double[] DeltaValues { get; private set; }

        public double[] WantedValues { get; private set; }
        public double[] ActivationValues { get; private set; }

        public bool IsOutputLayer { get; private set; }

        public Func<double, double> ActivationFunction { get; set; }
        public Func<double, double> Derivative { get; set; }
 

        public Layer(INeurone[] neurones, INeurone bias, Layer next, Layer previous)
        {
            NextLayer = next;
            PreviousLayer = previous;

            Neurones = neurones;
            Bias = bias;

            Values = new double[Neurones.Length];
            ActivationValues = new double[Neurones.Length];

            for (int i = 0; i < Neurones.Length; i++)
            {
                Values[i] = Neurones[i].Value;
                ActivationValues[i] = Neurones[i].Get();
            }

            IsOutputLayer = Neurones[0] is OutputNeurone;
            
        }



        public List<double[]> GetDeltaWeights(double epsilon)
        {
            List<double[]> delta_weights = new List<double[]>();
            if (IsOutputLayer)
            {
                DeltaValues = new double[Neurones.Length];
                for (int i = 0; i < DeltaValues.Length; i++)
                    DeltaValues[i] = Derivative(Values[i]) * (WantedValues[i] - ActivationValues[i]);

                double bias = PreviousLayer.Bias.Get();
                double[] activations = PreviousLayer.ActivationValues;
                double[] complete = new double[1 + PreviousLayer.Neurones.Length];

                for (int i = 0; i < DeltaValues.Length; i++)
                {
                    int k = 0;
                    complete[k++] = bias * DeltaValues[i] * epsilon;
                    
                    for (int j = 0; j < activations.Length; j++)
                    {
                        complete[k++] = activations[j] * DeltaValues[i] * epsilon;
                    }
                    delta_weights.Add(complete);
                }
                
            }
            else
            {
                DeltaValues = new double[Neurones.Length];

                for (int i = 0; i < DeltaValues.Length; i++)
                {
                    double sum = 0.0;
                    for (int j = 0; j < Neurones.Length; j++)
                    {
                        for (int k = 0; k < Neurones[j].Connections.Count; k++)
                        {
                            sum += Neurones[j].Connections[k].Weight * NextLayer.DeltaValues[k];
                        }

                    }

                    DeltaValues[i] = Derivative(Values[i]) * sum;
                }

                double bias = PreviousLayer.Bias.Get();
                double[] activations = PreviousLayer.ActivationValues;
                double[] complete = new double[1 + PreviousLayer.Neurones.Length];

                for (int i = 0; i < DeltaValues.Length; i++)
                {
                    int k = 0;
                    complete[k++] = bias * DeltaValues[i] * epsilon;

                    for (int j = 0; j < activations.Length; j++)
                    {
                        complete[k++] = activations[j] * DeltaValues[i] * epsilon;
                    }
                    delta_weights.Add(complete);
                }
            }

            return delta_weights;
        }

        public void SetWantedValues(params double[] values)
        {
            WantedValues = new double[values.Length];
            for (int i = 0; i < values.Length; i++)
            {
                WantedValues[i] = values[i];
            }
        }

       
    }
}
