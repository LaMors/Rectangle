using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;

namespace Rectangle.Impl
{
	public static class Service
	{
        /// <summary>
        /// See TODO.txt file for task details.
        /// Method "FindRectangle" accepts a list of random points and returns a 
        /// rectangle that includes all points from the list except one (all points inside and one of them outside).
        /// </summary>
        /// <param name="points"> Random collection of points</param>
        public static Rectangle FindRectangle(List<Point> points)
		{

            if (!IsValid(points))
            {
                throw new Exception("The incoming point collection is not valid");
            }

            //Algorithm for constructing a rectangle from two points
            if (points.Count == 2)
            {
                return BuildRectangleFromTwoPoints(points);

            }

            //Algorithm for constructing a rectangle from three points
            else if (points.Count == 3)
            {
                var OutsidePoint = FindOutsidePoint(points);

                points.Remove(OutsidePoint);

                ThreePointAngle sideOfRectangle = FindSideOfRectangle(points, OutsidePoint);


               
                return new Rectangle(a: new Point((sideOfRectangle.PointA.X),       // Сonstruct a rectangle by performing 
                                                   sideOfRectangle.PointA.Y),       // a parallel translation of the line
                                                                                    // by a unit vector to the side opposite 
                                     b: new Point((sideOfRectangle.PointB.X),       // to the point that will not enter the rectangle
                                                   sideOfRectangle.PointB.Y),  

                                     c: new Point((sideOfRectangle.PointA.X + 1),
                                                   sideOfRectangle.PointA.Y + 1),

                                     d: new Point((sideOfRectangle.PointB.X + 1),
                                                   sideOfRectangle.PointB.Y + 1));
            }


            //Algorithm for constructing a rectangle from a collection that contains more than three points
            else
            {
                Point OutsidePoint = FindOutsidePoint(points);

                points.Remove(OutsidePoint);

                var sideOfRectangle = FindSideOfRectangle(points, OutsidePoint);

                double distanceClosestPoint = points.Min(p => FindDistance(p, OutsidePoint));

                Point closestPoint = points.FirstOrDefault(p => FindDistance(p, OutsidePoint) == distanceClosestPoint);

                double distancefarthestPoint = points.Max(p => FindDistance(p, OutsidePoint));

                Point farthestPoint = points.FirstOrDefault(p => FindDistance(p, OutsidePoint) == distancefarthestPoint);

                double closestPointOffsetDistance = FindDistance(sideOfRectangle.PointA, sideOfRectangle.PointB, closestPoint);

                double farthestPointOffsetDistance = FindDistance(sideOfRectangle.PointA, sideOfRectangle.PointB, farthestPoint);

                return new Rectangle(a: new Point((sideOfRectangle.PointA.X - (int)Math.Round(closestPointOffsetDistance)),   // Сonstruct a rectangle by performing a parallel
                                                   sideOfRectangle.PointA.Y - (int)Math.Round(closestPointOffsetDistance)),   // translation of the sides to the nearest and 
                                                                                                                              // farthest point relative to a point that is not included in the rectangle
                                     b: new Point((sideOfRectangle.PointB.X - (int)Math.Round(closestPointOffsetDistance)),
                                                   sideOfRectangle.PointB.Y - (int)Math.Round(closestPointOffsetDistance)),

                                     c: new Point((sideOfRectangle.PointA.X + (int)Math.Round(farthestPointOffsetDistance)),
                                                   sideOfRectangle.PointA.Y + (int)Math.Round(farthestPointOffsetDistance)),

                                     d: new Point((sideOfRectangle.PointB.X + (int)Math.Round(farthestPointOffsetDistance)),
                                                   sideOfRectangle.PointB.Y + (int)Math.Round(farthestPointOffsetDistance)));

            }
        }

        /// <summary>
        /// Returns the distance between point and line.
        /// </summary>
        /// <param name="lineStart"> Line start point</param>
        /// <param name="lineFinish"> Line finish point</param>
        /// <param name="point"> Point relative to which you want to search for the distance</param>
        private static double FindDistance(Point lineStart, Point lineFinish, Point point)
        {
            // Find the distance to the line using Heron's formula for height
            double perimeter = (FindDistance(lineStart, lineFinish) +
                                FindDistance(point, lineFinish) +
                                FindDistance(point, lineStart)) / 2;

            double distance = (2 * Math.Sqrt(perimeter * (perimeter - FindDistance(lineStart, lineFinish)) *
                                                      (perimeter - FindDistance(point, lineFinish)) *
                                                      (perimeter - FindDistance(point, lineStart)))) /
                                                                                                      FindDistance(lineStart, lineFinish);
            return distance;
        }

        /// <summary>
        /// Returns the distance between two points
        /// </summary>
        /// <param name="pointA"></param>
        /// <param name="pointB"></param>
        private static double FindDistance(Point pointA, Point pointB)
        {
            return Math.Sqrt(Math.Pow(pointA.X - pointB.X, 2) + Math.Pow(pointA.Y - pointB.Y, 2));
        }

        /// <summary>
        /// Returns a special class containing two points, an outside point and the angle between the outside point and the found point
        /// The method searches for two points, the angle between which and the outer point will be maximum.
        /// </summary>
        /// <param name="points">Point array </param>
        /// <param name="OutsidePoint">A point that should not fall within the rectangle</param>
        private static ThreePointAngle FindSideOfRectangle(List<Point> points, Point OutsidePoint)
        {
            var Angles = new List<ThreePointAngle>();

            foreach (var point in points)
            {
                double maxAngle = points.Select(p => new ThreePointAngle(p, point, OutsidePoint)).Max(a => a.Angle);

                Angles.Add(points.Select(p => new ThreePointAngle(p, point, OutsidePoint)).FirstOrDefault(a => a.Angle == maxAngle));

            }

            double maxAngleAmongAll = Angles.Max(a => a.Angle);

            return Angles.FirstOrDefault(a => a.Angle == maxAngleAmongAll);
        }

        /// <summary>
        /// Returns the rectangle constructed by rotating the first point around the second.
        /// </summary>
        /// <param name="points"></param>
        private static Rectangle BuildRectangleFromTwoPoints(List<Point> points)
        {

            // Sorry, for such a rough implementation, but time is already running out((
            for (int i = 0; points.Count < 5; i++)
            {
                if (points.Count == 2)
                    points.Add(RotatePoint(points[points.Count - 1],
                                           points[points.Count - 2],
                                           angle: -90)) ;

                else

                    points.Add(RotatePoint(points[points.Count - 1],
                                           points[points.Count - 2],
                                           angle: 90));
            }

            return new Rectangle(a: points[1],
                                 b: points[2],
                                 c: points[3],
                                 d: points[4]);
        }

        /// <summary>
        /// AngleBetween - the angle between 2 vectors
        /// </summary>
        /// <returns>
        /// Returns the the angle in degrees between vector1 and vector2
        /// </returns>
        /// <param name="vector1"> The first Vector </param>
        public static double AngleBetween(Vector2 vector1, Vector2 vector2)
        {

            //I had a problem with windowsBase.dll so I didn't have access to the standard implementation of this method
            return Math.Acos(((vector1.X * vector2.X) + (vector1.Y * vector2.Y)) /
                                                                                  (Math.Sqrt(Math.Pow(vector1.X, 2) + Math.Pow(vector1.Y, 2)) * 
                                                                                   Math.Sqrt(Math.Pow(vector2.X, 2) + Math.Pow(vector2.Y, 2)))) *
                                                                                                                                (180 / Math.PI);
        }

        /// <summary>
        /// Returns the point that is the projection of A rotated relative to origin by the specified angle
        /// </summary>
        /// <param name="origin">rotation point</param>
        /// <param name="point">Point around which it rotates</param>
        /// <param name="angle"></param>
        private static Point RotatePoint(Point origin, Point point, int angle)
        {
            var resultPoint = new Point(x: (int)Math.Round((Math.Cos(angle) * (point.X - origin.X) - Math.Sin(angle) * (point.Y - origin.Y) + origin.X)),

                                        y: (int)Math.Round((Math.Sin(angle) * (point.X - origin.X) + Math.Cos(angle) * (point.Y - origin.Y) + origin.Y)));
            return resultPoint;
        }


        /// <summary>
        /// Returns the first point that does not lie inside the polygon drawn from all other points from the collection
        /// </summary>
        /// <param name="points"></param>
        /// <returns></returns>
        private static Point FindOutsidePoint(List<Point> points)
        {

            foreach (var point in points)
            {


                points.Remove(point);

                if (IsPointOutside(point, points))
                {
                    return point;
                }
                points.Add(point);

            }

            return points[0];
		}

        /// <summary>
        /// Checks if a point is inside a rectangle
        /// </summary>
        /// <param name="point"></param>
        /// <param name="rectangle"></param>
        /// <returns></returns>
        public static bool IsPointOutside(Point point, Rectangle rectangle)
        {
            return IsPointOutside(point, new List<Point>() { rectangle.A, 
                                                             rectangle.B, 
                                                             rectangle.C, 
                                                             rectangle.D });
        }

        /// <summary>
        /// Checks if a point is inside a polygon whose corners are array points
        /// </summary>
        /// <param name="point"></param>
        /// <param name="points"></param>
        /// <returns></returns>
        private static bool IsPointOutside(Point point, List<Point> points)
        {
            var vectors = new List<Vector2>();

            foreach (var secondPoint in points)
            {
                vectors.Add(VectorByTwoPoints(point, secondPoint));

            }

            double Angle = 0;

            for (int i = 0; i < vectors.Count - 1; i++)
            {

                Angle += Math.Round(AngleBetween(vectors[i], vectors[i + 1]));

            }
            Angle += Math.Round(AngleBetween(vectors[0], vectors[vectors.Count - 1]));

            if (Angle < 359)
                return true;


            else
            return false;
        }

        /// <summary>
        /// Returns a vector constructed from two points
        /// </summary>
        /// <param name="point"></param>
        /// <param name="secondPoint"></param>
        /// <returns></returns>
        public static Vector2 VectorByTwoPoints(Point point, Point secondPoint)
        {
            return new Vector2(secondPoint.X - point.X, secondPoint.Y - point.Y);
        }

        /// <summary>
        /// Performs checks on incoming parameters of the main method
        /// </summary>
        /// <param name="points"></param>
        /// <returns></returns>
        private static bool IsValid(List<Point> points)
        {
            if (points == null)
            {
                Console.WriteLine("The incoming point collection is empty");
                return false;
            }

            if (points.Count<2)
            {
                Console.WriteLine("An incoming point collection cannot be a single point");
                return false;
            }

            for (int i = 0; i < points.Count-1; i++)
            {
                if (points[i].Equals(points[i + 1])) 
                {
                    Console.WriteLine("The incoming point collection contains two points with the same coordinates");
                    return false;
                }
            }

            return true;
        }
    }
}
