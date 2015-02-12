using System;
using System.Collections;
using System.Collections.Generic;

using Cession.Geometries;

namespace Cession.Diagrams
{
    public class Region:CompositeShape
    {
        private Path _contour;
        private List<Path> _holes = null;

        public Path Contour {
            get{ return _contour; }
        }

        public IList<Path> Holes {
            get{ return _holes; }
        }

        public Region (Path contour)
        {
            if (null == contour)
                throw new ArgumentNullException ();

            _contour = contour;
        }

        public override IEnumerator<Shape> GetEnumerator ()
        {
            yield return _contour;

            if (null != _holes) {
                foreach (var h in _holes) {
                    yield return h;
                }
            }
        }
    }
}

