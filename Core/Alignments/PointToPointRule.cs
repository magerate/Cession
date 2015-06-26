using System;
using Cession.Geometries;

namespace Cession.Aligning
{
    public class PointToPointRule:AlignRule
    {
        public Point? ReferencePoint{ get; set; }

        public double NearLength{ get; set; }

        protected override Point DoAlign (Point point)
        {
            if (ReferencePoint == null)
                return point;

            if (point.DistanceBetween (ReferencePoint.Value) <= NearLength)
            {
                IsAligned = true;
                return ReferencePoint.Value;
            }

            return point;
        }

    }
}

