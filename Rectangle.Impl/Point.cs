using System;
using System.Collections.Generic;
using System.Text;

namespace Rectangle.Impl
{
	public sealed class Point
	{
		public int X { get; private set; }
		public int Y { get; private set; }

		public Point(int x, int y)
        {
			X = x;
			Y = y;
        }

        public bool Equals(Point point)
        {
            if (this.X == point.X &&
                this.Y == point.Y)
                return true;
           
            else
                return false;
        }
    }
}
