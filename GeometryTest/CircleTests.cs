using System;
using NUnit.Framework;
using Cession.Geometries;

namespace GeometryTest
{
	[TestFixture]
	public class CircleTests
	{
		[Test]
		public void IntersectsTest1(){
			var p1 = new Point (0, 0);
			double r1 = 2;

			var p2 = new Point (4, 0);
			var r2 = 2;

			var pp = Circle.Intersects (p1, r1, p2, r2);
			Assert.NotNull (pp);
			TestHelper.PointAreEqual (pp.Item1, pp.Item2);

			Assert.AreEqual (pp.Item1.X, 2);
			Assert.AreEqual (pp.Item1.Y, 0);
		}

		[Test]
		public void IntersectsTest2(){
			var p1 = new Point (0, 0);
			double r1 = 2;

			var p2 = new Point (3, 0);
			var r2 = 2;

			var pp = Circle.Intersects (p1, r1, p2, r2);
			Assert.NotNull (pp);

			Assert.AreEqual (pp.Item1.X, 1.5);
			TestHelper.AlmostEqual (pp.Item1.Y, 1.323,1e-3);

			Assert.AreEqual (pp.Item2.X, 1.5);
			TestHelper.AlmostEqual (pp.Item2.Y, -1.323,1e-3);
		}

		[Test]
		public void IntersectsTest3(){
			var p1 = new Point (1, 1);
			double r1 = 5;

			var p2 = new Point (3, 2);
			var r2 = 3;

			var pp = Circle.Intersects (p1, r1, p2, r2);
			Assert.NotNull (pp);

			TestHelper.AlmostEqual (pp.Item1.X, 4.432,1e-3);
			TestHelper.AlmostEqual (pp.Item1.Y, 4.636,1e-3);

			TestHelper.AlmostEqual (pp.Item2.X, 5.968,1e-3);
			TestHelper.AlmostEqual (pp.Item2.Y, 1.564,1e-3);
		}

		[Test]
		public void IntersectsTest4(){

			Action test = () => {
				var p1 = TestHelper.CreateRandomPoint();
				var r1 = TestHelper.NextDouble();

				var p2 = TestHelper.CreateRandomPoint();
				var r2 = TestHelper.NextDouble();

				var pp = Circle.Intersects(p1,r1,p2,r2);
				if(null != pp){
					var d1 = pp.Item1.DistanceBetween(p1);
					TestHelper.AlmostEqual(d1,r1);

					var d2 = pp.Item1.DistanceBetween(p2);
					TestHelper.AlmostEqual(d2,r2);

					var d3 = pp.Item2.DistanceBetween(p1);
					TestHelper.AlmostEqual(d3,r1);

					var d4 = pp.Item2.DistanceBetween(p2);
					TestHelper.AlmostEqual(d4,r2);
				}
			};

			test.RunBatch (1000000);
		}

		[Test]
		public void IntersectsWithLineTest1(){
			var c1 = new Point (0, 0);
			var r1 = 1;

			var p1 = new Point (1, 1);
			var p2 = new Point (2, 2);

			var ip = Circle.IntersectsWithLine (c1, r1, p1, p2);
			Assert.NotNull (ip);


			TestHelper.AlmostEqual (Math.Cos(Math.PI/4), ip.Item1.X);
			TestHelper.AlmostEqual (Math.Sin(Math.PI/4), ip.Item1.Y);

			TestHelper.AlmostEqual (-Math.Cos(Math.PI/4), ip.Item2.X);
			TestHelper.AlmostEqual (-Math.Sin(Math.PI/4), ip.Item2.Y);
		}

		[Test]
		public void IntersectsWithLineTest2(){

			Action test = () => {
				var c1 = TestHelper.CreateRandomPoint();
				var r1 = TestHelper.NextDouble();

				var p1 = TestHelper.CreateRandomPoint();
				var p2 = TestHelper.CreateRandomPoint();

				var ip = Circle.IntersectsWithLine(c1,r1,p1,p2);
				if(null != ip){
					var d1 = ip.Item1.DistanceBetween(c1);
					TestHelper.AlmostEqual(d1,r1);

					var d2 = ip.Item2.DistanceBetween(c1);
					TestHelper.AlmostEqual(d2,r1);

					Assert.True(LineHelper.AlmostContains(p1,p2,ip.Item1));
					Assert.True(LineHelper.AlmostContains(p1,p2,ip.Item2));
				}
			};

			test.RunBatch (100000);
		}

        [Test]
        public void GetCenterTest1(){
            var p1 = new Point (-1, 0);
            var p2 = new Point (1, 0);
            var p3 = new Point (0, 1);

            var cp = Circle.GetCenter (p1, p2, p3);
            Assert.NotNull (cp);
            TestHelper.AlmostEqual (0, cp.Value.X);
            TestHelper.AlmostEqual(0, cp.Value.Y);
        }

        [Test]
        public void GetCenterTest2(){
            Action test = () => {
                var p1 = TestHelper.CreateRandomPoint();
                var p2 = TestHelper.CreateRandomPoint();
                var p3 = TestHelper.CreateRandomPoint();

                var cp = Circle.GetCenter(p1,p2,p3);

                if(cp != null){
                    var d1 = cp.Value.DistanceBetween(p1);
                    var d2 = cp.Value.DistanceBetween(p2);
                    var d3 = cp.Value.DistanceBetween(p3);

                    TestHelper.AlmostEqual(d1,d2);
                    TestHelper.AlmostEqual(d1,d3);
                    TestHelper.AlmostEqual(d2,d3);
                }
            };
            test.RunBatch (100000);
        }
	}
}

