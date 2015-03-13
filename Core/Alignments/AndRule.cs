using System;
using Cession.Geometries;

namespace Cession.Alignments
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
            var axis1 = rule1.GetAlignAxis (point);

            if (!AlignAxis.IsValid (axis1))
                return rule2.Align (point);

            var axis2 = rule2.GetAlignAxis (point);

            if (!AlignAxis.IsValid (axis2))
                return rule1.Align (point);

            var cross = Line.Intersect (axis1.Value.P1, 
                   axis1.Value.P2, 
                   axis2.Value.P1, 
                   axis2.Value.P2);
            if (cross.HasValue)
                return new Point ((int)cross.Value.X, (int)cross.Value.Y);

            rule2.Reset ();
            return axis1.Value.P2;
        }

    }
}

