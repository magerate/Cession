using System;
using NUnit.Framework;
using Cession.Geometries;

namespace GeometryTest
{
	[TestFixture]
	public class SegmentTests
	{
		[Test]
		public void IntersectTest1(){

			Action test = () => {
				var p1 = TestHelper.CreateRandomPoint();
				var p2 = TestHelper.CreateRandomPoint();

				var p3 = TestHelper.CreateRandomPoint();
				var p4 = TestHelper.CreateRandomPoint();

				var cp = Segment.Intersect(p1,p2,p3,p4);
				if(cp.HasValue){
					LineHelper.AlmostContains(p1,p2,cp.Value);
					LineHelper.AlmostContains(p3,p4,cp.Value);

					Assert.GreaterOrEqual(cp.Value.X,Math.Min(p1.X,p2.X));
					Assert.LessOrEqual(cp.Value.X,Math.Max(p1.X,p2.X));

					Assert.GreaterOrEqual(cp.Value.Y,Math.Min(p1.Y,p2.Y));
					Assert.LessOrEqual(cp.Value.Y,Math.Max(p1.Y,p2.Y));

					Assert.GreaterOrEqual(cp.Value.X,Math.Min(p3.X,p4.X));
					Assert.LessOrEqual(cp.Value.X,Math.Max(p3.X,p4.X));

					Assert.GreaterOrEqual(cp.Value.Y,Math.Min(p3.Y,p4.Y));
					Assert.LessOrEqual(cp.Value.Y,Math.Max(p3.Y,p4.Y));
				}
			};

			test.RunBatch (100000);
		}
	}
}

