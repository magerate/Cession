﻿using Cession.Geometries;

namespace Cession.Diagrams
{
	public abstract class Segment:Shape
	{
		public static readonly RoutedEvent CornerMoveEvent = new RoutedEvent ("Offset", 
			typeof(RoutedEventHandler), 
			typeof(Segment));

		private Point2 _point1;

		public Point2 Point1{
			get{ return _point1; }
			set{
				if (value != _point1) {
					_point1 = value;
					var rea = new RoutedEventArgs (CornerMoveEvent,this);
					RaiseEvent (rea);
				}
			}
		}


		internal Segment (Point2 point)
		{
			_point1 = point;
			Ability = ShapeAbility.CanAssign | ShapeAbility.CanHitTest;
		}

		public Segment Next{
			get{ 
				if (Parent is Path)
					return ((Path)Parent).GetNextSide (this);
				else if (Parent is PolyLine)
					return ((PolyLine)Parent).GetNextSide (this);
				return null;
			}
		}

		public Segment Previous{
			get{
				if (Parent is Path)
					return ((Path)Parent).GetPreviousSide (this);
				else if (Parent is PolyLine)
					return ((PolyLine)Parent).GetPreviousSide (this);
				return null;
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

