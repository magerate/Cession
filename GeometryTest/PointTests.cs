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

        [Test ()]
        public void DistanceBetweenTest2 ()
        {
            var p1 = new Point (int.MaxValue, int.MaxValue);
            var p2 = new Point (int.MinValue, int.MinValue);

            var d1 = p1.DistanceBetween (p2);
            var d2 = p2.DistanceBetween (p1);

            Assert.AreEqual (6074000998.5378857d, d1);
            Assert.AreEqual (6074000998.5378857d, d2);
        }

        [Test ()]
        public void RotateTest1 ()
        {
            var p1 = new Point (1, 0);
            var p2 = new Point (0, 0);
            p1.Rotate (p2, Math.PI / 2);

            Assert.AreEqual (0, p1.X);
            Assert.AreEqual (1, p1.Y);
        }

        [Test ()]
        public void RotateTest2 ()
        {
            Action test = () =>
            {
                var p1 = PointHelper.CreateRandomIntPoint ();
                var p2 = PointHelper.CreateRandomIntPoint ();
                var angle = RandomHelper.NextDouble (Math.PI * 2);

                var x = p1.X;
                var y = p1.Y;

                p1.Rotate (p2, angle);
                p1.Rotate (p2, -angle);

                TestHelper.AlmostEqual(p1.X,x,1);
                TestHelper.AlmostEqual(p1.Y,y,1);
            };

            test.RunBatch ();
        }

        [Test ()]
        public void ProjectTest1 ()
        {
            var p1 = new Point (1, 1);
            var p2 = new Point (2, 1);

            var p3 = new Point (100, 0);

            var pp = p3.Project (p1, p2);

            Assert.AreEqual (pp.X, 100);
            Assert.AreEqual (pp.Y, 1);
        }

        [Test ()]
        public void ProjectTest2 ()
        {
            Func<Point> pg = PointHelper.CreateRandomIntPoint;

            Action test = () =>
            {
                var p1 = pg ();
                var p2 = pg ();
                var p3 = pg ();
                var pp = p3.Project (p1, p2);

                var d1 = pp.DistanceBetween (p3);
                var d2 = Math.Abs (Line.DistanceBetween (p1, p2, p3));

                Assert.LessOrEqual (Math.Abs (d1 - d2), 1);

                var d3 = Math.Abs (Line.DistanceBetween (p1, p2, pp));
                Assert.LessOrEqual (d3, 1);
            };

            test.RunBatch ();
        }
    }
}

