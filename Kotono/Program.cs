using Kotono.Assistant;
using System.Threading.Tasks;

namespace Kotono
{
	public class Program
	{
		public static async Task Main(string[] args)
		{
			//Console.WriteLine("Compilation Completed !");

			using (var partner = new Partner())
			{
				await Partner.Run();
			}
		}
	}
}