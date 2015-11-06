using System;
using Cession.Geometries;

namespace Cession.Aligning
{
    public class OrRule:AlignRule
    {
        private AlignRule rule1;
        private AlignRule rule2;

        public OrRule (AlignRule rule1, AlignRule rule2)
        {
            if (null == rule1)
                throw new ArgumentNullException ();
            if (null == rule2)
                throw new ArgumentNullException ();

            this.rule1 = rule1;
            this.rule2 = rule2;
        }

        public override bool IsAligned
        {
            get { return rule1.IsAligned || rule2.IsAligned; }
            protected set { throw new InvalidOperationException (); }
        }

        public override void Reset ()
        {
            rule1.Reset ();
            rule2.Reset ();
        }

        protected override Point DoAlign (Point point)
        {
            var alignedPoint = rule1.Align (point);

            if (rule1.IsAligned)
                return alignedPoint;

            return rule2.Align (point);
        }

        public override AlignConstraint GetConstraint (Point point)
        {
            var constraint = rule1.GetConstraint (point);
            if (null != constraint)
                return constraint;
            return rule2.GetConstraint (point);
        }
    }
}

