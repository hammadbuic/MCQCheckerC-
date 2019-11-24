using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Emgu;
using Emgu.CV;
using Emgu.CV.Structure;
namespace MCQ
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void ImageBox1_Click(object sender, EventArgs e)
        {
            
        }

        private void ImageBox1_Click_1(object sender, EventArgs e)
        {
            string fileName = "mcqSample.jpg";
            Image<Bgr, byte> img = new Image<Bgr, byte>(fileName);
            CvInvoke.Imshow("Image", img);
            
        }

        private void Button2_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
