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
            int[] img = new int[800 *800];
            AI ai = new AI(800, 800, "test");

            ai.CreateHiddenLayer0(15);

            ai.AddTrainingMaterial(0, img);

            ai.MeshFully();
            ai.Train();


        }
    }
}
