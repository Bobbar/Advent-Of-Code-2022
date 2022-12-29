namespace Day_3___Rucksack_Reorganization
{
	internal class Program
	{
		private const int LCaseOffset = 96;
		private const int UCaseOffset = 38;

		static void Main(string[] args)
		{
			PartOne();

			PartTwo();

			Console.ReadKey();
		}

		static void PartOne()
		{
			var inputPath = $@"{Environment.CurrentDirectory}\input.txt";
			var inputText = File.ReadAllLines(inputPath).ToList();

			int sum = 0;

			foreach (var line in inputText)
			{
				var len = line.Length;
				var halfLen = len / 2;

				var comp1 = line.Substring(0, halfLen);
				var comp2 = line.Substring(halfLen, halfLen);

				var inBoth = comp1.Where(c => comp2.Contains(c)).ToList();


				sum += Priority(inBoth[0]);
			}

			Console.WriteLine($"Final Sum Part One: {sum}");
		}

		static void PartTwo()
		{
			var inputPath = $@"{Environment.CurrentDirectory}\input.txt";
			var inputText = File.ReadAllLines(inputPath).ToList();

			int sum = 0;

			for (int i = 0; i < inputText.Count; i += 3)
			{		
				var first = inputText[i];
				var inAll = first.ToList();

				for (int j = i + 1; j < i + 3; j++)
				{
					var line = inputText[j];
					inAll = line.Where(c => inAll.Contains(c)).Distinct().ToList();
				}

				var badge = inAll.First();

				sum += Priority(badge);
			}

			Console.WriteLine($"Final Sum Part Two: {sum}");
		}

		static int Priority(char c)
		{
			if (char.IsUpper(c))
				return (int)c - UCaseOffset;
			else
				return (int)c - LCaseOffset;
		}
	}
}