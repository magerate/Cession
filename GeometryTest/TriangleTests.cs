using System;
using NUnit.Framework;
using Cession.Geometries;

namespace GeometryTest
{
    [TestFixture]
    public class TriangleTests
    {
        [Test]
        public void ContainsTest1()
        {
            var p1 = new Point (1, 1);
            var p2 = new Point (-1, 1);
            var p3 = new Point (0, -1);

            var p4 = new Point (0.5, 0.5);

            Assert.True (Triangle.Contains (p1, p2, p3, p4));
        }

        [Test]
        public void ContainTest2()
        {
        }

        [Test]
        public void GetSignedAreaTest1()
        {
            var p1 = new Point (0, 0);
            var p2 = new Point(1,0);
            var p3 = new Point (1, 1);

            double area = Triangle.GetSignedArea (p1, p2, p3);
            Assert.AreEqual (0.5, area);
        }

        [Test]
        public void ClockwiseTest1(){
            var p1 = new Point (0, 0);
            var p2 = new Point (1, 0);
            var p3 = new Point (1, 1);

            Assert.True (Triangle.IsClockwise (p1, p2, p3));
            Assert.False (Triangle.IsClockwise (p3, p2, p1));
        }
    }
}

