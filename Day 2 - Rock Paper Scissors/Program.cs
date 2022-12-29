namespace Day_2___Rock_Paper_Scissors
{
	internal class Program
	{
		private static Dictionary<string, Shapes> shapeDict = new Dictionary<string, Shapes>() 
		{ 
			{ "A", Shapes.Rock }, 
			{ "B", Shapes.Paper }, 
			{ "C", Shapes.Scissors }, 
			{ "X", Shapes.Rock }, 
			{ "Y", Shapes.Paper }, 
			{ "Z", Shapes.Scissors } 
		};

		private static Dictionary<string, Outcome> outcomeDict = new Dictionary<string, Outcome>()
		{
			{ "X", Outcome.Lose },
			{ "Y", Outcome.Draw },
			{ "Z", Outcome.Win }
		};

		private static Dictionary<Shapes, int> shapeScore = new Dictionary<Shapes, int>()
		{
			{ Shapes.Rock, 1 },
			{ Shapes.Paper, 2 },
			{ Shapes.Scissors, 3 }
		};

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

			int total = 0;

			foreach (var line in inputText)
			{
				var split = line.Split(' ');

				var plr1 = shapeDict[split[1]];
				var plr2 = shapeDict[split[0]];

				var result = Result(plr1, plr2);

				result += shapeScore[plr1];

				total += result;
			}

			Console.WriteLine($"Final Score Part One: {total}");
		}


		static void PartTwo()
		{
			var inputPath = $@"{Environment.CurrentDirectory}\input.txt";
			var inputText = File.ReadAllLines(inputPath).ToList();

			int total = 0;

			foreach (var line in inputText)
			{
				var split = line.Split(' ');

				var plr1 = shapeDict[split[0]];
				var outcome = outcomeDict[split[1]];
				var plr2 = GetShapePerOutcome(plr1, outcome);
				var result = Result(plr2, plr1);

				result += shapeScore[plr2];
				total += result;
			}

			Console.WriteLine($"Final Score Part Two: {total}");
		}

		static Shapes GetShapePerOutcome(Shapes a, Outcome outcome)
		{
			if (outcome == Outcome.Draw)
				return a;

			switch (a)
			{
				case Shapes.Rock:

					switch (outcome)
					{
						case Outcome.Win:
							return Shapes.Paper;

						case Outcome.Lose:
							return Shapes.Scissors;
					}

					break;

				case Shapes.Paper:

					switch (outcome)
					{
						case Outcome.Win:
							return Shapes.Scissors;

						case Outcome.Lose:
							return Shapes.Rock;
					}

					break;

				case Shapes.Scissors:

					switch (outcome)
					{
						case Outcome.Win:
							return Shapes.Rock;

						case Outcome.Lose:
							return Shapes.Paper;
					}

					break;
			}

			return a;
		}

		static int Result(Shapes a, Shapes b)
		{
			if (a == b) 
				return 3;

			switch (a)
			{
				case Shapes.Rock:

					if (b == Shapes.Scissors)
						return 6;

					break;

				case Shapes.Paper:

					if (b == Shapes.Rock)
						return 6;

					break;

				case Shapes.Scissors:

					if (b == Shapes.Paper)
						return 6;

					break;
			}

			return 0;
		}

		public enum Shapes
		{
			Rock,
			Paper,
			Scissors
		}

		public enum Outcome
		{
			Lose,
			Draw,
			Win
		}
	}
}