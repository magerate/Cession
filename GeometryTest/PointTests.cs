using NUnit.Framework;
using System;
using Cession.Geometries;

namespace GeometryTest
{
	[TestFixture]
	public class PointTests
	{
		public PointTests ()
		{
		}

		[Test ()]
		public void DistanceBetweenTest1 ()
		{
			var p1 = new Point (0, 0);
			var p2 = new Point (3, 4);

			var distance = p1.DistanceBetween (p2);
			Assert.AreEqual (distance, 5);
		}

		[Test()]
		public void DistanceBetweenTest2(){
			var p1 = new Point (double.MaxValue, double.MaxValue);
			var p2 = new Point (double.MinValue, double.MinValue);

			var d1 = p1.DistanceBetween (p2);
			var d2 = p2.DistanceBetween (p1);
			Assert.AreEqual (double.PositiveInfinity,d1);
			Assert.AreEqual (double.PositiveInfinity,d2);
		}

		[Test()]
		public void DistanceBetweenTest3(){
			var p1 = new Point (double.MaxValue, double.MaxValue);
			var p2 = new Point (double.MinValue, double.MinValue);

			var d1 = p1.DistanceBetween (p2);
			var d2 = p2.DistanceBetween (p1);
			Assert.AreEqual (double.PositiveInfinity,d1);
			Assert.AreEqual (double.PositiveInfinity,d2);
		}

		[Test()]
		public void RotateTest1(){
			var p1 = new Point (1, 0);
			var p2 = new Point (0, 0);
			p1.Rotate (p2, Math.PI / 2);
			TestHelper.AlmostEqual (p1.X, 0);
			TestHelper.AlmostEqual (p1.Y, 1);
		}

		[Test()]
		public void RotateTest2(){

			Action test = () => {
				var p1 = TestHelper.CreateRandomPoint ();
				var p2 = TestHelper.CreateRandomPoint ();
				var angle = TestHelper.NextDouble (Math.PI * 2);

				var x = p1.X;
				var y = p1.Y;

				p1.Rotate (p2, angle);
				p1.Rotate (p2, -angle);

				TestHelper.AlmostEqual (p1.X, x);
				TestHelper.AlmostEqual (p1.Y, y);
			};

			test.RunBatch ();
		}


		
		[Test ()]
		public void ProjectTest1(){
			var p1 = new Point (1, 1);
			var p2 = new Point (2, 1);

			var p3 = new Point (100, 0);

			var pp = p3.Project (p1, p2);

			Assert.AreEqual (pp.X, 100);
			Assert.AreEqual (pp.Y, 1);
		}
		
		[Test ()]
		public void ProjectTest2(){
			Func<Point> pg = () => TestHelper.CreateRandomPoint (100000);

			Action test = () => {
				var p1 = pg();
				var p2 = pg();
				var p3 = pg();
				var pp = p3.Project(p1,p2);

				var d1 = pp.DistanceBetween(p3);
				var d2 = Math.Abs(Line.DistanceBetween(p1,p2,p3));

				TestHelper.AlmostEqual(d1,d2);

				var d3 = Math.Abs(Line.DistanceBetween(p1,p2,pp));
				TestHelper.AlmostZero(d3);
			};

			test.RunBatch ();
		} 
		
		
	}
}

