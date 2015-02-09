using System;
using NUnit.Framework;
using Cession.Geometries;

namespace GeometryTest
{
	public static class TestHelper
	{
		private static Random random = new Random();

		public static double NextDouble(double maxValue){
			return random.NextDouble () * maxValue;
		}

		public static Point CreateRandomPoint(){
			var x = random.NextDouble ();
			var y = random.NextDouble ();
			return new Point (x, y);
		}
			
		public static Point CreateRandomPoint(double maxValue){
			var x = random.NextDouble () * maxValue;
			var y = random.NextDouble () * maxValue;
			return new Point (x, y);
		}

		public static void AlmostEqual(double left,double right){
			Assert.LessOrEqual(Math.Abs (left - right),1e-5);
		}

		public static void AlmostZero(double value){
			Assert.LessOrEqual(Math.Abs (value),1e-5);
		}

		public static void RunBatch(this Action action,int count = 10000){
			for (int i = 0; i < count; i++) {
				action ();
			}
		}

	}
}

