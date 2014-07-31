namespace Cession.Modeling
{
	using System.Collections.Generic;

	using Cession.Geometries;

	public abstract class Diagram
	{
		public Diagram Parent{ get; internal set; }

		public Diagram Owner
		{
			get{
				var parent = Parent;
				while (parent != null && parent.Parent != null)
					parent = parent.Parent;
				return parent;
			}
		}

		public virtual bool CanSelect{
			get{ return false; }
		}

		protected Diagram()
		{
		}

		protected Diagram (Diagram parent)
		{
			this.Parent = parent;
		}

		public abstract Diagram HitTest (Point2 point);

		public void Offset(Vector vector)
		{
			this.Offset ((int)vector.X, (int)vector.Y);
		}

		public abstract void Offset (int x, int y);
	}
}

