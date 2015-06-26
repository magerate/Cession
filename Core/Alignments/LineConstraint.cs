using System;

using Cession.Geometries;

namespace Cession.Aligning
{
    public class LineConstraint:AlignConstraint
    {
        public Point P1{ get; private set;}

        public LineConstraint (Point p1,Point p2)
        {
            P1 = p1;
            AlignedPoint = p2;
        }

        public override Point? IntersectWith (AlignConstraint constraint)
        {
            if (null == constraint)
                return null;

            if (constraint is LineConstraint)
            {
                return IntersectWithLine (constraint as LineConstraint);
            }
            else if (constraint is ArcConstraint)
            {
                return AlignConstraint.Intersect (this, constraint as ArcConstraint);
            }
            return null;
        }

        private Point? IntersectWithLine(LineConstraint lineConstraint)
        {
            var v1 = AlignedPoint - P1;
            var v2 = lineConstraint.AlignedPoint - lineConstraint.P1;

            v1.Normalize ();
            v2.Normalize ();
            double crossProduct = Math.Abs (v1.CrossProduct (v2));
            //如果两条线接近平行 就认为没有交点 不然交点可能很远
            //crossProduct <= 0.052 大概3度
            if (crossProduct <= 0.052)
                return null;

            return Line.Intersect (P1, AlignedPoint, lineConstraint.P1, lineConstraint.AlignedPoint);
        }
    }
}

