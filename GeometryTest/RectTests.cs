using System;
using NUnit.Framework;
using Cession.Geometries;

namespace GeometryTest
{
	[TestFixture]
	public class RectTests
	{
		int batchCount = 10000;

		[Test]
		public void ContainsPointTest1(){
			var rect = new Rect (0, 0, 100, 100);
			var point = new Point (50, 50);
			Assert.True(rect.Contains(point));
		}

		[Test]
		public void ContainsPointTest2(){

			Action test = () => {
				var rect = new Rect (0, 0, TestHelper.NextDouble(double.MaxValue), TestHelper.NextDouble(double.MaxValue));
				var point = new Point (TestHelper.NextDouble(double.MaxValue), TestHelper.NextDouble(double.MaxValue));
				if(rect.Contains(point)){
					Assert.True(point.X >= rect.Left);
					Assert.True(point.X <= rect.Right);
					Assert.True(point.Y >= rect.Top);
					Assert.True(point.Y <= rect.Bottom);
				}
			};

			test.RunBatch (batchCount);
		}

		[Test]
		public void InflateTest1(){
			var rect = new Rect (0, 0, 100, 100);
			rect.Inflate (10, 20);

			Assert.AreEqual (-10, rect.X);
			Assert.AreEqual (-20, rect.Y);
			Assert.AreEqual (120, rect.Width);
			Assert.AreEqual (140, rect.Height);
		}

		[Test]
		public void InflateTest2(){
			Action test = () => {
				double x = TestHelper.NextDouble(double.MinValue + 100);
				double y = TestHelper.NextDouble(double.MinValue + 100);
				double width = TestHelper.NextDouble(double.MaxValue - 100);
				double height = TestHelper.NextDouble(double.MaxValue - 100);

				var rect = new Rect(x,y,width,height);

				double fWidth = TestHelper.NextDouble(100);
				double fHeight = TestHelper.NextDouble(100);

				rect.Inflate (fWidth, fHeight);

				Assert.AreEqual (x, rect.X + fWidth);
				Assert.AreEqual (y, rect.Y + fHeight);
				Assert.AreEqual (width, rect.Width - 2 * fWidth);
				Assert.AreEqual (height, rect.Height - 2 * fHeight);

				Assert.True(rect.Contains(new Rect(x,y,width,height)));
			};
			test.RunBatch (batchCount);
		}

		[Test]
		public void UnionTest1(){
			var rect1 = new Rect (0, 0, 100, 100);
			var rect2 = new Rect (50, 50, 100, 100);

			var rect3 = rect1.Union (rect2);

			Assert.AreEqual (0, rect3.X);
			Assert.AreEqual (0, rect3.Y);
			Assert.AreEqual (150, rect3.Width);
			Assert.AreEqual (150, rect3.Height);
		}

		[Test]
		public void UnionTest2(){
			double minValue = -100000;
			double maxValue = 100000;

			Action test = () => {
				var rect1 = new Rect (TestHelper.NextDouble(minValue), 
					TestHelper.NextDouble(minValue), 
					TestHelper.NextDouble(maxValue), 
					TestHelper.NextDouble(maxValue));

				var rect2 = new Rect (TestHelper.NextDouble(minValue), 
					TestHelper.NextDouble(minValue), 
					TestHelper.NextDouble(maxValue), 
					TestHelper.NextDouble(maxValue));


				var rect3 = rect1.Union (rect2);


				TestHelper.AlmostEqual(rect3.X,Math.Min(rect1.X,rect2.X));
				TestHelper.AlmostEqual(rect3.Y,Math.Min(rect1.Y,rect2.Y));
				TestHelper.AlmostEqual(rect3.Right,Math.Max(rect1.Right,rect2.Right));
				TestHelper.AlmostEqual(rect3.Bottom,Math.Max(rect1.Bottom,rect2.Bottom));

				Assert.LessOrEqual (rect3.Left,rect1.Left + 1e-5);
				Assert.GreaterOrEqual (rect3.Right + 1e-5,rect1.Right);
				Assert.LessOrEqual (rect3.Top,rect1.Top + 1e-5);
				Assert.GreaterOrEqual (rect3.Bottom + 1e-5,rect1.Bottom);

				Assert.LessOrEqual (rect3.Left,rect2.Left + 1e-5);
				Assert.GreaterOrEqual (rect3.Right + 1e-5,rect2.Right);
				Assert.LessOrEqual (rect3.Top,rect2.Top + 1e-5);
				Assert.GreaterOrEqual (rect3.Bottom + 1e-5,rect2.Bottom);
			};

			test.RunBatch (batchCount);
		}

		[Test]
		public void IntersectTest1(){
			var rect1 = new Rect (0, 0, 100, 100);
			var rect2 = new Rect (50, 50, 100, 100);

			var rect3 = rect1.Intersects (rect2).Value;

			Assert.AreEqual (50, rect3.X);
			Assert.AreEqual (50, rect3.Y);
			Assert.AreEqual (50, rect3.Width);
			Assert.AreEqual (50, rect3.Height);
		}

		[Test]
		public void IntersectTest2(){
			double minValue = -100000;
			double maxValue = 100000;

			Action test = () => {
				var rect1 = new Rect (TestHelper.NextDouble(minValue), 
					TestHelper.NextDouble(minValue), 
					TestHelper.NextDouble(maxValue), 
					TestHelper.NextDouble(maxValue));

				var rect2 = new Rect (TestHelper.NextDouble(minValue), 
					TestHelper.NextDouble(minValue), 
					TestHelper.NextDouble(maxValue), 
					TestHelper.NextDouble(maxValue));


				var nrect3 = rect1.Intersects (rect2);

				if(nrect3.HasValue){
					var rect3 = nrect3.Value;

					Assert.GreaterOrEqual (rect3.Left + 1e-5 ,rect1.Left);
					Assert.LessOrEqual (rect3.Right,rect1.Right + 1e-5);
					Assert.GreaterOrEqual (rect3.Top + 1e-5,rect1.Top);
					Assert.LessOrEqual (rect3.Bottom,rect1.Bottom + 1e-5);

					Assert.GreaterOrEqual (rect3.Left + 1e-5 ,rect2.Left);
					Assert.LessOrEqual (rect3.Right,rect2.Right + 1e-5);
					Assert.GreaterOrEqual (rect3.Top + 1e-5,rect2.Top);
					Assert.LessOrEqual (rect3.Bottom,rect2.Bottom + 1e-5);
				}
			};

			test.RunBatch (batchCount);
		}

	}
}

