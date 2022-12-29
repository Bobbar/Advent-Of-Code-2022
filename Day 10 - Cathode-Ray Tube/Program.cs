using System.Diagnostics;

namespace Day_10___Cathode_Ray_Tube
{
	internal class Program
	{
		private static int lastCheck = 0;

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

			int x = 1;
			int cycle = 0;
			int sum = 0;
			foreach (var line in inputText)
			{
				var cmd = line.Split(' ');
				var op = cmd[0];
				int val = 0;

				if (cmd.Length > 1)
					val = int.Parse(cmd[1]);

				int cycles = 0;

				switch (op)
				{
					case "noop":
						cycles = 1;
						break;
					case "addx":
						cycles = 2;
						break;
				}

				int opCycles = cycle + cycles;
				while (cycle < opCycles)
				{
					cycle++;

					var sig = GetSignal(cycle, x);

					if (sig > 0)
					{
						Debug.WriteLine($"Cycle: {cycle}  Sig: {sig}");
						sum += sig;
					}
				}

				x += val;
			}

			Console.WriteLine($"Part One Sum: {sum}");
		}


		static void PartTwo()
		{
			var inputPath = $@"{Environment.CurrentDirectory}\input.txt";
			var inputText = File.ReadAllLines(inputPath).ToList();

			int x = 1;
			int cycle = 0;
			int crtPos = 0;
			var crtRow = new bool[40];
			var crtRows = new List<bool[]>();

			foreach (var line in inputText)
			{
				var cmd = line.Split(' ');
				var op = cmd[0];
				int val = 0;

				if (cmd.Length > 1)
					val = int.Parse(cmd[1]);

				int cycles = 0;

				switch (op)
				{
					case "noop":
						cycles = 1;
						break;
					case "addx":
						cycles = 2;
						break;
				}

				int opCycles = cycle + cycles;
				while (cycle < opCycles)
				{
					cycle++;

					if (crtPos >= crtRow.Length)
					{
						crtRows.Add(crtRow);
						crtRow = new bool[40];
						crtPos = 0;
					}

					if (crtPos >= x - 1 && crtPos <= x + 1)
						crtRow[crtPos] = true;

					crtPos++;
				}

				x += val;
			}

			crtRows.Add(crtRow);

			foreach (var row in crtRows)
				DrawRow(row);
		}

		static void DrawRow(bool[] row)
		{
			foreach(var r in row)
			{
				if (r)
					Console.Write("#");
				else
					Console.Write(".");
			}

			Console.WriteLine("");
		}


		static int GetSignal(int cycle, int x)
		{
			int signal = 0;

			if (cycle == 20)
			{
				signal = cycle * x;
				lastCheck = cycle;
			}

			if (cycle == lastCheck + 40)
			{
				signal = cycle * x;
				lastCheck = cycle;
			}

			return signal;
		}

		static void PrintSignal(int cycle, int x)
		{
			if (cycle == 20)
			{
				int signal = cycle * x;
				Debug.WriteLine($"Cycle: {cycle}  Sig: {signal}");
				lastCheck = cycle;
			}

			if (cycle == lastCheck + 40)
			{
				int signal = cycle * x;
				Debug.WriteLine($"Cycle: {cycle}  Sig: {signal}");
				lastCheck = cycle;
			}

		}
	}
}