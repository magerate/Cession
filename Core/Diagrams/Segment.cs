using Cession.Geometries;

namespace Cession.Diagrams
{
	internal interface ISegmentHost{
		Point2 GetNextPoint (Segment segment);
		Segment GetNextSide(Segment segment);
		Segment GetPreviousSide(Segment segment);
	}

	public abstract class Segment:Shape
	{
		private Point2 _point1;

		public Point2 Point1{
			get{ return _point1; }
		}

		internal ISegmentHost Host{
			get{ return Parent as ISegmentHost; }
		}

		internal Segment (Point2 point)
		{
			_point1 = point;
			Ability = ShapeAbility.CanAssign | ShapeAbility.CanHitTest;
		}

		public Segment Next{
			get{ 
				if (null == Host)
					return null;
				return Host.GetNextSide (this); 
			}
		}

		public Segment Previous{
			get{
				if (null == Host)
					return null;
				return Host.GetPreviousSide (this); 
			}
		}

		internal override void DoOffset (int x, int y)
		{
			_point1.Offset (x, y);
		}

		internal override void DoRotate (Point2 point, double radian)
		{
			_point1.Rotate (point, radian);
		}
	}
}

