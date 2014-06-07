﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using Emgu.CV;
using Emgu.CV.UI;
using Emgu.CV.CvEnum;
using Emgu.CV.Structure;


namespace EmguCVRecognition
{
    public partial class Form1 : Form
    {
        // Variables
        bool imagesloaded = false;
        public Dictionary<string, Emgu.CV.Image<Hsv,Byte>> LoadedImages = new Dictionary<string,Emgu.CV.Image<Hsv,Byte>>();
        public List<Emgu.CV.Image<Hsv, byte>> imgs = new List<Emgu.CV.Image<Hsv, byte>>();

        public Emgu.CV.UI.ImageBox  im1;
        public Emgu.CV.UI.ImageBox im2;
        public MCvFont font;

        List<ShapeColorObject> shapes;

        //----------------------------------------------------



        public Form1()
        {
            InitializeComponent();

            this.MouseClick += new MouseEventHandler(myForm_MouseClick);
            im1 = imageBox1;
            im2 = imageBox2;
            shapes = new List<ShapeColorObject>();
            //im1.SizeMode = PictureBoxSizeMode.StretchImage;
            //im2.SizeMode = PictureBoxSizeMode.StretchImage;
            listBox1.MultiColumn = true;
            font = new MCvFont(Emgu.CV.CvEnum.FONT.CV_FONT_HERSHEY_COMPLEX, 0.6, 0.6);
        }

        void myForm_MouseClick(object sender, MouseEventArgs e)
        {
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (imagesloaded) {
                MessageBox.Show("Images can only be loaded once!");
                return; }

            //Initialize openFiledialogue
            OpenFileDialog openFileDialog1 = new OpenFileDialog();
           // openFileDialog1.InitialDirectory = "c:\\";
            openFileDialog1.Filter = "Images (*.BMP;*.JPG;*.GIF;*.PNG)|*.BMP;*.JPG;*.GIF;*.PNG";
            openFileDialog1.FilterIndex = 1;
            openFileDialog1.RestoreDirectory = true;
            openFileDialog1.Title = "Image Selection";
            openFileDialog1.Multiselect = true;
            //----------------------------

            DialogResult dialogresult = openFileDialog1.ShowDialog();

            //Save Images in Dictionary and display in ListBox
            if (dialogresult == System.Windows.Forms.DialogResult.OK)
            {
                int currentImage = 0;
                // Read the files
                listBox1.BeginUpdate();
                foreach (String file in openFileDialog1.FileNames)
                {
                    // Create a PictureBox.
                    try
                    {
                        //Convert Images to Emgu Format
                        Image tempImage = Image.FromFile(file);
                        Bitmap tempBitmap = new Bitmap(tempImage);
                        Emgu.CV.Image<Hsv,Byte> loadedImage = new Emgu.CV.Image<Hsv, Byte>(tempBitmap);
                        string loadedImageName = "Image:"+currentImage;
                        //Save Images
                        listBox1.Items.Add(loadedImageName);
                        LoadedImages.Add(loadedImageName, loadedImage);
                        listBox1.SelectedIndex = 0;
                    }

                    catch (Exception ex)
                    {
                        // Could not load the image - probably related to Windows file system permissions.
                        MessageBox.Show("Cannot load the image: " + file.Substring(file.LastIndexOf('\\'))
                            + ".\n\n" + ex.Message);
                    }
                    currentImage++;
                }
                listBox1.EndUpdate();

            }
            //----------------------------------------
            imagesloaded = true;
        }


            
                

       private void button2_Click(object sender, EventArgs e)
       {

           Capture capture = new Capture(); //create a camera capture
  
           Emgu.CV.Image<Bgr, Byte> img = capture.QueryFrame();
           this.im2.Image = img; //draw the image obtained from camera
               
       }

   
        

        private void imageBox1_Click(object sender, EventArgs e)
        {
            var mouseEventArgs = e as MouseEventArgs;
            int x = (int)(mouseEventArgs.X / im1.ZoomScale);//((float) im1.Width / (float) im1.Image.Size.Width));
            int y = (int)(mouseEventArgs.Y / im1.ZoomScale);//((float) im1.Height / (float) im1.Image.Size.Height));
            if (mouseEventArgs != null) label1.Text = "X= " + x + " Y= " + y;
            Emgu.CV.Image<Hsv, byte> inImg = LoadedImages[listBox1.SelectedItem.ToString()];
            Hsv pcolor = inImg[y, x];

            Emgu.CV.Image<Gray, byte> outImg;
            double lowH = Math.Max(pcolor.Hue - 10, 0);
            double lowS = Math.Max(pcolor.Satuation - 20, 0);
            double lowV = Math.Max(pcolor.Value - 20, 0);
            double highH = Math.Min(pcolor.Hue + 10, 179);
            double highS = Math.Min(pcolor.Satuation + 20, 255);
            double highV = Math.Min(pcolor.Value + 20, 255);

            outImg = inImg.InRange(new Hsv(lowH, lowS, lowV), new Hsv(highH, highS, highV));
            imageBox2.Image = outImg;
            imageBox2.Update();

            /*for (int i = -1; i < 2; i++)
            {
                for (int j = -1; j < 2; j++)
                    inImg[y + i, x + j] = new Hsv(0,0,255);
            }
            im1.Image = inImg;
            im1.Update();*/
        }

        private void imageBox2_Click(object sender, EventArgs e)
        {

        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            im1.Image = LoadedImages[listBox1.SelectedItem.ToString()];
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (im2.Image == null) return;
            shapes.Clear();
            Emgu.CV.Image<Hsv, byte> refImg = (Emgu.CV.Image<Hsv, byte>)im1.Image;
            Emgu.CV.Image<Gray, byte> inImg = (Emgu.CV.Image<Gray, byte>)im2.Image;
            Emgu.CV.Image<Hsv, byte> outImg = inImg.Convert<Hsv, byte>();

            for (Contour<Point> contours = inImg.FindContours(); contours != null; contours = contours.HNext)
            {
                Contour<Point> current = contours.ApproxPoly(contours.Perimeter * 0.008);
                if (current.Area > 200)
                {
                    Point[] points = current.ToArray();

                    int meanX = 0;
                    int meanY = 0;

                    foreach (Point p in points)
                    {
                        meanX += p.X;
                        meanY += p.Y;
                    }

                    meanX /= current.Total;
                    meanY /= current.Total;

                    Hsv col;
                    if (current.Total == 3)
                    {
                        col = new Hsv(0, 255, 220);
                        shapes.Add(new ShapeColorObject(current.Area, ShapeColorObject.shape.triangle, refImg[meanY, meanX], meanX, meanY));
                    }
                    else if (current.Total == 4 && current.Convex)
                    {
                        col = new Hsv(45, 255, 220);
                        shapes.Add(new ShapeColorObject(current.Area, ShapeColorObject.shape.rectangle, refImg[meanY, meanX], meanX, meanY));
                    }
                    else if (current.Total > 8 && current.Convex)
                    {
                        col = new Hsv(90, 255, 220);
                        shapes.Add(new ShapeColorObject(current.Area, ShapeColorObject.shape.circle, refImg[meanY, meanX], meanX, meanY));
                    }
                    else
                    {
                        col = new Hsv(135, 255, 220);
                        shapes.Add(new ShapeColorObject(current.Area, ShapeColorObject.shape.undefined, refImg[meanY, meanX], meanX, meanY));
                    }

                    for (int i = -1; i < 2; i++)
                        for (int j = -1; j < 2; j++)
                        {
                            int x = Math.Max(Math.Min(meanX + i, outImg.Width - 1), 0);
                            int y = Math.Max(Math.Min(meanY + j, outImg.Height - 1), 0);
                            outImg[y, x] = new Hsv(0, 255, 255);
                        }

                    for (int i = 0; i < current.Total - 1; i++)
                    {
                        LineSegment2D line = new LineSegment2D(points[i], points[i + 1]);
                        outImg.Draw(line, col, 2);
                    }
                    LineSegment2D line2 = new LineSegment2D(points[current.Total - 1], points[0]);
                    outImg.Draw(line2, col, 2);

                    string data = "";
                    foreach (ShapeColorObject s in shapes)
                    {
                        data += s.toString() + "\n";
                        outImg.Draw(s.toString(), ref font, s.pos, new Hsv(0, 255, 255));
                    }
                    label2.Text = data;


                    im2.Image = outImg;
                    im2.Update();
                }
            }
        }

    }
}
