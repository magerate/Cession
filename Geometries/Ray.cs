using System;

namespace Cession.Geometries
{
    public struct Ray : IEquatable<Ray>
    {
        private Point _point;
        private Vector _direction;

        public Point Point {
            get { return _point; }
            set { _point = value; }
        }

        public Vector Direction {
            get { return _direction; }
            set { _direction = value; }
        }

        public Ray (Point point, Vector direction)
        {
            this._point = point;
            this._direction = direction;
        }

        public bool Equals (Ray ray)
        {
            return _point == ray._point && _direction.CrossProduct (ray._direction) == 0;
        }

        public override bool Equals (object obj)
        {
            if (null == obj || !(obj is Ray))
                return false;
            return Equals ((Ray)obj);
        }

        public override int GetHashCode ()
        {
            return _point.GetHashCode () ^ _direction.Angle.GetHashCode ();
        }
    }
}
