using NUnit.Framework;
using System;
using Cession.Geometries;

namespace GeometryTest
{
	[TestFixture ()]
	public class LineTest
	{
		[Test ()]
		public void ContainsTest1 ()
		{
			var p1 = new Point (1, 1);
			var p2 = new Point (2, 2);
			var p3 = new Point (3, 3);

			Assert.True (Line.Contains (p1, p2, p3));
		}

		[Test ()]
		public void ContainsTest2 ()
		{
			var p1 = new Point (1, 1);
			var p2 = new Point (2, 1);
			var p3 = new Point (3, 1);

			Assert.True (Line.Contains (p1, p2, p3));
		}

		[Test ()]
		public void LineDistanceBetweenTest1 ()
		{
			var p1 = new Point (1, 1);
			var p2 = new Point (2, 1);

			var p3 = new Point (1, 0);

			var distance = Line.DistanceBetween (p1, p2, p3);

			Assert.AreEqual (distance,1);
		}

		[Test ()]
		public void LineDistanceBetweenTest2 ()
		{
			var p1 = new Point (1, 1);
			var p2 = new Point (2, 2);

			var p3 = new Point (3, 3);

			var distance = Line.DistanceBetween (p1, p2, p3);

			Assert.AreEqual (distance,0);
		}

		[Test ()]
		public void LineDistanceBetweenTest3 ()
		{
			var p1 = new Point (1, 1);
			var p2 = new Point (1, 1);

			var p3 = new Point (3, 3);

			var distance = Line.DistanceBetween (p1, p2, p3);

			Assert.AreEqual (distance,p1.DistanceBetween(p3));
		}

		[Test ()]
		public void LineDistanceBetweenTest4 ()
		{
			Action test = () => {
				var p1 = TestHelper.CreateRandomPoint();
				var p2 = TestHelper.CreateRandomPoint();
				var p3 = TestHelper.CreateRandomPoint();

				var d1 = Line.DistanceBetween(p1,p2,p3);
				var d2 = Line.DistanceBetween(p2,p1,p3);

				TestHelper.AlmostEqual(d1,-d2);
			};

			test.RunBatch ();
		}



		[Test ()]
		public void LineIntersectTest1(){
			var p1 = new Point (1, 1);
			var p2 = new Point (2, 2);

			var p3 = new Point (1, 0);
			var p4 = new Point (1, 10);

			var cross = Line.Intersect (p1, p2, p3, p4);
			Assert.True (cross.HasValue);
			Assert.AreEqual (cross.Value.X, 1);
			Assert.AreEqual (cross.Value.Y, 1);
		}

		[Test ()]
		public void LineIntersectTest2(){

			Func<Point> pg = () => TestHelper.CreateRandomPoint(100000);

			Action test = () => {
				var p1 = pg ();
				var p2 = pg ();
				var p3 = pg ();
				var p4 = pg ();

				var cross = Line.Intersect (p1, p2, p3, p4);
				if (cross.HasValue) {
					Assert.LessOrEqual (Line.DistanceBetween (p1, p2, cross.Value), 1e-5);
					Assert.LessOrEqual (Line.DistanceBetween (p3, p4, cross.Value), 1e-5);

					Assert.True(LineHelper.AlmostContains(p1,p2,cross.Value));
					Assert.True(LineHelper.AlmostContains(p3,p4,cross.Value));
				}
			};

			test.RunBatch ();
		}

	}
}

