namespace Cession.Modeling
{
	using System;

	using Cession.Geometries;

	public class Label:Diagram
	{
		public static HitTestProvider HitTestProvider{ get; set; }

		private string text;
		private Point2 location;

		public string Text
		{
			get{ return text; }
			set{ text = value; }
		}

		public Point2 Location{
			get{ return location; }
			set{ location = value; }
		}

		public Label (string text,Point2 location,Diagram parent):base(parent)
		{
			this.text = text;
			this.location = location;
		}

		internal override void InternalOffset (int x, int y)
		{
			location.Offset (x, y);
		}

		public override Diagram HitTest (Point2 point)
		{
			return Label.HitTestProvider.HitTest (this, point);
		}

		public override Rect Bounds {
			get {
				return Label.HitTestProvider.GetBounds (this);
			}
		}
	}
}

