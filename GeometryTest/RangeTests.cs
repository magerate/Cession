using System;
using NUnit.Framework;
using Cession.Geometries;

namespace GeometryTest
{
	[TestFixture]
	public class RangeTests
	{
		[Test]
		public void ContainsTest1(){
			Assert.True (Range.Contains (1, 2, 1.5));
		}

		[Test]
		public void ContainsTest2(){
			Assert.True(Range.Contains(1,double.PositiveInfinity,double.MaxValue));
		}

		[Test]
		[ExpectedException(typeof(ArgumentException))]
		public void ContainsTest3(){
			Assert.True(Range.Contains(1,double.NaN,double.MaxValue));
		}

		[Test]
		public void ContainsTest4(){
			Action test = () => {
				double value = TestHelper.NextDouble(double.MaxValue);
				Assert.True (Range.Contains (double.NegativeInfinity, double.PositiveInfinity, value));
			};

			test.RunBatch (1000000);
		}
	}
}

