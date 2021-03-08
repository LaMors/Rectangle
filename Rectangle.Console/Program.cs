using System;
using System.Linq;
using Rectangle.Impl;

namespace Rectangle.Console
{
	class Program
	{
		/// <summary>
		/// Use this method for local debugging only. The implementation should remain in Rectangle.Impl project.
		/// See TODO.txt file for task details.
		/// </summary>
		/// <param name="args"></param>
		static void Main(string[] args)
		{

			var rectangle = Service.FindRectangle(new[] {
				new Point(0, 0),
				new Point(1, 1),
				new Point(2, 1),
				new Point(1, 2),
				new Point(2, 2),
				 }.ToList());

			System.Console.ReadKey();
		}
	}
}
