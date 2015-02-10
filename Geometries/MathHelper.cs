using System;

namespace Cession.Geometries
{
	internal static class MathHelper
	{
		public static bool AlmostEquals(double left,double right,double delta = 1e-5){
			return Math.Abs (left - right) <= delta;
		}
	}
}

