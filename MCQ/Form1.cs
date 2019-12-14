﻿using System;
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
using NumSharp.Utilities;
using NumSharp;
namespace MCQ
{
    public partial class Form1 : Form
    {
        string fileName = "mcqSample.jpg";
        Image<Bgr, Byte> img;
        Image<Gray, Byte> imgGray;
        Image<Bgr, byte> imgDst;
        Image<Bgr, byte> copyImage;

        Point[][] points1;
        UMat cannyImage = new UMat(); //initializing with matrix
        public Form1()
        {
            InitializeComponent();
            img = new Image<Bgr, Byte>(fileName);
            imageBox1.Image = img;
            copyImage = img;
        }
        NDArray order(NDArray points)
        {
            var rect1 = np.zeros((4, 2),dtype: np.float32);

            var s = points.sum(1);
            rect1[0] = points[np.argmin(s)];
            rect1[2] = points[np.argmax(s)];
            //var difference
            return rect1;
        }
      

        private void Button2_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            //Browsing Image from Location
            if(ofd.ShowDialog()==DialogResult.OK)
            {
                fileName = ofd.FileName;
                img = new Image<Bgr, Byte>(fileName);
                ofd.Filter = " Image Files(*.tif;*.dcm;*.jpg;*.jpeg;*.bmp)|*.tif;*.dcm;*.jpg;*.jpeg;*.bmp";
                imageBox1.Image = img;          //Displaying image in image Box
                copyImage = img; //Copy of the orignal immage
            }
        }

        private void Button3_Click(object sender, EventArgs e)
        {
            //imgGray = new Image<Gray, Byte>(fileName);   //Converting Image to Grayscale
            imgGray = img.Convert<Gray, Byte>();
            imageBox2.Image = imgGray;
            //Converted to blurred
            CvInvoke.GaussianBlur(imgGray, imgGray, new Size(5, 5), 0);
            
            // using adaptive threshhold
            CvInvoke.AdaptiveThreshold(imgGray, imgGray, 255, Emgu.CV.CvEnum.AdaptiveThresholdType.GaussianC, Emgu.CV.CvEnum.ThresholdType.Binary, 75, 10);
            CvInvoke.BitwiseNot(imgGray, imgGray);
            imageBox3.Image = imgGray;
            //Applying canny
            //initialize canny matrix
            CvInvoke.Canny(imgGray, cannyImage, 75, 200);
            imageBox4.Image = cannyImage;
            cannyImage.ConvertTo(imgGray, Emgu.CV.CvEnum.DepthType.Default, -1, 0);
            Emgu.CV.Util.VectorOfVectorOfPoint vector = new Emgu.CV.Util.VectorOfVectorOfPoint();
            //Point[,] points = new Point[4,2];
            CvInvoke.FindContours(cannyImage, vector, null, Emgu.CV.CvEnum.RetrType.Tree, Emgu.CV.CvEnum.ChainApproxMethod.ChainApproxSimple);
            //MCvScalar() is i think used for drawing with some sort of color (last argument is defining thickness)
            CvInvoke.DrawContours(img, vector, -1, new MCvScalar(240, 0, 159),3);
            //change image variable so that user can see change in images
            points1=vector.ToArrayOfArray();
            imageBox5.Image = img;
        }

        private void Button4_Click(object sender, EventArgs e)
        {
            Emgu.CV.Util.VectorOfVectorOfPoint vecOut = new Emgu.CV.Util.VectorOfVectorOfPoint();
            CvInvoke.FindContours(cannyImage, vecOut, null, Emgu.CV.CvEnum.RetrType.List, Emgu.CV.CvEnum.ChainApproxMethod.ChainApproxSimple);
            //Point defines the x-y coordinates in 2d-plane
            //using dictionary learn about 
            Emgu.CV.Util.VectorOfPoint approx = new Emgu.CV.Util.VectorOfPoint();
            Dictionary<int, double> dict = new Dictionary<int, double>();
            
            if (vecOut.Size > 0)
            {
                for (int i = 0; i < vecOut.Size; i++)
                {
                    //calculating area of contours
                    double area = CvInvoke.ContourArea(vecOut[i]);
                    dict.Add(i, area); //adding areas in dictionary i don't know why i did that
                }
                var item = dict.OrderByDescending(v => v.Value);  //.Take(1);

                
                //Preparing for perspective transformation
                foreach (var it in item)
                {
                    int key = Convert.ToInt32(it.Key.ToString());
                    //generating arc length wrapping the doc
                    double peri = CvInvoke.ArcLength(vecOut[key], true);
                    CvInvoke.ApproxPolyDP(vecOut[key], approx, 0.02 * peri, true);
                    if (approx.Size == 0)
                    {

                    }
                    else if (approx.Size == 4)
                    {
                        try
                        {
                            MessageBox.Show("Size of approx: " + approx.Size);
                            CvInvoke.DrawContours(copyImage, vecOut, key, new MCvScalar(255, 0, 0), 5);
                           
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show(ex.Message);
                        }
                        //Rectangle rect = CvInvoke.BoundingRectangle(vector[key]);
                        //CvInvoke.Rectangle(img, rect, new MCvScalar(255, 0, 0), 3);
                        // MessageBox.Show("Vector\n" + vector.ToArrayOfArray() + "\napprox\n" + approx.ToArray());
                        break;
                    }
                }
            }
            var src = approx.ToArray();
            
            
            //src.ResolveShape();
            imageBox6.Image = copyImage;
            
            try
            {
                var nd = np.array(src, true).reshape(4, 2);

            }
            catch (Exception er)
            {
                MessageBox.Show("Eroor");
            }
         
            

        }

        private void ContourBtn_Click(object sender, EventArgs e)
        {
            Image<Gray, byte> imgContour = img.Convert<Gray, byte>().ThresholdBinary(new Gray(120), new Gray(255));
            imageBox2.Image = imgContour;
            cannyImage.ConvertTo(imgContour, Emgu.CV.CvEnum.DepthType.Default,-1,0);
            //imageBox1.Image = cannyImage;
            Emgu.CV.Util.VectorOfVectorOfPoint Contour = new Emgu.CV.Util.VectorOfVectorOfPoint();
            CvInvoke.FindContours(cannyImage, Contour, null, Emgu.CV.CvEnum.RetrType.Tree, Emgu.CV.CvEnum.ChainApproxMethod.ChainApproxSimple);
            imageBox1.Image = img;
        }
    }
}
