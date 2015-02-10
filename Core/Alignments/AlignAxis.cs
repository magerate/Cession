using System;
using Cession.Geometries;

namespace Cession.Alignments
{
	public struct AlignAxis
	{
		private Point p1;
		private Point p2;

		public Point P1 
		{
			get {return p1;}
			set {p1 = value;}
		}

		public Point P2 
		{
			get {return p2;}
			set {p2 = value;}
		}

		public AlignAxis (Point p1,Point p2)
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

