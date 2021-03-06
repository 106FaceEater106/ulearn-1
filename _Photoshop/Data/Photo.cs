using System;
using System.Drawing;
using System.Runtime.CompilerServices;

namespace MyPhotoshop
{
	public class Photo
	{
		public readonly int Width;
		public readonly int Height;
		private Pixel[,] data;

        public Photo(int width, int height)
        {
            Width = width;
            Height = height;
            data = new Pixel[width, height];
        }

        public Pixel this[int x, int y]
        {
            get => data[x,y];
            set => data[x, y] = value;
        }
	}


}

