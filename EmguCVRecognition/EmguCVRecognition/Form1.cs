using System;
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
            im1.SizeMode = PictureBoxSizeMode.Zoom;
            im2.SizeMode = PictureBoxSizeMode.Zoom;
            listBox1.MultiColumn = true;
            font = new MCvFont(Emgu.CV.CvEnum.FONT.CV_FONT_HERSHEY_PLAIN,1, 1);
        }

        void myForm_MouseClick(object sender, MouseEventArgs e)
        {
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //if (imagesloaded) {
            //    MessageBox.Show("Images can only be loaded once!");
            //    return; }

            //Initialize openFiledialogue
            OpenFileDialog openFileDialog1 = new OpenFileDialog();
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
            int imWidth, imHeight, boxWidth, boxHeight;
            imWidth = im1.Image.Size.Width;
            imHeight = im1.Image.Size.Height;
            boxWidth = im1.Size.Width;
            boxHeight = im1.Size.Height;

            Point mouse = TranslateZoomMousePosition(mouseEventArgs.X, mouseEventArgs.Y, imWidth, imHeight, boxWidth, boxHeight);
            int x = mouse.X;//(int)(mouseEventArgs.X / im1.ZoomScale);//((float) im1.Width / (float) im1.Image.Size.Width));
            int y = mouse.Y;//(int)(mouseEventArgs.Y / im1.ZoomScale);//((float) im1.Height / (float) im1.Image.Size.Height));
            if (mouseEventArgs != null) label1.Text = "X= " + x + " Y= " + y;
            Emgu.CV.Image<Hsv, byte> inImg = LoadedImages[listBox1.SelectedItem.ToString()];
            Hsv pcolor = inImg[y, x];

            Emgu.CV.Image<Gray, byte> outImg;
            outImg = thresholdHSVtoGray(inImg, pcolor, 10, 20, 20);
            imageBox2.Image = outImg;
            imageBox2.Update();
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

            List<LineSegment2D> linesegments = new List<LineSegment2D>();
            List<Hsv> linecolors = new List<Hsv>();

            shapes.AddRange(findShapesinGrayImg(inImg, refImg, ref linesegments,ref linecolors));

            for (int i = 0; i < linesegments.Count; i++)
            {
                outImg.Draw(linesegments.ElementAt(i), linecolors.ElementAt(i), 2);
            }
           
            

            string data = "";
            foreach (ShapeColorObject s in shapes)
            {
                data += s.toString() + "\n";
                outImg.Draw(s.toString(), ref font, s.pos, new Hsv(0, 255, 255));
                outImg.Draw(new Cross2DF(new PointF((float)s.pos.X, (float)s.pos.Y), (float)5.0,(float) 5.0), new Hsv(0, 200, 200), 2);
            }
            label2.Text = data;
            im2.Image = outImg;
            im2.Update();
        }



        /// <summary>
        /// Returns a List of Shapes found in the gray Image and returns Linesegments and color of shape
        /// </summary>
        /// <param name="inImg">Gray Image</param>
        /// <param name="refImg">Color Image for shapecolor</param>
        /// <param name="lines">outputarray for lines</param>
        /// <param name="colors">outputarry for linecolors</param>
        /// <returns></returns>
        public static  List<ShapeColorObject> findShapesinGrayImg(Emgu.CV.Image<Gray, Byte> inImg, Emgu.CV.Image<Hsv, Byte> refImg, ref List<LineSegment2D> lines, ref List<Hsv> colors) {

            if (inImg == null || refImg == null)  return null; 

            List<ShapeColorObject> funkshapes = new List<ShapeColorObject>();

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
                        funkshapes.Add(new ShapeColorObject(current.Area, ShapeColorObject.shape.triangle, refImg[meanY, meanX], meanX, meanY));
                    }
                    else if (current.Total == 4 && current.Convex)
                    {
                        col = new Hsv(45, 255, 220);
                        funkshapes.Add(new ShapeColorObject(current.Area, ShapeColorObject.shape.rectangle, refImg[meanY, meanX], meanX, meanY));
                    }
                    else if (current.Total > 8 && current.Convex)
                    {
                        col = new Hsv(90, 255, 220);
                        funkshapes.Add(new ShapeColorObject(current.Area, ShapeColorObject.shape.circle, refImg[meanY, meanX], meanX, meanY));
                    }
                    else
                    {
                        col = new Hsv(135, 255, 220);
                        funkshapes.Add(new ShapeColorObject(current.Area, ShapeColorObject.shape.undefined, refImg[meanY, meanX], meanX, meanY));
                    }

                    if (lines != null && colors != null)
                    {
                        for (int i = 0; i < current.Total - 1; i++)
                        {
                            lines.Add(new LineSegment2D(points[i], points[i + 1]));
                            colors.Add(col);

                        }
                        lines.Add(new LineSegment2D(points[current.Total - 1], points[0]));
                        colors.Add(col);
                    }
                }//ende if(200>area)
            }//ende for(contours....)

            return funkshapes;
        }

        /// <summary>
        /// Returns a List of Shapes found in the Gray Image
        /// </summary>
        /// <param name="inImg">Gray Iamge</param>
        /// <param name="refImg">color Image for shapecolor</param>
        /// <returns></returns>
        public static List<ShapeColorObject> findShapesinGrayImg(Emgu.CV.Image<Gray, Byte> inImg, Emgu.CV.Image<Hsv, Byte> refImg)
        {

            if (inImg == null || refImg == null) return null;

            List<ShapeColorObject> funkshapes = new List<ShapeColorObject>();

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
                        funkshapes.Add(new ShapeColorObject(current.Area, ShapeColorObject.shape.triangle, refImg[meanY, meanX], meanX, meanY));
                    }
                    else if (current.Total == 4 && current.Convex)
                    {
                        col = new Hsv(45, 255, 220);
                        funkshapes.Add(new ShapeColorObject(current.Area, ShapeColorObject.shape.rectangle, refImg[meanY, meanX], meanX, meanY));
                    }
                    else if (current.Total > 8 && current.Convex)
                    {
                        col = new Hsv(90, 255, 220);
                        funkshapes.Add(new ShapeColorObject(current.Area, ShapeColorObject.shape.circle, refImg[meanY, meanX], meanX, meanY));
                    }
                    else
                    {
                        col = new Hsv(135, 255, 220);
                        funkshapes.Add(new ShapeColorObject(current.Area, ShapeColorObject.shape.undefined, refImg[meanY, meanX], meanX, meanY));
                    }

                    
                }//ende if(200>area)
            }//ende for(contours....)

            return funkshapes;
        }

        /// <summary>
        /// Nimmt HSV Bild, vergleicht mit eingegebener Farbe und gibt binaeres Graubild zurueck
        /// </summary>
        /// <param name="inImg">Eingabe HSV-bild</param>
        /// <param name="pcolor">Vergleichsfarbe</param>
        /// <param name="dHue">Farbtoleranz</param>
        /// <param name="dSaturation">Saettigungstoleranz</param>
        /// <param name="dValue">Helligeitstoleranz</param>
        /// <returns></returns>
        private Emgu.CV.Image<Gray, byte> thresholdHSVtoGray(Emgu.CV.Image<Hsv, byte> inImg, Hsv pcolor, int dHue, int dSaturation, int dValue)
        {
            double lowH = Math.Max(pcolor.Hue - dHue, 0);
            double lowS = Math.Max(pcolor.Satuation - dSaturation, 0);
            double lowV = Math.Max(pcolor.Value - dValue, 0);
            double highH = Math.Min(pcolor.Hue + dHue, 179);
            double highS = Math.Min(pcolor.Satuation + dSaturation, 255);
            double highV = Math.Min(pcolor.Value + dValue, 255);
            return inImg.InRange(new Hsv(lowH, lowS, lowV), new Hsv(highH, highS, highV));
        }


        /// <summary>
        /// Rechnet die Eingabekoordinaten in Bild/Pixelkoordinaten um
        /// Koordinaten werden auf Bildkoordinaten geclamped
        /// </summary>
        /// <param name="x">Mouseposition.x in imageBox</param>
        /// <param name="y">Mouseposition.y in imageBox</param>
        /// <param name="imWidth">Bildbreite</param>
        /// <param name="imHeight">Bildhoehe</param>
        /// <param name="boxWidth">imageboxbreite</param>
        /// <param name="boxHeight">imageboxhoehe</param>
        /// <returns></returns>
        protected Point TranslateZoomMousePosition(int x, int y, int imWidth, int imHeight, int boxWidth,int boxHeight)
        {
            
            float imageAspect = (float)imWidth / imHeight;
            float controlAspect = boxWidth / boxHeight;
            float newX = x;
            float newY = y;
            if (imageAspect > controlAspect)
            {
                // This means that we are limited by width, 
                // meaning the image fills up the entire control from left to right
                float ratioWidth = (float)imWidth / boxWidth;
                newX *= ratioWidth;
                float scale = (float)boxWidth / imWidth;
                float displayHeight = scale * imHeight;
                float diffHeight = boxHeight - displayHeight;
                diffHeight /= 2;
                newY -= diffHeight;
                newY /= scale;
            }
            else
            {
                // This means that we are limited by height, 
                // meaning the image fills up the entire control from top to bottom
                float ratioHeight = (float)imHeight / boxHeight;
                newY *= ratioHeight;
                float scale = (float)boxHeight / imHeight;
                float displayWidth = scale * imWidth;
                float diffWidth = boxWidth - displayWidth;
                diffWidth /= 2;
                newX -= diffWidth;
                newX /= scale;
            }

            newX = Math.Min(Math.Max(0, newX), imWidth-1);
            newY = Math.Min(Math.Max(0, newY), imHeight-1);

            return new Point((int)newX, (int)newY);
        }
    }
}
