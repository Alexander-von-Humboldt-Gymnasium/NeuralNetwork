﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.Xna.Framework.Graphics;
using NeuralNetworkMG.NeuralNetwork.Components;
using NeuralNetworkMG.NeuralNetwork.Components.Backpropagation;
using Microsoft.Xna.Framework;

namespace NeuralNetworkMG.NeuralNetwork
{
    public class AI
    {
        public InputNeurone[] InputLayer { get; private set; }

        public List<WorkingNeurone> HiddenLayer { get; private set; }
        public OutputNeurone OutputNeurone { get; private set; }

        public InputNeurone Bias0 { get; private set; }
        public InputNeurone Bias1 { get; private set; }

        public List<double[]> TrainingSets { get; private set; }
        public string Description { get; set; }

        public int Width { get; set; }
        public int Height { get; set; }

        private bool _isMeshed;
        private bool _createdHiddenLayer;

        private static Random RAND = new Random();

        public AI(int width, int height, string description)
        {

            Width = width;
            Height = height;

            TrainingSets = new List<double[]>();

            InputLayer = new InputNeurone[Width * Height];

            HiddenLayer = new List<WorkingNeurone>();

            for (int i = 0; i < InputLayer.Length; i++)
            {
                InputLayer[i] = new InputNeurone();
            }

            Bias0 = new InputNeurone();
            Bias0.Set(1.0);

            Bias1 = new InputNeurone();
            Bias1.Set(1.0);
        }

        public void AddTrainingMaterial(int label, double[] training)
        {
            if (OutputNeurone == null)
                OutputNeurone = new OutputNeurone(label);

            TrainingSets.Add(training);
        }

        public void CreateHiddenLayer0(int max)
        {
            
            for (int i = 0; i < max; i++)
            {
                HiddenLayer.Add(new WorkingNeurone());
            }
            _createdHiddenLayer = true;
        }

        public void MeshFully()
        {
            if (!_createdHiddenLayer)
                return;


            #region Biases
            for (int i = 0; i < HiddenLayer.Count; i++)
            {
                WorkingNeurone b = HiddenLayer[i];
                Bias0.Connections.Add(new Connector(Bias0, b, RAND.NextDouble() * RAND.Next(-2, 3)));
            }
            Bias1.Connections.Add(new Connector(Bias1, OutputNeurone, RAND.NextDouble() * RAND.Next(-2, 3)));
            

            #endregion

            #region Input and Working Neurones
            for (int i = 0; i < InputLayer.Length; i++)
            {
                InputNeurone a = InputLayer[i];
                for (int j = 0; j < HiddenLayer.Count; j++)
                {
                    WorkingNeurone b = HiddenLayer[j];
                    a.Connections.Add(new Connector(a, b, RAND.NextDouble() * RAND.Next(-2, 3)));
                }
            }

            for (int i = 0; i < HiddenLayer.Count; i++)
            {
                WorkingNeurone a = HiddenLayer[i];
                OutputNeurone b = OutputNeurone;
                a.Connections.Add(new Connector(a, b, RAND.NextDouble() * RAND.Next(-2, 3)));

            }
            #endregion


            _isMeshed = true;
        }

        

        public void Train()
        {
            if (!_isMeshed)
                return;

            for (int i = 0; i < TrainingSets.Count; i++)
            {
                __processInternal(TrainingSets[i]);
                if (OutputNeurone.Get() != 1.0)
                {
                    BackwardPass backwardPass = new BackwardPass(this, 1, .5);
                    backwardPass.Adjust();

                    for (int j = 0; j < Bias0.Connections.Count; j++)
                        Bias0.Connections[j].AdjustWeight(backwardPass.L_Weight_Hidden[j][0]);

                    for (int j = 0; j < Bias1.Connections.Count; j++)
                        Bias1.Connections[j].AdjustWeight(backwardPass.L_Weights_Out[j][0]);

                    for (int j = 0; j < InputLayer.Length; j++)
                    {
                        var neurone = InputLayer[j];
                        for (int k = 0; k < neurone.Connections.Count; k++)
                        {
                            for (int l = 0; l < backwardPass.L_Weight_Hidden[k].Length; l++)
                            {
                                neurone.Connections[k].AdjustWeight(backwardPass.L_Weight_Hidden[k][l]);
                            }

                        }
                    }

                    for (int j = 0; j < HiddenLayer.Count; j++)
                    {
                        var neurone = HiddenLayer[j];
                        for (int k = 0; k < neurone.Connections.Count; k++)
                        {
                            for (int l = 0; l < backwardPass.L_Weights_Out[k].Length; l++)
                            {
                                neurone.Connections[k].AdjustWeight(backwardPass.L_Weights_Out[k][l]);
                            }

                        }
                    }
                }
            }

        }

        private void __processInternal(double[] image)
        {
            for (int i = 0; i < image.Length; i++)
                InputLayer[i].Set(image[i]);

            Bias0.Fire();
            for (int i = 0; i < InputLayer.Length; i++)
                InputLayer[i].Fire();

            Bias1.Fire();
            for (int i = 0; i < HiddenLayer.Count; i++)
                HiddenLayer[i].Fire();

            OutputNeurone.Fire();

        }

        public Vector2 Test(double[] image)
        {
            __processInternal(image);

            return new Vector2((float)OutputNeurone.Value, (float)OutputNeurone.Get());
        }
    }
}
