using System;

using Cession.Geometries;

namespace Cession.Aligning
{
    public abstract class AlignConstraint
    {
        public Point AlignedPoint{ get; protected set;}

        protected AlignConstraint ()
        {
        }

        public abstract Point? IntersectWith(AlignConstraint constraint);

        public static Point? Intersect(LineConstraint lineConstraint,ArcConstraint arcConstraint)
        {
            if(null == lineConstraint)
                return null;

            if (null == arcConstraint)
                return null;

//            CrossPoint cross1 = new CrossPoint();;
//            CrossPoint cross2 = new CrossPoint();;
//
//            Arc2d arc = new Arc2d (arcConstraint.P1, arcConstraint.P2, arcConstraint.AlignedPoint);
//            int result = GeoApiLine.Arc_Line_Crosses (arc, lineConstraint.P1, lineConstraint.AlignedPoint, 
//                ref cross1,ref cross2,LinePart.InLine, LinePart.All);
//         
//            if (result == 1)
//                return cross1;
//            else if(result == 2)
//            {
//                if (cross1.point.DistanceSqr (arcConstraint.AlignedPoint) <= cross2.point.DistanceSqr (arcConstraint.AlignedPoint))
//                    return cross1;
//                return cross2;
//            }

            return null;
        }
    }
}

