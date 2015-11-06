using System;

using Cession.Geometries;

namespace Cession.Aligning
{
    public class ArcConstraint:AlignConstraint
    {
        public Point P1{ get; private set;}
        public Point P2{ get; private set;}

        public ArcConstraint (Point p1,Point p2,Point p3)
        {
            P1 = p1;
            P2 = p2;
            AlignedPoint = p3;
        }

        public override Point? IntersectWith (AlignConstraint constraint)
        {
            if(null == constraint)
                return null;

            if (constraint is LineConstraint)
                return AlignConstraint.Intersect (constraint as LineConstraint, this);
            else if (constraint is ArcConstraint)
                throw new NotSupportedException ();

            return null;
        }
    }
}

