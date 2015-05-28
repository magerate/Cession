using System;
using System.Collections.Generic;

namespace Cession.Diagrams
{
    public abstract class LayoutProvider
    {
        public static LayoutProvider CurrentProvider{ get; set; }

        static LayoutProvider()
        {
            CurrentProvider = new FanLayoutProvider ();
        }

        public abstract void Layout (ClosedShape contour, IEnumerable<WallSurface> walls);
    }
}

