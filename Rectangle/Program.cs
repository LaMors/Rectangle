using System;
using System.Linq;
using Rectangle.Impl;

namespace Rectangle
{
	class Program
	{
		/// <summary>
		/// Use this method for local debugging only. The implementation should remain in Rectangle.Impl project.
		/// </summary>
		/// <param name="args"></param>
		static void Main(string[] args)
		{
			var rectangle = Service.FindRectangle(new[] { new Point() }.ToList());

			Console.ReadKey();
		}
	}
}
