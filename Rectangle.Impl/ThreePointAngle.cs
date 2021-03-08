using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rectangle.Impl
{
    public class ThreePointAngle
    {
       

        public Point PointA { get; set; }

        public Point PointB { get; set; }

        public Point Foundation { get; set; }

        public double Angle { get => Service.AngleBetween(Service.VectorByTwoPoints(Foundation, PointA), Service.VectorByTwoPoints(Foundation, PointB)); }

        public ThreePointAngle(Point pointA, Point pointB, Point foundation)
        {
            PointA = pointA;
            PointB = pointB;
            Foundation = foundation;
        }
    }
}
