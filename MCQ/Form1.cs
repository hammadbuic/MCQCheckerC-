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
        string fileName = "mcqSample.jpg";
        Image<Bgr, Byte> img;
        Image<Gray, Byte> imgGray;
        public Form1()
        {
            InitializeComponent();
            img = new Image<Bgr, Byte>(fileName);
            imageBox1.Image = img;
        }

      

        private void Button2_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            
            if(ofd.ShowDialog()==DialogResult.OK)
            {
                fileName = ofd.FileName;
                img = new Image<Bgr, Byte>(fileName);
                ofd.Filter = " Image Files(*.tif;*.dcm;*.jpg;*.jpeg;*.bmp)|*.tif;*.dcm;*.jpg;*.jpeg;*.bmp";
                imageBox1.Image = img;
            }
        }

        private void Button3_Click(object sender, EventArgs e)
        {
           imgGray = new Image<Gray, Byte>(fileName);
           imageBox2.Image = imgGray;
           imgGray = imgGray.ThresholdBinary(new Gray(120), new Gray(255));
           imageBox2.Image = imgGray;
        }

        private void Button4_Click(object sender, EventArgs e)
        {
            CvInvoke.AdaptiveThreshold(imgGray, imgGray,255, Emgu.CV.CvEnum.AdaptiveThresholdType.GaussianC, Emgu.CV.CvEnum.ThresholdType.Binary, 75, 10);
            CvInvoke.BitwiseNot(imgGray, imgGray);
            imageBox2.Image = imgGray;
        }
    }
}
