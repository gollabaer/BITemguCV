using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Emgu.CV;
using Emgu.CV.Util;
using Emgu.CV.Structure;


namespace EmguCVRecognition
{
    class BackgroundSubtractor
    {

        public static int dilatationErosionNumIter = 10;
        public static float backgroundRatio = 0.7f;

        public static bool exception = false;
        
        public static Dictionary<string, Image<Hsv, byte>> getWithoutBackground(Dictionary<string, Image<Bgr, byte>> images) {

            Dictionary<string, Image<Hsv, byte>> outImages = new Dictionary<string, Image<Hsv, byte>>();
            try
            {
                VideoWriter videoW = new VideoWriter("test.avi", 4, images["Image:0"].Width, images["Image:0"].Height, true);

                //schreib die bilder ins video
                for (int i = 0; i < images.Count; i++)
                {
                    string key = "Image:" + i;
                    videoW.WriteFrame(images[key]);
                }

                //erstelle neuen backgroundsubtractor
                Emgu.CV.VideoSurveillance.BackgroundSubtractorMOG subtractor = new Emgu.CV.VideoSurveillance.BackgroundSubtractorMOG(3, 4, backgroundRatio, 0.01);


                Image<Gray, byte> backgroundmodel = new Image<Gray, byte>(images["Image:" + 0].Width, images["Image:" + 0].Height);
                Image<Bgr, byte> bgrOhneHintergrund = new Image<Bgr, byte>(backgroundmodel.Width, backgroundmodel.Height);

                for (int i = 0; i < images.Count; i++)
                {
                    string key = "Image:" + i;
                    subtractor.Update(images[key]);
                    backgroundmodel = subtractor.ForegroundMask;
                    backgroundmodel = backgroundmodel.Mul(1.0 / 255.0);
                    backgroundmodel._Dilate(dilatationErosionNumIter);
                    backgroundmodel._Erode(dilatationErosionNumIter);

                    bgrOhneHintergrund[0] = images[key][0].Mul(backgroundmodel);
                    bgrOhneHintergrund[1] = images[key][1].Mul(backgroundmodel);
                    bgrOhneHintergrund[2] = images[key][2].Mul(backgroundmodel);

                    outImages.Add(key, bgrOhneHintergrund.Convert<Hsv, byte>());
                }
            }
            catch (Exception e) {
                foreach (KeyValuePair<string, Emgu.CV.Image<Bgr, byte>> kvp in images)
                    outImages.Add(kvp.Key, kvp.Value.Convert<Hsv, byte>());
                MessageBox.Show(e.ToString());
                exception = true;
            }
            return outImages;
        }

    }
}
