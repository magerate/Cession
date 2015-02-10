using System;
using Cession.Geometries;

namespace Cession.Alignments
{
	public class DevicePointToPointRule:AlignRule
	{
		public Point? ReferencePoint
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

		protected override Point DoAlign (Point point)
		{
			rule.Length = Length * Scale;
			return rule.Align (point);
		}
	}
}

