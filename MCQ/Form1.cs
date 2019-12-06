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
        Image<Bgr, byte> imgDst;
        UMat cannyImage = new UMat(); //initializing with matrix
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
            //Browsing Image from Location
            if(ofd.ShowDialog()==DialogResult.OK)
            {
                fileName = ofd.FileName;
                img = new Image<Bgr, Byte>(fileName);
                ofd.Filter = " Image Files(*.tif;*.dcm;*.jpg;*.jpeg;*.bmp)|*.tif;*.dcm;*.jpg;*.jpeg;*.bmp";
                imageBox1.Image = img;          //Displaying image in image Box
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
            CvInvoke.FindContours(cannyImage, vector, null, Emgu.CV.CvEnum.RetrType.External, Emgu.CV.CvEnum.ChainApproxMethod.ChainApproxSimple);
            //MCvScalar() is i think used for drawing with some sort of color (last argument is defining thickness)
            CvInvoke.DrawContours(img, vector, -1, new MCvScalar(240, 0, 159),3);
            //change image variable so that user can see change in images
            imageBox5.Image = img;
            //Point defines the x-y coordinates in 2d-plane
            //using dictionary learn about 
            Emgu.CV.Util.VectorOfPointF approx = new Emgu.CV.Util.VectorOfPointF();
           // UMat approx = new UMat();
            
            Dictionary<int, double> dict = new Dictionary<int, double>();
            if (vector.Size > 0)
            {
                for (int i = 0; i < vector.Size; i++)
                {
                    //calculating area of contours
                    double area = CvInvoke.ContourArea(vector[i]);
                    dict.Add(i,area); //adding areas in dictionary i don't know why i did that
                }
                var item = dict.OrderByDescending(v => v.Value);  //.Take(1);
                
                
                //Preparing for perspective transformation
                foreach(var it in item)
                {
                    int key = Convert.ToInt32(it.Key.ToString());
                    //generating arc length wrapping the doc
                    double peri = CvInvoke.ArcLength(vector[key], true);
                    MessageBox.Show(Convert.ToString(peri));
                    CvInvoke.ApproxPolyDP(vector[key],approx, 0.02 * peri, true);
                    if(approx.Size==0)
                    {
                        
                    }
                    else if(approx.Size==4)
                    {
                        //Rectangle rect = CvInvoke.BoundingRectangle(vector[key]);
                        //CvInvoke.Rectangle(img, rect, new MCvScalar(255, 0, 0), 3);
                        CvInvoke.DrawContours(img, vector, key, new MCvScalar(255, 0, 0), 5);
                        MessageBox.Show("Vector\n" + vector.ToArrayOfArray() + "\napprox\n" + approx.ToArray());
                        break;
                    }
                }
            }
            imageBox6.Image = img;
            ///var a=vector.ToArrayOfArray();
            ///var b = approx.ToString();
            //MessageBox.Show(b);
            
            Mat approxMat = new Mat();
            //MessageBox.Show("is umat "+approx.IsUmat());
            //CvInvoke.PerspectiveTransform(img, imgDst, approx);
            approxMat=CvInvoke.GetPerspectiveTransform(approx,vector);
            //CvInvoke.GetPerspectiveTransform(approx, vector);
            //CvInvoke.PerspectiveTransform(img, imgDst, approxMat);
            imageBox7.Image = imgDst;
        }

        private void Button4_Click(object sender, EventArgs e)
        {

            
            
            //imageBox2.Image = imgGray; //Addaptive threshhold
            
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
