using Cession.Geometries;

namespace Cession.Diagrams
{
    public abstract class ClosedShape:Shape
    {
        public abstract double GetPerimeter ();

        public abstract double GetArea ();

        protected ClosedShape ()
        {
        }

        protected ClosedShape (Shape parent) : base (parent)
        {
        }
    }
}

