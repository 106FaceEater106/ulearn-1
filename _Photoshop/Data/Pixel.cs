using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace MyPhotoshop
{
    public struct Pixel
    {
        public readonly double R;
        public readonly double G;
        public readonly double B;

        public Pixel(double r, double g, double b)
        {
            if(r > 1 || g > 1 || b > 1) throw new ArgumentException();
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
            var red = Clamp(pixel.R * modifier, 0, 1);
            var green = Clamp(pixel.G * modifier, 0, 1);
            var blue = Clamp(pixel.B * modifier, 0, 1);
            return new Pixel(red, green, blue);
        }

        private static double Clamp(double value, double min, double max)  
        {  
            return (value < min) ? min : (value > max) ? max : value;  
        }
    }
}
