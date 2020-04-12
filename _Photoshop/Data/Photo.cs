using System;
using System.Drawing;

namespace MyPhotoshop
{
	public class Photo
	{
		public int width;
		public int height;
		public Pixel[,] data;
	}

    public struct Pixel
    {
        public double R;
        public double G;
        public double B;

        public Pixel(double r, double g, double b)
        {
            R = r;
            G = g;
            B = b;
        }

        public Pixel WithRed(double red)
        {
            return new Pixel(red, G, B);
        }
        public Pixel WithGreen(double green)
        {
            return new Pixel(R, green, B);
        }
        public Pixel WithBlue(double blue)
        {
            return new Pixel(R, G, blue);
        }

        public static Pixel operator*(Pixel pixel, double modifier)
        {
            return new Pixel(pixel.R*modifier, pixel.G*modifier, pixel.B*modifier);
        }
    }

}

