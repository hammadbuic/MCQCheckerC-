using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Windows.Forms;
using Emgu.CV;
using Emgu.Util;
using Emgu.CV.Structure;
using System.IO;
using AForge.Imaging;
namespace MCQ
{
    public partial class Form1 : Form
    {
        string fileName = "mcqSample.jpg";
        Image<Bgr, Byte> img;
        Image<Gray, Byte> imgGray;
        Image<Bgr, byte> copyImage;
        Image<Bgr, float> cpyImgPerspect;
        Image<Bgr, byte> transformedImage;
        Bitmap bitmapImage;
        UMat cannyImage = new UMat(); //initializing with matrix

        public Form1()
        {
            InitializeComponent();
            img = new Image<Bgr, Byte>(fileName);
            orignalImage.Image = img;
            copyImage = img;
            cpyImgPerspect = new Image<Bgr, float>(fileName);
            bitmapImage = new Bitmap(fileName);
            
        }
        PointF[] order(Emgu.CV.Util.VectorOfPoint p)
        {
            PointF[] rectPoint = new PointF[4];
            int[] sum = new int[4];
            for(int i=0;i<sum.Length;i++)  //Traversing for sum of p(x+y) & save in array
            {
                sum[i] = p[i].X + p[i].Y;
            }
            // Getting Index of Point with Maximum (x+y) sum:: Bottom Right Point [2]
            int maxValue = sum.Max();
            int maxIndex = sum.ToList<int>().IndexOf(maxValue);
            
            rectPoint[2] = p[maxIndex];   //Setting to bottom-right point

            // Getting index of Point with Minimum (x+y) sum :: Top-Left Point [0]
            int minValue = sum.Min();
            int minIndex = sum.ToList<int>().IndexOf(minValue);

            rectPoint[0] = p[minIndex];  //setting to Top-left Point

            int[] diff = new int[4];
            for(int i=0;i<diff.Length;i++)  //Traversing for difference p(x-y) and save in array
            {
                diff[i] = p[i].X - p[i].Y;
                MessageBox.Show("diff " + i + " = " + diff[i]);
            }

            // Getting index of Point with minimum (x-y) diff:: Top-right Point [1]

            int minDiff = diff.Min();
            int minDiffIndex = diff.ToList<int>().IndexOf(minDiff);

            rectPoint[1] = p[minDiffIndex];  // Setting Top-Right Point

            // Getting Index of point with maximum (x-y) diff :: Bottom-Left Point [3]

            int maxDiff = diff.Max();
            int maxDiffIndex = diff.ToList<int>().IndexOf(maxDiff);

            rectPoint[3] = p[maxDiffIndex]; // Setting Bottom-left Point
            MessageBox.Show("max diff: " + rectPoint[3]);

            //return rectPoint  top-left [0] :  top-right [1]: bottom-right [2]: bottom-left [3]
            return rectPoint;
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
                orignalImage.Image = img;          //Displaying image in image Box
                copyImage = img; //Copy of the orignal immage
                cpyImgPerspect = new Image<Bgr, float>(fileName);
                bitmapImage = new Bitmap(fileName);
            }
        }
        Image<Bgr,float> transformImage(Image<Bgr,float> image,Emgu.CV.Util.VectorOfPoint points)
        {
            var orderPoints=order(points);      // Ordering the Points in Particular Order
           for(int i=0;i<points.Size;i++)
            {
                MessageBox.Show("X= " + orderPoints[i].X + "Y= " + orderPoints[i].Y);
            }
            //Assigning to Points Orderwise For Mathematical Operations
            PointF topLeft = orderPoints[0];
            PointF topRight = orderPoints[1];
            PointF bottomRight = orderPoints[2];
            PointF bottomLeft = orderPoints[3];

            // Calculating Width

            double widthSqrt = Math.Pow(bottomRight.X - bottomLeft.X, 2) + Math.Pow(bottomRight.Y - bottomLeft.Y, 2);
            double widthA = Math.Sqrt(widthSqrt);
            double widthBsqrt = Math.Pow(topRight.X - topLeft.X, 2) + Math.Pow(topRight.Y - topLeft.Y, 2);
            double widthB = Math.Sqrt(widthBsqrt);
            int maxWidth = Convert.ToInt32(Math.Max(widthA, widthB));

            // Calculating Height

            double heightSqrt = Math.Pow(topRight.X - bottomRight.X, 2) + Math.Pow(topRight.Y - bottomRight.Y, 2);
            double heightA = Math.Sqrt(heightSqrt);
            double heightBSqrt = Math.Pow(topLeft.X - bottomLeft.X, 2) + Math.Pow(topLeft.Y - bottomLeft.Y, 2);
            double heightB = Math.Sqrt(heightBSqrt);
            int maxHeight = Convert.ToInt32(Math.Max(heightA, heightB));

            // getting dest Points
            MessageBox.Show("Max Width: " + maxWidth + " Max Height: " + maxHeight);

            PointF[] destPoints = new PointF[4]
            {
                new PointF { X = 0 , Y = 0 },
                new PointF { X = maxWidth - 1 , Y = 0 },
                new PointF { X = maxWidth - 1 ,Y = maxHeight - 1 },
                new PointF { X =  0, Y = maxHeight - 1 }
            };
             //var mat=CvInvoke.FindHomography(orderPoints, destPoints, Emgu.CV.CvEnum.RobustEstimationAlgorithm.AllPoints, 3, null);
            //var mat=Mat.Zeros(3, 3, Emgu.CV.CvEnum.DepthType.Cv32F, 0);
            var mat = CvInvoke.GetPerspectiveTransform(orderPoints, destPoints);
            MessageBox.Show("Mat rows: " + mat.Rows + " Mat cols: " + mat.Cols);
            Matrix<float> matrix = new Matrix<float>(3, 3);
            mat.CopyTo(matrix);
            //var mat = CvInvoke.GetAffineTransform(orderPoints, destPoints);
            //image = image.WarpAffine(mat, Emgu.CV.CvEnum.Inter.Linear, Emgu.CV.CvEnum.Warp.Default, Emgu.CV.CvEnum.BorderType.Constant,new Bgr(23,40,60));

            //image = mat.ToImage<Bgr, float>();
            //CvInvoke.Invert(mat, mat, Emgu.CV.CvEnum.DecompMethod.LU);
            Image<Bgr, float> destImage = new Image<Bgr, float>(maxWidth, maxHeight);
            image=image.Resize(   maxWidth, maxHeight, Emgu.CV.CvEnum.Inter.Linear);
            MessageBox.Show("Size of Image: " + image.Size);
            CvInvoke.WarpPerspective(image, image, mat, image.Size, Emgu.CV.CvEnum.Inter.Lanczos4, Emgu.CV.CvEnum.Warp.FillOutliers, Emgu.CV.CvEnum.BorderType.Constant, new MCvScalar(255,255,255));
            //image = image.WarpPerspective<float>(matrix, maxWidth, maxHeight, Emgu.CV.CvEnum.Inter.Nearest, Emgu.CV.CvEnum.Warp.Default, Emgu.CV.CvEnum.BorderType.Default, new Bgr(255, 255, 255));
            //CvInvoke.Rotate(image, image, Emgu.CV.CvEnum.RotateFlags.Rotate90CounterClockwise);
            //destImage = mat.ToImage<Bgr, float>();
            return image;
        }
        private void Button3_Click(object sender, EventArgs e)
        {
            imgGray = img.Convert<Gray, Byte>();
            //Converted to blurred
            CvInvoke.GaussianBlur(imgGray, imgGray, new Size(5, 5), 0);
            // using adaptive threshhold
            CvInvoke.AdaptiveThreshold(imgGray, imgGray, 255, Emgu.CV.CvEnum.AdaptiveThresholdType.GaussianC, Emgu.CV.CvEnum.ThresholdType.BinaryInv, 75, 10);
            CvInvoke.Canny(imgGray, cannyImage, 75, 200);
            cannyImage.ConvertTo(imgGray, Emgu.CV.CvEnum.DepthType.Default, -1, 0);
            Emgu.CV.Util.VectorOfVectorOfPoint vector = new Emgu.CV.Util.VectorOfVectorOfPoint();
            CvInvoke.FindContours(cannyImage, vector, null, Emgu.CV.CvEnum.RetrType.External, Emgu.CV.CvEnum.ChainApproxMethod.ChainApproxSimple);
            CvInvoke.DrawContours(img, vector, -1, new MCvScalar(240, 0, 159),3);
            MessageBox.Show("Question Part Detected");
            sheetDetectImage.Image = img;
            
        }

        private void Button4_Click(object sender, EventArgs e)
        {
            Emgu.CV.Util.VectorOfPoint approx = new Emgu.CV.Util.VectorOfPoint();
            Emgu.CV.Util.VectorOfVectorOfPoint vecOut = new Emgu.CV.Util.VectorOfVectorOfPoint();
            CvInvoke.FindContours(cannyImage, vecOut, null, Emgu.CV.CvEnum.RetrType.External, Emgu.CV.CvEnum.ChainApproxMethod.ChainApproxSimple);
            //Point defines the x-y coordinates in 2d-plane
            
            Dictionary<int, double> dict = new Dictionary<int, double>();
            AForge.IntPoint[] point = new AForge.IntPoint[4];
            if (vecOut.Size > 0)
            {
                for (int i = 0; i < vecOut.Size; i++)
                {
                    //calculating area of contours
                    double area = CvInvoke.ContourArea(vecOut[i]);
                    dict.Add(i, area); //adding areas in dictionary
                }
                var item = dict.OrderByDescending(v => v.Value);
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
                            CvInvoke.DrawContours(copyImage, vecOut, key, new MCvScalar(255, 0, 0), 5);
                            for (int i = 0; i < approx.Size; i++)
                            {

                                point[i].X=approx[i].X;
                                point[i].Y = approx[i].Y;
                                
                            }
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show(ex.Message);
                        }
                        break;
                    }
                }
            }
            sheetDetectImage.Image = copyImage;
            
            try
            {
                List<AForge.IntPoint> pointList = new List<AForge.IntPoint>();
                for(int i=0;i<4;i++)
                {
                    pointList.Add(new AForge.IntPoint(point[i].X, point[i].Y));
                }
                AForge.Imaging.Filters.SimpleQuadrilateralTransformation simple = new AForge.Imaging.Filters.SimpleQuadrilateralTransformation(pointList,200,200);
                Bitmap abc1 = simple.Apply(bitmapImage);
                AForge.Imaging.Filters.Mirror m = new AForge.Imaging.Filters.Mirror(false, true);
                m.ApplyInPlace(abc1);
                Image<Bgr, byte> newIm = new Image<Bgr, byte>(abc1);
                CvInvoke.Rotate(newIm, newIm, Emgu.CV.CvEnum.RotateFlags.Rotate90CounterClockwise);
                MessageBox.Show("Scanned Document");
                sheetDetectImage.Image = newIm;
                transformedImage = newIm.Copy();
            }
            catch (Exception er)
            {
                MessageBox.Show(er.StackTrace);
            }
        }

        private void BubbleDetectBtn_Click(object sender, EventArgs e)
        {

            //Applying Operations on transformed Image
            transformedImage = transformedImage.Resize(400, 400, Emgu.CV.CvEnum.Inter.Linear);
            Image<Bgr, byte> transCopy = transformedImage.Copy();
            Emgu.CV.Util.VectorOfVectorOfPoint qtnVect = new Emgu.CV.Util.VectorOfVectorOfPoint();
            Image<Gray, byte> qtnGray = transCopy.Convert<Gray, byte>();
            Image<Gray, byte> copyG = qtnGray.Copy();
            CvInvoke.GaussianBlur(qtnGray, qtnGray, new Size(5, 5), 0);
            CvInvoke.AdaptiveThreshold(qtnGray, qtnGray, 255, Emgu.CV.CvEnum.AdaptiveThresholdType.GaussianC, Emgu.CV.CvEnum.ThresholdType.Binary, 55, 9);
            CvInvoke.BitwiseNot(qtnGray, qtnGray);
            CvInvoke.FindContours(qtnGray, qtnVect, null, Emgu.CV.CvEnum.RetrType.External, Emgu.CV.CvEnum.ChainApproxMethod.ChainApproxSimple, default);


            //CIRCLE METHOD
            List<CircleF> circList = new List<CircleF>();
            Emgu.CV.Util.VectorOfVectorOfPoint test = new Emgu.CV.Util.VectorOfVectorOfPoint();
            Emgu.CV.Util.VectorOfPoint qtnApprox = new Emgu.CV.Util.VectorOfPoint();
            Dictionary<int, double> qtnDict = new Dictionary<int, double>();
            if (qtnVect.Size > 0)
            {
                for (int i = 0; i < qtnVect.Size; i++)
                {
                    double area = CvInvoke.ContourArea(qtnVect[i]);
                    if (area > 70)
                    {
                        qtnDict.Add(i, area);
                    }
                }
                var item = qtnDict.OrderByDescending(v => v.Value);  //.Take(1);

                Emgu.CV.Util.VectorOfPoint approxList = new Emgu.CV.Util.VectorOfPoint();

                foreach (var it in item)
                {
                    int key = Convert.ToInt32(it.Key.ToString());
                    double peri = CvInvoke.ArcLength(qtnVect[key], true);
                    CvInvoke.ApproxPolyDP(qtnVect[key], qtnApprox, 0.02 * peri, true);

                    if (qtnApprox.Size == 0)
                    {

                    }
                    else if (qtnApprox.Size > 6)
                    {
                        CircleF circle = CvInvoke.MinEnclosingCircle(qtnVect[key]);
                        Point centre = new Point();
                        centre.X = (int)circle.Center.X;
                        centre.Y = (int)circle.Center.Y;
                        CvInvoke.Circle(transformedImage, centre, (int)circle.Radius, new MCvScalar(0, 255, 0), 2, Emgu.CV.CvEnum.LineType.Filled, 0);
                        //break;
                    }
                }
                MessageBox.Show("Bubbles Detected");
                bubbleImage.Image = transformedImage;
            }
        }
    }
}
