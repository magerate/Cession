using System;
using NUnit.Framework;
using Cession.Geometries;

namespace GeometryTest
{
    public static class TestHelper
    {
        public static void AlmostEqual (double left, double right)
        {
            AlmostEqual (left, right, 1e-5);
        }

        public static void AlmostEqual (double left, double right, double delta)
        {
            Assert.LessOrEqual (Math.Abs (left - right), delta);
        }

        public static void AlmostEqual(int left,int right,int delta)
        {
            Assert.LessOrEqual (Math.Abs (left - right), delta);
        }

        public static void AlmostZero (double value)
        {
            Assert.LessOrEqual (Math.Abs (value), 1e-5);
        }

        public static void RunBatch (this Action action, int count = 100000)
        {
            for (int i = 0; i < count; i++)
            {
                action ();
            }
        }



    }
}

