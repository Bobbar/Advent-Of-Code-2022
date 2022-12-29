namespace Day_5___Supply_Stacks
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

			var stacks = ParseInitialStacks(inputText);

			foreach (var line in inputText)
			{
				if (line.Contains("move") == false)
					continue;

				var info = line.Replace("move", "").Replace("from", "").Replace("to", "").Trim().Split(' ', StringSplitOptions.RemoveEmptyEntries);

				var count = int.Parse(info[0]);
				var source = int.Parse(info[1]) - 1;
				var dest = int.Parse(info[2]) - 1;

				DoMovePartOne(source, dest, count, ref stacks);
			}

			string message = string.Empty;
			foreach (var stack in stacks)
				message += stack.Last();

			Console.WriteLine($"Part One Message: {message}");
		}

		static void PartTwo()
		{
			var inputPath = $@"{Environment.CurrentDirectory}\input.txt";
			var inputText = File.ReadAllLines(inputPath).ToList();

			var stacks = ParseInitialStacks(inputText);

			foreach (var line in inputText)
			{
				if (line.Contains("move") == false)
					continue;

				var info = line.Replace("move", "").Replace("from", "").Replace("to", "").Trim().Split(' ', StringSplitOptions.RemoveEmptyEntries);

				var count = int.Parse(info[0]);
				var source = int.Parse(info[1]) - 1;
				var dest = int.Parse(info[2]) - 1;

				DoMovePartTwo(source, dest, count, ref stacks);
			}

			string message = string.Empty;
			foreach (var stack in stacks)
				message += stack.Last();

			Console.WriteLine($"Part Two Message: {message}");
		}

		static void DoMovePartOne(int source, int dest, int count, ref List<char>[] stacks)
		{
			for (int i = 0; i < count; i++)
			{
				stacks[dest].Add(stacks[source].Last());
				stacks[source].RemoveAt(stacks[source].Count - 1);
			}
		}

		static void DoMovePartTwo(int source, int dest, int count, ref List<char>[] stacks)
		{
			int sourceStart = stacks[source].Count - count;

			stacks[dest].AddRange(stacks[source].GetRange(sourceStart, count));
			stacks[source].RemoveRange(sourceStart, count);
		}

		static List<char>[] ParseInitialStacks(List<string> lines)
		{
			string first = string.Empty;
			int rows = 0;
			string[] colStrings = new string[0];

			while (first != "1" && rows < lines.Count)
			{
				rows++;

				var line = lines[rows].Split(' ', StringSplitOptions.RemoveEmptyEntries);

				if (line.Length > 0)
				{
					first = line[0];
					colStrings = line;
				}
			}

			var cols = int.Parse(colStrings.Last());
			var stacks = new List<char>[cols];

			for (int r = rows - 1; r >= 0; r--)
			{
				var row = lines[r];
				int pos = 1;

				for (int c = 0; c < cols; c++)
				{
					var col = row.Substring(pos, 1).Trim();

					if (stacks[c] == null)
						stacks[c] = new List<char>();

					if (string.IsNullOrEmpty(col) == false)
						stacks[c].Add(col[0]);

					pos += 4;
				}
			}

			return stacks;
		}
	}
}