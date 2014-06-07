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
        public Dictionary<string, Emgu.CV.Image<Bgr,Byte>> LoadedImages = new Dictionary<string,Emgu.CV.Image<Bgr,Byte>>();

        public Emgu.CV.UI.ImageBox  im1;
        public Emgu.CV.UI.ImageBox im2;

        //----------------------------------------------------



        public Form1()
        {
            InitializeComponent();

            this.MouseClick += new MouseEventHandler(myForm_MouseClick);
            im1 = imageBox1;
            im2 = imageBox2;
            im1.SizeMode = PictureBoxSizeMode.StretchImage;
            im2.SizeMode = PictureBoxSizeMode.StretchImage;
            listBox1.MultiColumn = true;
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
                        Emgu.CV.Image<Bgr,Byte> loadedImage = new Emgu.CV.Image<Bgr, Byte>(tempBitmap);
                        string loadedImageName = "Image:"+currentImage;
                        //Save Images
                        listBox1.Items.Add(loadedImageName);
                        LoadedImages.Add(loadedImageName, loadedImage);
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
            
        }

        private void imageBox2_Click(object sender, EventArgs e)
        {

        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            im1.Image = LoadedImages[listBox1.SelectedItem.ToString()];
        }


    }
}
