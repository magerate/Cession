using System;
using System.Collections.Generic;

namespace Cession.Diagrams
{
    public abstract class LayoutProvider
    {
        public abstract void Layout (ClosedShape contour, IEnumerable<WallSurface> walls);
    }
}

