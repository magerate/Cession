namespace Cession.Diagrams
{
	using System;
	using System.Collections.Generic;

	using Cession.Geometries;

	public abstract class Shape
	{
		private static Dictionary<Type,HitTestProvider> m_hitTestors;

		internal ShapeConstraints Constraints{ get; set; }

		public bool CanSelect{
			get{ return (Constraints & ShapeConstraints.CanSelect) != 0; }
		}

		public bool CanHitTest{
			get{ return (Constraints & ShapeConstraints.CanHitTest) != 0; }
		}

		public bool CanOffset{
			get{ return (Constraints & ShapeConstraints.CanOffset) != 0; }
		}

		public bool CanRotate{
			get{ return (Constraints & ShapeConstraints.CanRotate) != 0; }
		}

		public bool CanAssign{
			get{ return (Constraints & ShapeConstraints.CanAssign) != 0; }
		}



		public Shape Parent{ get; internal set; }
		public abstract Rect Bounds{ get; }
			

		public Shape Owner
		{
			get{
				var parent = Parent;
				while (parent != null && parent.Parent != null)
					parent = parent.Parent;
				return parent;
			}
		}


		protected Shape():this(null)
		{
		}

		protected Shape (Shape parent)
		{
			this.Parent = parent;
			this.Constraints = ShapeConstraints.All;
		}

		public void Offset (int x, int y){
			if (!CanOffset)
				throw new InvalidOperationException ();
			InternalOffset (x, y);
		}

		public void Rotate(Point2 point,double radian){
			if(!CanRotate)
				throw new InvalidOperationException ();
				
			InternalRotate (point, radian);
		}

		internal abstract void InternalOffset(int x,int y);
		internal abstract void InternalRotate(Point2 point,double radian);

	}
}

