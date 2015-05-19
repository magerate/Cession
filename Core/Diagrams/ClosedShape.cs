using System;

using Cession.Geometries;

namespace Cession.Diagrams
{
    public abstract class ClosedShape:Shape
    {
        protected ClosedShape ()
        {
        }

        public abstract double GetPerimeter();
        public abstract double GetArea();
    }
}

