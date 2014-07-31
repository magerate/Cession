namespace Cession.Alignments
{
	using System;
	using Cession.Geometries;

	public class PointToPointRule:AlignRule
	{
		public Point2 ReferencePoint{ get; set; }
		public double Length{get;set;}


		protected override Point2 DoAlign (Point2 point)
		{
			if (point.DistanceBetween (ReferencePoint) <= Length) {
				IsAligned = true;
				return ReferencePoint;
			}

			return point;
		}

	}
}

