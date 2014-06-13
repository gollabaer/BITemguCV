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
        //-Variables------------------------------------------
        bool imagesloaded = false;
        public Dictionary<string, Emgu.CV.Image<Hsv,Byte>> LoadedImages = new Dictionary<string,Emgu.CV.Image<Hsv,Byte>>();
        public Dictionary<string, Emgu.CV.Image<Bgr, Byte>> cleanLoadedImages = new Dictionary<string, Emgu.CV.Image<Bgr, Byte>>();
        public Dictionary<string, Emgu.CV.Image<Hsv, Byte>> workImages = new Dictionary<string, Image<Hsv, byte>>();
       
        public List<Emgu.CV.Image<Hsv, byte>> imgs = new List<Emgu.CV.Image<Hsv, byte>>();

        public Emgu.CV.UI.ImageBox  im1;
        public Emgu.CV.UI.ImageBox im2;
        public MCvFont font;

        List<ShapeColorObject> shapes;
        List<ShapeColorObject> trackedshapes;
        List<ShapeColorObject> chosenshapes;

        //----------------------------------------------------
        //Parameter
        int mediansize = 1;
        int gaussiansize = 1;
        int dHue = 10;
        int dSaturation = 20;
        int dValue = 20;

        //------------------------------------------------

        public Form1()
        {
            InitializeComponent();

            this.MouseClick += new MouseEventHandler(myForm_MouseClick);
            im1 = imageBox1;
            im2 = imageBox2;
            shapes = new List<ShapeColorObject>();
            trackedshapes = new List<ShapeColorObject>();
            chosenshapes = new List<ShapeColorObject>();
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

        private void reset()
        {
            chosenshapes.Clear();
            trackedshapes.Clear();
            label2.Text = "no shapes chosen";
            label3.Text = "no shapes tracked";
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Dictionary<string, Image<Bgr, byte>> bgrImages = new Dictionary<string, Image<Bgr, byte>>();

            //--Initialize openFiledialogue---
            OpenFileDialog openFileDialog1 = new OpenFileDialog();
            openFileDialog1.Filter = "Images (*.BMP;*.JPG;*.GIF;*.PNG)|*.BMP;*.JPG;*.GIF;*.PNG";
            openFileDialog1.FilterIndex = 1;
            openFileDialog1.RestoreDirectory = true;
            openFileDialog1.Title = "Image Selection";
            openFileDialog1.Multiselect = true;
            //--------------------------------

            DialogResult dialogresult = openFileDialog1.ShowDialog();

            //---Save Images in Dictionary and display in ListBox----
            if (dialogresult == System.Windows.Forms.DialogResult.OK)
            {
                if (imagesloaded)
                {
                    //reset and clear everything if images were loaded before
                    reset();
                    cleanLoadedImages.Clear();
                    LoadedImages.Clear();
                    workImages.Clear();
                    listBox1.Items.Clear();
                    //listBox1.Update();
                }

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
                        Emgu.CV.Image<Bgr, byte> cleanImage = new Emgu.CV.Image<Bgr, byte>(tempBitmap);
                        Emgu.CV.Image<Hsv,Byte> loadedImage = new Emgu.CV.Image<Hsv, byte>(tempBitmap);
                        string loadedImageName = "Image:"+currentImage;
                        //Save Images
                        listBox1.Items.Add(loadedImageName);
                        cleanLoadedImages.Add(loadedImageName, cleanImage);
                        LoadedImages.Add(loadedImageName, loadedImage.SmoothMedian(mediansize) );
                        if(checkBox1.Checked)
                            bgrImages.Add(loadedImageName, cleanImage.SmoothGaussian(gaussiansize));
                       
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
                imagesloaded = true;

                if (checkBox1.Checked) {
                    LoadedImages = BackgroundSubtractor.getWithoutBackground(bgrImages);
                }
                listBox1.SelectedIndex = 0;
            }
            //----------------------------------------
            
            
        }

        private void imageBox1_Click(object sender, EventArgs e)
        {
            //--Get Mouseposition in Pixelcoordinates--------
            var mouseEventArgs = e as MouseEventArgs;
            int imWidth, imHeight, boxWidth, boxHeight;
            imWidth = im1.Image.Size.Width;
            imHeight = im1.Image.Size.Height;
            boxWidth = im1.Size.Width;
            boxHeight = im1.Size.Height;
            Point mouse = TranslateZoomMousePosition(mouseEventArgs.X, mouseEventArgs.Y, imWidth, imHeight, boxWidth, boxHeight);
            int x = mouse.X;//(int)(mouseEventArgs.X / im1.ZoomScale);
            int y = mouse.Y;//(int)(mouseEventArgs.Y / im1.ZoomScale);

            
            Emgu.CV.Image<Hsv, byte> original = LoadedImages[listBox1.SelectedItem.ToString()];
            Hsv pcolor = original[y, x];
            if (mouseEventArgs != null) label1.Text = "@(" + x + "; " + y+ "): " + pcolor.ToString() ;
            Emgu.CV.Image<Gray, byte> threshedimage;
            threshedimage = thresholdHSVtoGray(original, pcolor);
            imageBox2.Image = threshedimage;

            //
            ///-----CODE VON BUTTON3CLICK
            //

            shapes.Clear();
            Emgu.CV.Image<Hsv, byte> refImg = (Emgu.CV.Image<Hsv, byte>)im1.Image;
            Emgu.CV.Image<Gray, byte> inImg = (Emgu.CV.Image<Gray, byte>)im2.Image;
            Emgu.CV.Image<Hsv, byte> outImg = inImg.Convert<Hsv, byte>();

            shapes.AddRange(findShapesinGrayImg(inImg, refImg, listBox1.SelectedIndex));

            //--Find closest shape---------
            ShapeColorObject temp = null;
            int dist = 0;
            foreach (ShapeColorObject shp in shapes)
            {
                int d = (shp.pos.X - mouse.X) * (shp.pos.X - mouse.X) + (shp.pos.Y - mouse.Y) * (shp.pos.Y - mouse.Y);
                if (ShapeColorObject.compareHues(shp.getColor().Hue, pcolor.Hue, dHue))
                if ( temp == null || d < dist)
                {
                    temp = shp;
                    temp.imIndex = listBox1.SelectedIndex;
                    dist = d;
                }
            }
            if (temp != null && !chosenshapes.Contains(temp))
            {
                temp.objIndex = chosenshapes.Count;
                chosenshapes.Add(temp);
                //trackedshapes.Add(temp);                
            }

            foreach (ShapeColorObject shp in chosenshapes)
            {
                shp.drawOnImg(ref outImg);
                im2.Image = outImg;
                im2.Update();
            }

            label2.Text = "";
            foreach (ShapeColorObject shape in chosenshapes)
                label2.Text += shape.toString() + "\n";
            //
            ///-CODE VON BUTTON3--ENDE----
            //
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            updateSelectedImage();
        }

        private void updateSelectedImage()
        {
            im1.Image = LoadedImages[listBox1.SelectedItem.ToString()];
            if (workImages.ContainsKey(listBox1.SelectedItem.ToString()))
                im2.Image = workImages[listBox1.SelectedItem.ToString()];
        }

        private void button3_Click(object sender, EventArgs e)
        {
            searchOverImages();
            label3.Text = "";
            foreach (ShapeColorObject shape in trackedshapes)
                label3.Text += shape.toString() + "\n";
        }



        /// <summary>
        /// Returns a List of Shapes found in the gray Image and returns Linesegments and color of shape
        /// </summary>
        /// <param name="inImg">Gray Image</param>
        /// <param name="refImg">Color Image for shapecolor</param>
        /// <param name="lines">outputarray for lines</param>
        /// <param name="colors">outputarry for linecolors</param>
        /// <returns></returns>
        public List<ShapeColorObject> findShapesinGrayImg(Emgu.CV.Image<Gray, Byte> inImg, Emgu.CV.Image<Hsv, Byte> refImg, int index) {

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
                    ShapeColorObject newObj; 
                    Hsv col;
                    if (current.Total == 3) //3 vertices
                    {
                        
                        col = new Hsv(0, 255, 220);
                        newObj = new ShapeColorObject(current.Area, ShapeColorObject.shape.tri, refImg[meanY, meanX], meanX, meanY);
                        newObj.imIndex = index;
                        funkshapes.Add(newObj);
                    }
                    else if (current.Total == 4 && current.Convex) //4 vertices convex
                    {
                        col = new Hsv(45, 255, 220);
                        newObj = new ShapeColorObject(current.Area, ShapeColorObject.shape.rect, refImg[meanY, meanX], meanX, meanY);
                        newObj.imIndex = index;
                        funkshapes.Add(newObj);
                    }
                    else if (current.Total > 8 && current.Convex) //8+ vertices (circle) convex
                    {
                        col = new Hsv(90, 255, 220);
                        newObj = new ShapeColorObject(current.Area, ShapeColorObject.shape.circle, refImg[meanY, meanX], meanX, meanY);
                        newObj.imIndex = index;
                        funkshapes.Add(newObj);
                    }
                    else //other shapes
                    {
                        col = new Hsv(135, 255, 220);
                        newObj = new ShapeColorObject(current.Area, ShapeColorObject.shape.undef, refImg[meanY, meanX], meanX, meanY);
                        newObj.imIndex = index;
                        funkshapes.Add(newObj);
                    }

                   for (int i = 0; i < current.Total - 1; i++)
                   {
                         newObj.lineSegments.Add(new LineSegment2D(points[i], points[i + 1])); //add contour line segments to new ShapeColorObject
                   }

                   newObj.lineSegments.Add(new LineSegment2D(points[current.Total - 1], points[0]));
                   
                    double cirumf = 0;
                    foreach(LineSegment2D s in newObj.lineSegments)
                        cirumf += s.Length;


                    newObj.circularity =(float) (cirumf * cirumf / current.Area);
                  
                }//ende if(200>area)
            }//ende von for(contours....)

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
        private Emgu.CV.Image<Gray, byte> thresholdHSVtoGray(Emgu.CV.Image<Hsv, byte> inImg, Hsv pcolor)
        {
            double lowH, lowS, lowV, highH, highS, highV;
            if (pcolor.Value < 15)
            {
                lowH = 0; highH = 179;
                lowS = 0; highS = 255;
            }
            else
            {
                lowH = pcolor.Hue - dHue;//Math.Max(pcolor.Hue - dHue, 0);
                lowS = Math.Max(pcolor.Satuation - dSaturation, 0);
                highH = pcolor.Hue + dHue; // Math.Min(pcolor.Hue + dHue, 179);
                highS = Math.Min(pcolor.Satuation + dSaturation, 255);
            }
            
            lowV = Math.Max(pcolor.Value - dValue, 0);
            highV = Math.Min(pcolor.Value + dValue, 255);

            Image<Gray, byte> gray = new Image<Gray, byte>(inImg.Size);
            if (highH >= 180)
                highH -= 180;
            if (lowH < 0) 
                lowH += 180;
            if (highH <= lowH)
            {
                Image<Gray, byte> gray1 = inImg.InRange(new Hsv(lowH, lowS, lowV), new Hsv(179, highS, highV));
                Image<Gray, byte> gray2 = inImg.InRange(new Hsv(0, lowS, lowV), new Hsv(highH, highS, highV));
                gray = gray1.Add(gray2);
            }
            else
                gray = inImg.InRange(new Hsv(lowH, lowS, lowV), new Hsv(highH, highS, highV));
            //Bildverbesserung
            gray.SmoothMedian(mediansize);

            return gray;
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
        private Point TranslateZoomMousePosition(int x, int y, int imWidth, int imHeight, int boxWidth,int boxHeight)
        {
            
            float imageAspect = (float)imWidth / imHeight;
            float controlAspect = boxWidth / boxHeight;
            float newX = x;
            float newY = y;
            if (imageAspect < controlAspect)
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

        /// <summary>
        /// Finds shapes similar to template in the given image. Similarity calculated by ShapeColorObject.compare method
        /// </summary>
        /// <param name="template"></param>
        /// <param name="image"></param>
        /// <returns></returns>
        private List<ShapeColorObject> findSimilarShapeinPicture(ShapeColorObject template, Image<Hsv, byte> image, int index) {
            
            Image<Gray, Byte> threshhImage = thresholdHSVtoGray(image, template.getColor());
            List<ShapeColorObject> samecoloredshapes = findShapesinGrayImg(threshhImage, image, index);
            List<ShapeColorObject> similar = new List<ShapeColorObject>();

            for (int i = 0; i < samecoloredshapes.Count ; i++)
            {
                //only adds found object if similar to template and not yet tracked
                if (template.compare(samecoloredshapes.ElementAt(i), 5, 30) && !trackedshapes.Contains(samecoloredshapes.ElementAt(i)))
                    similar.Add(samecoloredshapes.ElementAt(i));
            }
            return similar;
        }

        private void searchOverImages()
        {
            if (chosenshapes.Count == 0)
                return;
            
            //reset tracked shapes to only include starting shapes
            trackedshapes.Clear();
            trackedshapes.AddRange(chosenshapes);
            int index = listBox1.SelectedIndex;
            for (int i = 0; i < listBox1.Items.Count; i++) {

                for (int j = 0; j < trackedshapes.Count; j++) {
                    
                    if(trackedshapes.ElementAt(j).imIndex == i-1){
                        List<ShapeColorObject> templist = new List<ShapeColorObject>();
                        templist = findSimilarShapeinPicture(trackedshapes.ElementAt(j), LoadedImages["Image:" + i], i);
                        if (templist.Count != 0)
                        {
                            ShapeColorObject tempshape = findClosest(templist, trackedshapes.ElementAt(j));
                            //ShapeColorObject tempshape = templist.ElementAt(0);
                            tempshape.previousPosition = trackedshapes.ElementAt(j).pos;
                            tempshape.prev = trackedshapes.ElementAt(j);
                            tempshape.imIndex = i;
                            tempshape.objIndex = tempshape.prev.objIndex;
                            tempshape.type = tempshape.prev.type;
                            trackedshapes.Add(tempshape);
                        }
                    }

                }
            }

            for (int i = index; i < listBox1.Items.Count; i++) {
                Image<Hsv, byte> tempimage = LoadedImages["Image:" + i].Copy();
                
                for (int j = 0; j < trackedshapes.Count; j++) {
                    if (trackedshapes.ElementAt(j).imIndex == i)
                    {
                        trackedshapes.ElementAt(j).drawOnImg(ref tempimage);
                        tempimage.Draw(new Cross2DF(trackedshapes.ElementAt(j).predictedPos(), 5, 5), new Hsv(29, 170, 255), 3);
                    }
                }

                workImages["Image:" + i] = tempimage;
            }
            
        }

        private ShapeColorObject findClosest(List<ShapeColorObject> list, ShapeColorObject template)
        {
            if (list.Count == 1)
                return list.ElementAt(0);
            else
            {
                Point predicted = template.predictedPos();
                //--Find closest shape---------
                ShapeColorObject temp = null;
                int dist = 0;
                foreach (ShapeColorObject shp in list)
                {
                    int d = (shp.pos.X - predicted.X) * (shp.pos.X - predicted.X) + (shp.pos.Y - predicted.Y) * (shp.pos.Y - predicted.Y);
                    if (temp == null || d < dist)
                    {
                        temp = shp;
                        dist = d;
                    }
                }
                return temp;
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Emgu.CV.Image<Hsv, byte> outImg = (Emgu.CV.Image<Hsv, byte>)im2.Image;
            foreach (ShapeColorObject shp in trackedshapes)
            {
                shp.drawOnImg(ref outImg);
            }

            foreach (ShapeColorObject s in trackedshapes)
            {
                outImg.Draw(s.getLabel(), ref font, s.pos, new Hsv(0, 255, 255));
                outImg.Draw(new Cross2DF(new PointF((float)s.pos.X, (float)s.pos.Y), (float)5.0, (float)5.0), new Hsv(0, 200, 200), 2);
            }
            im2.Image = outImg;
            im2.Update();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            reset();
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            LoadedImages.Clear();
            if (checkBox1.Checked)
            {
                try
                {
                    removeBackground();
                }
                catch (Exception) { }
            }
            else
            {
                foreach (KeyValuePair<string, Emgu.CV.Image<Bgr, byte>> kvp in cleanLoadedImages)
                {
                    LoadedImages.Add(kvp.Key, kvp.Value.Convert<Hsv, byte>().SmoothMedian(mediansize));
                }
            }
            updateSelectedImage();
        }

        private void removeBackground()
        {
            Dictionary<string, Image<Bgr, byte>> bgrImages = new Dictionary<string, Image<Bgr, byte>>();

            foreach (KeyValuePair<string, Emgu.CV.Image<Bgr, byte>> kvp in cleanLoadedImages)
            {
                bgrImages.Add(kvp.Key, (kvp.Value.SmoothGaussian(gaussiansize)));
            }
            LoadedImages = BackgroundSubtractor.getWithoutBackground(bgrImages);
        }

        private void trackBar1_Scroll(object sender, EventArgs e)
        {
            mediansize = 1 + 2*trackBar1.Value;
            gaussiansize = 1 + 2*trackBar1.Value;
            maskedTextBox1.Text = "" + mediansize;
            reset();
            LoadedImages.Clear();
            workImages.Clear();

            int index = listBox1.SelectedIndex;
            //listBox1.Items.Clear();
            if (checkBox1.Checked)
                    removeBackground();
            else
                foreach (KeyValuePair<string, Emgu.CV.Image<Bgr, byte>> kvp in cleanLoadedImages)
                {
                    LoadedImages.Add(kvp.Key, kvp.Value.SmoothMedian(mediansize).Convert<Hsv, byte>());
                    //listBox1.Items.Add(kvp.Key);
                }
            if (LoadedImages.Count > 0)
            {
                //listBox1.SelectedIndex = index;
                updateSelectedImage();
            }
        }

        private void trackBar2_Scroll(object sender, EventArgs e)
        {
            dHue = trackBar2.Value;
            maskedTextBox2.Text = "" + dHue;
        }

        private void trackBar3_Scroll(object sender, EventArgs e)
        {
            dSaturation = trackBar3.Value;
            maskedTextBox3.Text = "" + dSaturation;
        }

        private void trackBar4_Scroll(object sender, EventArgs e)
        {
            dValue = trackBar4.Value;
            maskedTextBox4.Text = "" + dValue;
        }

        private void maskedTextBox1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                int f = 1;
                if (int.TryParse(maskedTextBox1.Text, out f))
                {
                    f = Math.Max(1, Math.Min(15, f));
                    if (f % 2 == 0)
                        f += 1;
                    maskedTextBox1.Text = "" + f;
                    trackBar1.Value = (f - 1) / 2;
                    trackBar1_Scroll(sender, e);
                }
            }
        }

        private void maskedTextBox2_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                int f = 1;
                if (int.TryParse(maskedTextBox2.Text, out f))
                {
                    f = Math.Max(0, Math.Min(90, f));
                    maskedTextBox2.Text = "" + f;
                    trackBar2.Value = f;
                    trackBar2_Scroll(sender, e);
                }
            }
        }

        private void maskedTextBox3_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                int f = 1;
                if (int.TryParse(maskedTextBox3.Text, out f))
                {
                    f = Math.Max(0, Math.Min(255, f));
                    maskedTextBox3.Text = "" + f;
                    trackBar3.Value = f;
                    trackBar3_Scroll(sender, e);
                }
            }
        }

        private void maskedTextBox4_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                int f = 1;
                if (int.TryParse(maskedTextBox4.Text, out f))
                {
                    f = Math.Max(0, Math.Min(255, f));
                    maskedTextBox4.Text = "" + f;
                    trackBar4.Value = f;
                    trackBar4_Scroll(sender, e);
                }
            }
        }

    }
}
