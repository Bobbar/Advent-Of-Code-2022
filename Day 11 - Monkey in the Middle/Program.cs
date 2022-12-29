using System.Linq.Expressions;
using System.Linq.Dynamic;
using System.Linq.Dynamic.Core;
using System.Numerics;
using System.Diagnostics;

namespace Day_11___Monkey_in_the_Middle
{
	internal class Program
	{
		static void Main(string[] args)
		{
			DoKeepAway(1);
			DoKeepAway(2);

			Console.ReadKey();
		}

		static void DoKeepAway(int part)
		{
			var inputPath = $@"{Environment.CurrentDirectory}\input.txt";
			var inputText = File.ReadAllLines(inputPath).ToList();

			var old = Expression.Parameter(typeof(long), "old");
			var monkeys = new List<Monkey>();

			for (int i = 0; i < inputText.Count; i++)
			{
				var m = new Monkey();

				i++; // Skip Monkey ID

				// Parse start items.
				var itemSplit = inputText[i].Trim().Split(":");
				var items = itemSplit[1].Trim().Split(',');
				foreach (var item in items)
					m.Items.Add(int.Parse(item));

				i++; // Move to operation.

				// Parse operation.
				var opSplit = inputText[i].Trim().Split(':');
				var expression = opSplit[1].Trim().Replace("new = ", "");
				var exp = DynamicExpressionParser.ParseLambda(new ParameterExpression[] { old }, typeof(long), expression);
				m.Operation = exp.Compile();

				i++; // Move to Test.

				// Parse test.
				var divStr = inputText[i].Trim().Replace("Test: divisible by ", "");
				m.Divisor = int.Parse(divStr);

				i++; // Move to true part.

				var truePartStr = inputText[i].Trim().Replace("If true: throw to monkey ", "");
				m.TrueTarget = int.Parse(truePartStr);

				i++; // Move to false part.

				var falsePartStr = inputText[i].Trim().Replace("If false: throw to monkey ", "");
				m.FalseTarget = int.Parse(falsePartStr);

				i++;

				monkeys.Add(m);
			}

			int rounds = 20;

			if (part == 2)
				rounds = 10000;

			// For part two.
			int modulo = 1;
			monkeys.ForEach(m => modulo *= m.Divisor);

			for (int round = 0; round < rounds; round++)
			{
				for (int m = 0; m < monkeys.Count; m++)
				{
					var monk = monkeys[m];

					for (int i = 0; i < monk.Items.Count; i++)
					{
						var item = monk.Items[i];
						var worry = (long)monk.Operation.DynamicInvoke(item);

						if (part == 1)
							worry = worry / 3;
						else if (part == 2)
							worry = worry % modulo;

						monkeys[m].Inspected++;

						if (worry % monk.Divisor == 0)
							monkeys[monk.TrueTarget].Items.Add(worry);
						else
							monkeys[monk.FalseTarget].Items.Add(worry);

					}

					monkeys[m].Items.Clear();
				}
			}

			var sorted = monkeys.OrderByDescending(m => m.Inspected);
			var topTwo = sorted.Take(2);
			var monkeyBusiness = topTwo.First().Inspected * topTwo.Last().Inspected;

			Console.WriteLine($"Part {part} Monkey Business: {monkeyBusiness}");
		}
	}

	public class Monkey
	{
		public List<long> Items { get; set; } = new List<long>();
		public Delegate Operation { get; set; }
		public int Divisor { get; set; }
		public int TrueTarget { get; set; }
		public int FalseTarget { get; set; }
		public long Inspected { get; set; }
	}
}

