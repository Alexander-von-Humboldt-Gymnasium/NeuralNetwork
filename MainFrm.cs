using NeuralNetworkMG.NeuralNetwork;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace NeuralNetworkMG
{
    public partial class MainFrm : Form
    {
        public MainFrm()
        {
            InitializeComponent();
        }

        private void MainFrm_Load(object sender, EventArgs e)
        {
            double[] img = new double[2 * 2];
            AI ai = new AI(2, 2, "test");
            
            ai.CreateHiddenLayer0(2);
            ai.AddTrainingMaterial(0, img);
            ai.MeshFully();


            ai.Train();
            Console.WriteLine(ai.Test(img));


        }
    }
}
