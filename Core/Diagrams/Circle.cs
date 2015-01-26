using System;
using Cession.Geometries;

namespace Cession.Diagrams
{
	public class Circle:ClosedShape
	{
		private Point2 _center;
		private int _radius;


		public Circle (Point2 center,int radius,Shape parent):base(parent)
		{
			_center = center;
			_radius = radius;
		}

		protected override Rect DoGetBounds ()
		{
			return new Rect (_center.X - _radius, _center.Y - _radius, 2 * _radius, 2 * _radius);
		}

		protected override bool DoContains (Point2 point)
		{
			return point.DistanceBetween (_center) <= _radius;
		}

		internal override void DoOffset (int x, int y)
		{
			_center.Offset (x, y);
		}

		internal override void DoRotate (Point2 point, double radian)
		{
			if (point == _center)
				return;

			_center.Rotate (point, radian);
		}

		public override double GetArea ()
		{
			return Math.PI * _radius * _radius;
		}

		public override double GetPerimeter ()
		{
			return 2 * Math.PI * _radius;
		}

	}
}

