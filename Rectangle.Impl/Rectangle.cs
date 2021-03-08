using System;

namespace Rectangle.Impl
{
	public sealed class Rectangle
	{
		//public int X { get; set; }
		//public int Y { get; set; }
		//public int Width { get; set; }
		//public int Height { get; set; }

		// I did not fully understand this structure of a rectangle.
		// Are X and Y a starting point? And since my recruiter remained silent, 
		// I took the liberty of redefining the rectangle across the four corner points.

		public Point A { get; private set; }
		public Point B { get; private set; }
		public Point C { get; private set; }
		public Point D { get; private set; }

		public Rectangle(Point a, Point b, Point c, Point d)
        {
			A = a;
			B = b;
			C = c;
			D = d;
        }
	}
}
