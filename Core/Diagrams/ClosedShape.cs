using System;

using Cession.Geometries;

namespace Cession.Diagrams
{
    public abstract class ClosedShape:Shape
    {
        public event EventHandler<EventArgs> ContourChanged;

        protected ClosedShape ()
        {
        }

        public abstract double GetPerimeter();
        public abstract double GetArea();

        public abstract ClosedShape Inflate(double size);

        public abstract Tuple<ClosedShape,ClosedShape> Split(Polyline polyline);

        protected void OnContourChanged()
        {
            ContourChanged?.Invoke (this, EventArgs.Empty);
        }
    }
}

