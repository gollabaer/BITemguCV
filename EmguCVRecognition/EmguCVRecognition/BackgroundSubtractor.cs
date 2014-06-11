using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Emgu.CV;
using Emgu.CV.Util;
using Emgu.CV.Structure;


namespace EmguCVRecognition
{
    class BackgroundSubtractor
    {
        
        public static Dictionary<string, Image<Hsv, byte>> getWithoutBackground(Dictionary<string, Image<Bgr, byte>> images) {

            VideoWriter videoW = new VideoWriter("test.avi", 30, images["Image:0"].Width, images["Image:0"].Height, true);
            
            Dictionary<string, Image<Hsv, byte>> outImages = new Dictionary<string,Image<Hsv,byte>>();

            //schreib die bilder ins video
            for (int i = 0; i < images.Count; i++) {
                string key = "Image:" + i;
                videoW.WriteFrame(images[key]);
            }

            //erstelle neuen backgroundsubtractor
            Emgu.CV.VideoSurveillance.BackgroundSubtractorMOG subtractor = new Emgu.CV.VideoSurveillance.BackgroundSubtractorMOG(3,4,0.6,0.01 );

        
            Image<Gray, byte> backgroundmodel = new Image<Gray,byte>(images["Image:"+0].Width,images["Image:" +0].Height);
            Image<Bgr, byte> bgrOhneHintergrund = new Image<Bgr, byte>(backgroundmodel.Width,backgroundmodel.Height);
            
            for (int i = 0; i < images.Count; i++)
            {
                string key = "Image:" + i;
                subtractor.Update(images[key]);
                backgroundmodel = subtractor.ForegroundMask;
                backgroundmodel = backgroundmodel.Mul(1.0 / 255.0);

                bgrOhneHintergrund[0] = images[key][0].Mul(backgroundmodel);
                bgrOhneHintergrund[1] = images[key][1].Mul(backgroundmodel);
                bgrOhneHintergrund[2] = images[key][2].Mul(backgroundmodel);

                outImages.Add(key, bgrOhneHintergrund.Convert<Hsv, byte>());
            }

            return outImages;
        }

    }
}
