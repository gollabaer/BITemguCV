using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Emgu.CV.Structure;
using System.Drawing;
using Emgu.CV;

namespace EmguCVRecognition
{
  public  class ShapeColorObject: IEquatable<ShapeColorObject>
    {
        public string image;
        public Point previousPosition;
        public List<LineSegment2D> lineSegments; 
        double area;
        public shape type;
        public ShapeColorObject prev;
        public enum shape
        {
            triangle = 0,
            rectangle = 45,
            circle = 90,
            undefined = 145
        };

        private Hsv color;
        int x;
        int y;

        public Point pos
        {
            get { return new Point(x, y); }
        }

        public Point predictedPos()
        {
            int deltaX = (prev.deltaPos.X == 0) ? deltaPos.X : prev.deltaPos.X;
            int deltaY = (prev.deltaPos.Y == 0) ? deltaPos.Y : prev.deltaPos.Y;
            return new Point(pos.X + 2 * deltaPos.X - deltaX, pos.Y + 2 * deltaPos.Y - deltaY);
        }

        public Point deltaPos
        {
            get { return new Point(pos.X - previousPosition.X, pos.Y - previousPosition.Y); }
        }

        public ShapeColorObject(double area, shape type, Hsv color, int x, int y)
        {
            this.area = area;
            this.type = type;
            this.color = color;
            this.x = x;
            this.y = y;
            lineSegments = new List<LineSegment2D>();
            previousPosition = pos;
            prev = this;
        }

        public bool compare(ShapeColorObject shape2, int cTolerance, int aTolerance)
        {
            if (!compareHues(this.color.Hue, shape2.color.Hue, cTolerance))
                return false;
            else if (this.type != shape2.type)
                return false;
            else if (!compare(this.area, shape2.area, aTolerance))
                return false;
            else return true;
        }

        public bool Equals(ShapeColorObject shape2)
        {
            return (this.pos == shape2.pos && this.color.Hue == shape2.color.Hue && this.type == shape2.type && this.image == shape2.image);
        }

        public static bool compareHues(double h1, double h2, int tolerance)
        {
            tolerance = Math.Min(Math.Max(tolerance, 0), 63);
            double low = h2 - tolerance;
            double high = h2 + tolerance;
            bool result = false;

            if (high < 180 && low >= 0)
                result = (h1 < high && h1 > low);
            else if (high >= 180)
                result = (h1 > low || h1 < high - 180);
            else
                result = (h1 > low + 180 || h1 < high);

            return result;
        }

        private bool compare(double a, double b, double tolerance)
        {
            double low = Math.Max(b - b * tolerance/100, 0);
            double high = b + b * tolerance/100;

            return (a > low && a < high);
        }

        public string toString()
        {
            string s = type.ToString();
            string a = (area >= 10000) ? (int)(area / 1000) + "k" : area + "";
            s += " @(" + x + "; " + y + "; " + a + "): " + color.Hue;
            return s;
        }
        
        public void drawOnImg(ref Emgu.CV.Image<Hsv,byte> img){
            foreach(LineSegment2D segment in lineSegments)
                img.Draw(segment,new Hsv((int)type,240,240), 2);
            if (previousPosition != null)//klappt nicht hat immer nen wert
                img.Draw(new LineSegment2D(previousPosition, pos), new Hsv((int)type, 255, 255), 2);
        }

        public Hsv getColor() { return color; }
    }
}
