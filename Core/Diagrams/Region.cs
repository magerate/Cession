using System;
using System.Collections;
using System.Collections.Generic;

using Cession.Geometries;

namespace Cession.Diagrams
{
	public class Region:CompositeShape
	{
		private ClosedShape _contour;
		private List<ClosedShape> _holes;

		public Region (ClosedShape contour)
		{
			if (null == contour)
				throw new ArgumentNullException ();

			_contour = contour;
		}

		public override IEnumerator<Shape> GetEnumerator(){
			yield return _contour;

			if (null != _holes) {
				foreach (var h in _holes) {
					yield return h;
				}
			}
		}
	}
}

