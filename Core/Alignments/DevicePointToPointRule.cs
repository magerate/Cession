namespace Cession.Alignments
{
	using System;
	using Cession.Geometries;

	public class DevicePointToPointRule:AlignRule
	{
		public Point2 ReferencePoint
		{ 
			get{ return rule.ReferencePoint; }
			set{ rule.ReferencePoint = value; }
		}

		public float Length{get;set;}
		public float Scale{get;set;}

		public override bool IsAligned 
		{
			get {return rule.IsAligned;}
		}

		private PointToPointRule rule = new PointToPointRule();

		public DevicePointToPointRule()
		{
			Scale = 1.0f;
			Length = 24.0f;
		}

		public override void Reset ()
		{
			rule.Reset ();
		}

		protected override Point2 DoAlign (Point2 point)
		{
			rule.Length = Length * Scale;
			return rule.Align (point);
		}
	}
}

