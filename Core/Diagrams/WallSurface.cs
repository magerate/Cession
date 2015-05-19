using System;
using Cession.Geometries;

namespace Cession.Diagrams
{
    public class WallSurface:Shape
    {
        public WallSurface ()
        {
        }

        internal override void DoOffset (double x, double y)
        {
            throw new NotImplementedException ();
        }

        internal override void DoRotate (Point point, double radian)
        {
            throw new NotImplementedException ();
        }
    }
}

