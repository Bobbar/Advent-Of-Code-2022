namespace Day_1___Calorie_Counting
{
	internal class Program
	{
		static void Main(string[] args)
		{
			var inputPath = $@"{Environment.CurrentDirectory}\input.txt";

			var inputText = File.ReadAllLines(inputPath).ToList();

			PartOne(inputText);
			PartTwo(inputText);

			Console.ReadKey();
		}


		static void PartOne(List<string> values)
		{
			int max = int.MinValue;
			int cur = 0;
			foreach (var line in values)
			{
				if (string.IsNullOrEmpty(line))
				{
					max = Math.Max(max, cur);
					cur = 0;
				}
				else
				{
					var val = int.Parse(line);
					cur += val;
				}
			}

			Console.WriteLine($"Max calories: {max}");
		}

		static void PartTwo(List<string> values)
		{
			var totals = new List<int>();
			int cur = 0;
			foreach (var line in values)
			{
				if (string.IsNullOrEmpty(line))
				{
					totals.Add(cur);
					cur = 0;
				}
				else
				{
					var val = int.Parse(line);
					cur += val;
				}
			}

			totals = totals.OrderByDescending(x => x).ToList();

			var top3 = totals.Take(3).Sum();

			Console.WriteLine($"Top 3 calories total: {top3}");
		}
		
	}
}