using System;
using System.Collections.Generic;

namespace Cession.Diagrams
{
    public interface IFoldable
    {
        IFoldableHost Host{ get; }
    }

    public interface IFoldableHost
    {
        IEnumerable<Shape> GetFoldableShapes();
    }
}

