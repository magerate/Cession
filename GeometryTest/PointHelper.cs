using System;
using Cession.Geometries;
using NUnit.Framework;

namespace GeometryTest
{
    public static class PointHelper
    {
        public static Point CreateRandomIntPoint()
        {
            int range = 1000000;
            return CreateRandomIntPoint (-range, range);
        }

        public static Point CreateRandomIntPoint(int minValue,int maxValue)
        {
            int x = RandomHelper.Next (minValue, maxValue);
            int y = RandomHelper.Next (minValue, maxValue);
            return new Point (x, y);
        }

        public static void AreEqual (Point p1, Point p2)
        {
            Assert.AreEqual (p1.X, p2.X);
            Assert.AreEqual (p1.Y, p2.Y);
        }
    }
}

