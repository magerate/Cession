using System;
using System.Collections.Generic;

namespace Cession.Diagrams
{
    public interface IFloatable
    {
        IFloatableHost Host{ get; }
    }

    public interface IFloatableHost
    {
        IEnumerable<Shape> GetFoldableShapes();
    }
}

