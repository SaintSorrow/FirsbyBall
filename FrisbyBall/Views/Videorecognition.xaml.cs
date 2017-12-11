using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;

using Emgu.CV;
using Emgu.CV.CvEnum;
using Emgu.CV.Structure;
using Emgu.CV.UI;

namespace FrisbyBall.Views
{
    public partial class MyPage : ContentPage
    {

        Capture capWebcam = null;
        bool blnCapturingInProcess = false;
        Image<Bgr, Byte> imgOriginal;
        Image<Gray, Byte> imgProcessed;

        public MyPage()
        {
            InitializeComponent();
        }


            ///////////////////////////////////////////////////////////////////////////////////////////////
            private void Form1_Load(object sender, EventArgs e)
            {
                try
                {
                    capWebcam = new Capture();                              // associate capture object to the default webcam
                }
                catch (NullReferenceException except)
                {       // catch error if unsuccessful
                    txtXYRadius.Text = except.Message;              // show error object message in text box
                    return;
                }
                // once we have a good capture object . . .
                Application.Idle += processFrameAndUpdateGUI;           // add process image function to the application's list of tasks
                blnCapturingInProcess = true;                                           // update member flag variable
            }

            ///////////////////////////////////////////////////////////////////////////////////////////////
            private void Form1_FormClosed(object sender, FormClosedEventArgs e)
            {
                if (capWebcam != null)
                {
                    capWebcam.Dispose();
                }
            }

            ///////////////////////////////////////////////////////////////////////////////////////////////
            void processFrameAndUpdateGUI(object sender, EventArgs arg)
            {
                imgOriginal = capWebcam.QueryFrame();                       // get the next frame from the webcam
                if (imgOriginal == null) return;                                    // if we did not get a frame, bail

                imgProcessed = imgOriginal.InRange(new Bgr(0, 0, 175),              // min filter value (if color is greater than or equal to this)
                                                                                     new Bgr(100, 100, 256));           // max filter value (if color is less than or equal to this)

                imgProcessed = imgProcessed.SmoothGaussian(9);      // we call SmoothGaussian with only one param, that being the x and y size of the filter window

                CircleF[] circles = imgProcessed.HoughCircles(new Gray(100),                        // Canny threshold
                                                                                                            new Gray(50),                           // accumulator threshold
                                                                                                            2,                                              // size of image / this param = "accumulator resolution"
                                                                                                            imgProcessed.Height / 4,    // min distance in pixels between the centers of the detected circles
                                                                                                            10,                                             // min radius of detected circles
                                                                                                            400)[0];                                    // max radius of detected circle, get circles from the first channel
                foreach (CircleF circle in circles)
                {
                    if (txtXYRadius.Text != "") txtXYRadius.AppendText(Environment.NewLine);            // if we are not on the first line in the test box, insert a new line char

                    txtXYRadius.AppendText("ball position = x" + circle.Center.X.ToString().PadLeft(4) +                        // x position of center point of circle
                                                                                         ", y =" + circle.Center.Y.ToString().PadLeft(4) +                      // y position of center point of circle
                                                                                ", radius =" + circle.Radius.ToString("###.000").PadLeft(7));           // radius of circle
                    txtXYRadius.ScrollToCaret();                                                                                                                                        // move the text box scroll bar down to the line we just wrote

                    // draw a samll green circle at the center of the detected object
                    // to do this, we will call the OpenCV 1.x function, this is necessary b/c
                    // we are drawing a circle of radius 3, even though the size of the detected circle will be much bigger
                    // the CvInvoke object can be used to make OpenCV 1.x function calls
                    CvInvoke.cvCircle(imgOriginal,                                                                                              // draw on the original image
                                                        new Point((int)circle.Center.X, (int)circle.Center.Y),          // center point of circle
                                                        3,                                                                                                                  // radius of circle in pixels
                                                        new MCvScalar(0, 255, 0),                                                                           // draw pure green
                                                        -1,                                                                                                 // thickness of circle in pixels, -1 indicates to fill the circle
                                                        LINE_TYPE.CV_AA,                                                                                        // use AA to smooth the pixels
                                                        0);                                                                                                                 // no shift

                    // draw a red circle around the detected object
                    imgOriginal.Draw(circle,                                    // current circle we are on in foreach loop
                                                     new Bgr(Color.Red),            // draw pure red
                                                     3);                                            // thickness of circle in pixels
                }       // end foreach

                ibOriginal.Image = imgOriginal;
                ibProcessed.Image = imgProcessed;

            }

            ///////////////////////////////////////////////////////////////////////////////////////////////
            private void btnPauseOrResume_Click(object sender, EventArgs e)
            {
                if (blnCapturingInProcess == true)
                {                                                       // if we are currently processing an image, user just chose pause, so . . .
                    Application.Idle -= processFrameAndUpdateGUI;                               // remove the process image function from the application's list of tasks
                    blnCapturingInProcess = false;                                                          // update flag variable
                    btnPauseOrResume.Text = "resume";                                                       // update button text
                }
                else
                {                                                                                                           // else if we are not currently processing an image, user just chose resume, so . . .
                    Application.Idle += processFrameAndUpdateGUI;                               // add the process image function to the application's list of tasks
                    blnCapturingInProcess = true;                                                               // upadate flag variable
                    btnPauseOrResume.Text = "pause";                                                        // now button will offer pause option
                }
            }

        }       // end class
    }       // end namespace

    }
}
