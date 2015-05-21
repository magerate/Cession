using System;
using System.Collections.Generic;

namespace Cession.Diagrams
{
    public interface IFoldable
    {
        IEnumerable<Shape> GetFoldShapes();
        void Layout();
    }
}

