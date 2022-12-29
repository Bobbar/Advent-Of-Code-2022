namespace AdventOfCodeTemplate1
{
	internal class Program
	{
		static void Main(string[] args)
		{
			var inputPath = $@"{Environment.CurrentDirectory}\input.txt";
			var inputText = File.ReadAllLines(inputPath).ToList();

			Console.ReadKey();
		}
	}
}