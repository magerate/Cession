using System;
using Cession.Geometries;

namespace Cession.Diagrams
{
	public class Label:Shape
	{
		private string _text;
		private Point2 _location;

		public string Text
		{
			get{ return _text; }
			set{ _text = value; }
		}

		public Point2 Location{
			get{ return _location; }
			set{ _location = value; }
		}

		public Label (string text,Point2 location,Shape parent):base(parent)
		{
			this._text = text;
			this._location = location;
		}

		internal override void DoOffset (int x, int y)
		{
			_location.Offset (x, y);
		}

		internal override void DoRotate (Point2 point, double radian)
		{
			throw new NotSupportedException ();
		}
	}
}

