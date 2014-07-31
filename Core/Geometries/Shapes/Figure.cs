namespace Cession.Geometries.Shapes
{
	using System;

	public abstract class Figure
	{
		protected Figure ()
		{
		}

		public abstract double GetArea ();
		public abstract double GetPerimeter ();

		public abstract bool Contains(Point2 point);

		public void Offset(Vector vector)
		{
			this.Offset ((int)vector.X, (int)vector.Y);
		}

		public abstract void Offset(int x,int y);
	}
}

