namespace Cession.Drawing
{
	using Cession.Geometries;
	using MonoTouch.CoreGraphics;

	public static class MatrixExtension
	{
		public static CGAffineTransform ToCGAffineTransform(this Matrix matrix)
		{
			return new CGAffineTransform((float)matrix.M11,
				(float)matrix.M12,
				(float)matrix.M21,
				(float)matrix.M22,
				(float)matrix.OffsetX,
				(float)matrix.OffsetY);
		}
	}
}

