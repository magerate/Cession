using System;
using Cession.Geometries;

namespace Cession.Alignments
{
    public abstract class AlignRule
    {
        protected AlignRule ()
        {
            Enable = true;
        }

        public bool Enable{ get; set; }

        public virtual bool IsAligned{ get; protected set; }

        public virtual void Reset ()
        {
            IsAligned = false;
        }

        public virtual AlignAxis? GetAlignAxis (Point point)
        {
            var ap = Align (point);
            if (!IsAligned)
                return null;

            return DoGetAlignAxis (ap);
        }

        public Point Align (Point point)
        {
            Reset ();
            if (!Enable)
                return point;

            return DoAlign (point);
        }

        protected abstract Point DoAlign (Point point);

        protected virtual AlignAxis? DoGetAlignAxis (Point point)
        {
            return null;
        }

        public static AlignRule operator & (AlignRule rule1, AlignRule rule2)
        {
            return new AndRule (rule1, rule2);
        }

        public static AlignRule operator | (AlignRule rule1, AlignRule rule2)
        {
            return new OrRule (rule1, rule2);
        }
    }
}

