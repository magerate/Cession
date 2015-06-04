using System;
using System.Collections.Generic;

namespace Cession.Diagrams
{
    public interface IFoldableHost
    {
        IEnumerable<Shape> GetFoldableShapes();
    }
}

