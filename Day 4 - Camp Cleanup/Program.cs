namespace Day_4___Camp_Cleanup
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
			var inputText = File.ReadAllLines(inputPath).ToList();
			var pairs = new List<Tuple<Range, Range>>();

			foreach (var line in inputText)
			{
				var splitPairs = line.Split(',');

				var first = GetRange(splitPairs[0]);
				var second = GetRange(splitPairs[1]);

				pairs.Add(new Tuple<Range, Range>(first, second));
			}


			int nContain = 0;
			foreach (var pair in pairs)
			{
				if (pair.Item1.Contains(pair.Item2))
				{
					nContain++;
				}
			}

			Console.WriteLine($"Fully Contained Pairs: {nContain}");

		}


		static void PartTwo()
		{
			var inputPath = $@"{Environment.CurrentDirectory}\input.txt";
			var inputText = File.ReadAllLines(inputPath).ToList();
			var pairs = new List<Tuple<Range, Range>>();

			foreach (var line in inputText)
			{
				var splitPairs = line.Split(',');

				var first = GetRange(splitPairs[0]);
				var second = GetRange(splitPairs[1]);

				pairs.Add(new Tuple<Range, Range>(first, second));
			}


			int overlaps = 0;
			foreach (var pair in pairs)
			{
				if (pair.Item1.Overlaps(pair.Item2) || pair.Item2.Overlaps(pair.Item1))
				{
					overlaps++;
				}
			}

			Console.WriteLine($"Overlapping Pairs: {overlaps}");

		}


		static Range GetRange(string range)
		{
			var split = range.Split('-');

			var start = int.Parse(split[0]);
			var end = int.Parse(split[1]);

			return new Range(start, end);
		}
	}
}