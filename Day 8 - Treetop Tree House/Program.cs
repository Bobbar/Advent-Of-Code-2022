using System.Diagnostics;

namespace Day_8___Treetop_Tree_House
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

			int cols = inputText.First().Length;
			int rows = inputText.Count();

			int[,] map = new int[cols, rows];
			bool[,] isVisible = new bool[cols, rows];

			for (int r = 0; r < rows; r++)
			{
				for (int c = 0; c < cols; c++)
				{
					map[c, r] = int.Parse(inputText[r][c].ToString());
				}
			}

			for (int r = 1; r < rows - 1; r++)
			{
				for (int c = 1; c < cols - 1; c++)
				{
					for (int d = 0; d < 4; d++)
					{
						var isVis = IsVisible(map, r, c, d);

						if (isVis)
						{
							isVisible[r, c] = true;
						}
					}
				}
			}

			int vis = 0;
			for (int r = 0; r < rows; r++)
				for (int c = 0; c < cols; c++)
					if (isVisible[r, c])
						vis++;

			int edges = cols + ((rows - 1) * 2) + (cols - 2);

			int totVisible = vis + edges;

			Console.WriteLine($"Part One Total Visible: {totVisible}");

		}

		static void PartTwo()
		{
			var inputPath = $@"{Environment.CurrentDirectory}\input.txt";
			var inputText = File.ReadAllLines(inputPath).ToList();

			int cols = inputText.First().Length;
			int rows = inputText.Count();

			int[,] map = new int[cols, rows];
			var scores = new List<int>();

			for (int r = 0; r < rows; r++)
			{
				for (int c = 0; c < cols; c++)
				{
					map[c, r] = int.Parse(inputText[r][c].ToString());
				}
			}

			for (int r = 1; r < rows - 1; r++)
			{
				for (int c = 1; c < cols - 1; c++)
				{
					int score = 1;
					for (int d = 0; d < 4; d++)
					{
						var nVis = TreesInView(map, r, c, d);
						score *= nVis;
					}

					scores.Add(score);
				}
			}

			var maxPossible = scores.Max();

			Console.WriteLine($"Part Two Max Possible: {maxPossible}");
		}


		static int TreesInView(int[,] map, int x, int y, int dir)
		{
			int nVis = 0;
			var target = map[x, y];

			switch (dir)
			{
				case 0: // Down
					for (int i = y + 1; i < map.GetLength(1); i++)
					{
						var tree = map[x, i];
						if (tree < target)
							nVis++;
						else if (tree >= target)
						{
							nVis++;
							break;
						}
					}
					break;

				case 1: // Up

					for (int i = y - 1; i >= 0; i--)
					{
						var tree = map[x, i];
						if (tree < target)
							nVis++;
						else if (tree >= target)
						{
							nVis++;
							break;
						}
					}
					break;

				case 2: // Left
					for (int i = x - 1; i >= 0; i--)
					{
						var tree = map[i, y];
						if (tree < target)
							nVis++;
						else if (tree >= target)
						{
							nVis++;
							break;
						}
					}
					break;

				case 3: // Right
					for (int i = x + 1; i < map.GetLength(0); i++)
					{
						var tree = map[i, y];
						if (tree < target)
							nVis++;
						else if (tree >= target)
						{
							nVis++;
							break;
						}
					}
					break;
			}

			return nVis;

		}



		static bool IsVisible(int[,] map, int x, int y, int dir)
		{
			bool isVis = true;
			var target = map[x, y];

			switch (dir)
			{
				case 0: // Down
					for (int i = 0; i < map.GetLength(1); i++)
					{
						if (i == y)
							break;

						var tree = map[x, i];
						if (tree >= target)
							isVis = false;
					}
					break;

				case 1: // Up

					for (int i = map.GetLength(1) - 1; i >= 0; i--)
					{
						if (i == y)
							break;

						var tree = map[x, i];
						if (tree >= target)
							isVis = false;
					}
					break;

				case 2: // Left
					for (int i = map.GetLength(0) - 1; i >= 0; i--)
					{
						if (i == x)
							break;

						var tree = map[i, y];
						if (tree >= target)
							isVis = false;
					}
					break;

				case 3: // Right
					for (int i = 0; i < map.GetLength(0); i++)
					{
						if (i == x)
							break;

						var tree = map[i, y];
						if (tree >= target)
							isVis = false;
					}
					break;
			}

			return isVis;

		}
	}
}