using System;

namespace Cession.Geometries
{
    public struct Vector : IEquatable<Vector>
    {
        public static readonly Vector Zero = new Vector (0, 0);
        public double X, Y;

        public Vector (double x, double y)
        {
            this.X = x;
            this.Y = y;
        }

        public double LengthSquared {
            get { return X * X + Y * Y; }
        }

        public double Length {
            get { return Math.Sqrt (X * X + Y * Y); }
        }

        //(0,2pi]
        public double Angle {
            get { 
                var angle = Math.Atan2 (Y, X);
                if (angle < 0)
                    return angle + Math.PI * 2;
                return angle; 
            }
        }

        public void Negate ()
        {
            X = -X;
            Y = -Y;
        }

        public static double CrossProduct (Vector v1, Vector v2)
        {
            return v1.X * v2.Y - v1.Y * v2.X;
        }

        public double CrossProduct (Vector vector)
        {
            return Vector.CrossProduct (this, vector);
        }

        public static Vector Normalize (Vector vector)
        {
            if (vector == Zero)
                return Zero;
            vector /= vector.Length;
            return vector;
        }

        public void Normalize ()
        {
            if (this == Zero)
                return;
            this /= Length;
        }

        public bool Equals (Vector v)
        {
            return X == v.X && Y == v.Y;
        }

        public override bool Equals (object obj)
        {
            if (null == obj || !(obj is Vector))
                return false;
            return this.Equals ((Vector)obj);
        }

        public static bool operator == (Vector v1, Vector v2)
        {
            return v1.Equals (v2);
        }

        public static bool operator != (Vector v1, Vector v2)
        {
            return !v1.Equals (v2);
        }

        public static Vector operator + (Vector v1, Vector v2)
        {
            v1.X += v2.X;
            v1.Y += v2.Y;
            return v1;
        }

        public static Vector operator - (Vector v1, Vector v2)
        {
            v1.X -= v2.X;
            v1.Y -= v2.Y;
            return v1;
        }

        public static Vector operator - (Vector v)
        {
            v.X = -v.X;
            v.Y = -v.Y;
            return v;
        }

        public static Vector operator * (Vector v, double scalar)
        {
            v.X *= scalar;
            v.Y *= scalar;
            return v;
        }

        public static Vector operator * (double scalar, Vector v)
        {
            v.X *= scalar;
            v.Y *= scalar;
            return v;
        }

        public static double operator * (Vector v1, Vector v2)
        {
            return v1.X * v2.X + v1.Y * v2.Y;
        }

        public static Vector operator / (Vector v, double scalar)
        {
            v.X /= scalar;
            v.Y /= scalar;
            return v;
        }

        public override int GetHashCode ()
        {
            return X.GetHashCode () ^ Y.GetHashCode ();
        }

        public override string ToString ()
        {
            return string.Format ("({0},{1})", X.ToString (), Y.ToString ());
        }

        public static double AngleBetween (Vector v1, Vector v2)
        {
            return Math.Atan2 (CrossProduct (v1, v2), v1 * v2);
        }

        public double AngleBetween (Vector vector)
        {
            return Vector.AngleBetween (this, vector);
        }

        public static Vector Rotate (Vector vector, double angle)
        {
            if (vector == Zero)
                return Zero;

            var x = Math.Cos (angle) * vector.X - Math.Sin (angle) * vector.Y;
            var y = Math.Sin (angle) * vector.X + Math.Cos (angle) * vector.Y;
            return new Vector (x, y);
        }

        public void Rotate (double angle)
        {
            this = Vector.Rotate (this, angle);
        }
    }
}
