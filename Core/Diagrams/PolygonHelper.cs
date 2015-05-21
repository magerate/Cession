using System;
using System.Linq;
using System.Collections.Generic;

using ClipperLib;

using Cession.Geometries;

namespace Cession.Diagrams
{
    public static class PolygonHelper
    {
        private static ClipperOffset s_clipperOffset = new ClipperOffset();

        public static Path Inflate(Path path,double size)
        {
            var cp = path.Select (p => new IntPoint ((int)p.X, (int)p.Y)).ToList ();

            s_clipperOffset.Clear ();
            s_clipperOffset.AddPath (cp, JoinType.jtMiter, EndType.etClosedLine);

            List<List<IntPoint>> solution = new List<List<IntPoint>> ();
            s_clipperOffset.Execute (ref solution, size);

            var points = solution [0].Select (p => new Point ((double)p.X, (double)p.Y)).ToArray ();
            return new Path(points);
        }
    }
}

