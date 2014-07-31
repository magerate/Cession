namespace Cession.Alignments
{
	using System;
	using Cession.Geometries;

	public struct AlignAxis
	{
		private Point2 p1;
		private Point2 p2;

		public Point2 P1 
		{
			get {return p1;}
			set {p1 = value;}
		}

		public Point2 P2 
		{
			get {return p2;}
			set {p2 = value;}
		}



		public AlignAxis (Point2 p1,Point2 p2)
		{
			this.p1 = p1;
			this.p2 = p2;
		}

		public static bool IsValid(AlignAxis? alignAxis)
		{
			return alignAxis != null && alignAxis.Value.P1 != alignAxis.Value.P2;
		}
	}
}

