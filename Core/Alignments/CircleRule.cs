using System;

using Cession.Geometries;

namespace Cession.Aligning
{
    public class CircleRule:AlignRule
    {
        public Point Center{ get; set;}
        public double Radius{ get; set;}

        public double NearLength{ get; set;}

        public CircleRule ()
        {
            NearLength = 16;
        }

        protected override Point DoAlign (Point point)
        {
            double distance = point.DistanceBetween (Center);
            if (Math.Abs (distance - Radius) <= NearLength)
            {
                var v = point - Center;
                v *= Radius / v.Length;
                IsAligned = true;
                return Center + v;
            }
            return point;
        }
    }
}

