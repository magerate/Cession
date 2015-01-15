using System;

namespace Cession.Geometries
{
    public struct Ray : IEquatable<Ray>
    {
		private Point2 _point;
		private Vector _direction;

        public Point2 Point
        {
            get { return _point; }
            set { _point = value; }
        }

        public Vector Direction
        {
            get { return _direction; }
            set { _direction = value; }
        }

        public Ray(Point2 point, Vector direction)
        {
            this._point = point;
            this._direction = direction;
        }

        public bool Equals(Ray ray)
        {
            return _point == ray._point && _direction.CrossProduct(ray._direction) == 0;
        }

        public override bool Equals(object obj)
        {
            if (null == obj || !(obj is Ray))
                return false;
            return Equals((Ray)obj);
        }

        public override int GetHashCode()
        {
            return _point.GetHashCode() ^ _direction.Angle.GetHashCode();
        }

//        public static bool Contains(Point2 p1, Vector vector, Point2 point)
//        {
//            var p2 = p1 + vector;
//            return OpenRange.Contains(p1.X, vector.X, point.X) && 
//                    Line.Contains(p1, p2, point);
//        }

//        public static bool Contains(Ray ray,Point2 point)
//        {
//            return Ray.Contains(ray.point, ray.direction, point);
//        }

//        public static bool AlmostContains(Point2 p1,Vector vector,Point2 point)
//        {
//            return OpenRange.Contains(p1.X, vector.X, point.X, Constants.Epsilon) &&
//                    Line.AlmostContains(p1, p1 + vector, point);
//        }

//        public static bool AlmostContains(Ray ray,Point2 point)
//        {
//            return Ray.AlmostContains(ray.point, ray.direction, point);
//        }

       

//        public static Point2? CrossBetween(Point2 p1, Vector v1, Point2 p2, Vector v2)
//        {
//            var cross = Line.Intersect(p1, p1 + v1, p2, p2 + v2);
//            if (cross.HasValue &&
//                OpenRange.Contains(p1.X, v1.X, cross.Value.X) &&
//                OpenRange.Contains(p2.X, v2.X, cross.Value.X))
//                return cross;
//            return null;
//        }

//        public static Point2? CrossBetween(Ray ray1, Ray ray2)
//        {
//            return Ray.CrossBetween(ray1.Point, ray1.Direction, ray2.Point, ray2.Direction);
//        }

//        public static Point2? CrossBetweenLine(Point2 p1, Vector v1, Point2 p2, Point2 p3)
//        {
//            var cross = Line.Intersect(p1, p1 + v1, p2, p3);
//            if (cross.HasValue && OpenRange.Contains(p1.X, v1.X, cross.Value.X))
//                return cross;
//            return null;
//        }

//        public static Point2? CrossBetweenLine(Ray ray, Line line)
//        {
//            return Ray.CrossBetweenLine(ray.point, ray.direction, line.P1, line.P2);
//        }
    }
}
