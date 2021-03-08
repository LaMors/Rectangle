using System.Linq;
using NUnit.Framework;
using Rectangle.Impl;

namespace Rectangle.Tests
{
	public class Tests
	{
		[SetUp]
		public void Setup()
		{
		}

		[Test]
		public void Test1()
		{
			var rectangle = Service.FindRectangle(new[] { new Point(0, 0),
				new Point(1, 1),
				new Point(2, 1),
				new Point(1, 2),
				new Point(2, 2),}.ToList());
			Assert.IsNotNull(rectangle);
		}

	}
}