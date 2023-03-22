using Kotono.Assistant;
using System;

namespace Kotono
{
	public class Program
	{
		public static void Main(string[] args)
        {
			//Console.WriteLine("Compilation Completed !");

			using (var partner = new Partner())
			{
				partner.Run();
			}
		}
	}
}