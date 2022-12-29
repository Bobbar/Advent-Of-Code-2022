using System.Net.Http.Headers;

namespace Day_6___Tuning_Trouble
{
	internal class Program
	{
		static void Main(string[] args)
		{
			PartOne();

			PartTwo();

			Console.ReadKey();
		}

		static void PartOne()
		{
			var inputPath = $@"{Environment.CurrentDirectory}\input.txt";
			var inputText = File.ReadAllText(inputPath);

			int firstStart = 0;

			for (int i = 0; i < inputText.Length; i++)
			{
				var seq = inputText.Substring(i, 4);
				var distinct = seq.Distinct();

				if (distinct.Count() == 4)
				{
					firstStart = i + 4;
					break;
				}
			}

			Console.WriteLine($"Part One First Start: {firstStart}");
		}

		static void PartTwo()
		{
			var inputPath = $@"{Environment.CurrentDirectory}\input.txt";
			var inputText = File.ReadAllText(inputPath);

			int firstStart = 0;

			for (int i = 0; i < inputText.Length; i++)
			{
				var seq = inputText.Substring(i, 14);
				var distinct = seq.Distinct();

				if (distinct.Count() == 14)
				{
					firstStart = i + 14;
					break;
				}
			}

			Console.WriteLine($"Part Two First Start: {firstStart}");
		}
	}
}