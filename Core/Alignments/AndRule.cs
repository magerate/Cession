using System;
using Cession.Geometries;

namespace Cession.Aligning
{
    public class AndRule:AlignRule
    {
        private AlignRule rule1;
        private AlignRule rule2;

        public AndRule (AlignRule rule1, AlignRule rule2)
        {
            if (null == rule1)
                throw new ArgumentNullException ();
            if (null == rule2)
                throw new ArgumentNullException ();

            this.rule1 = rule1;
            this.rule2 = rule2;
        }

        public override void Reset ()
        {
            rule1.Reset ();
            rule2.Reset ();
        }

        public override bool IsAligned
        {
            get { return rule1.IsAligned || rule2.IsAligned; }
            protected set { throw new InvalidOperationException (); }
        }

        protected override Point DoAlign (Point point)
        {
            var constraint1 = rule1.GetConstraint(point);

            if(null == constraint1)
                return rule2.Align(point);

            var constraint2 = rule2.GetConstraint(point);

            if (null == constraint2)
                return constraint1.AlignedPoint;

            var cross = constraint1.IntersectWith (constraint2);
            if (cross != null)
                return cross.Value;
            else
            {
                rule2.Reset ();
                return constraint1.AlignedPoint;
            }
        }

    }
}

