
using System.Diagnostics;
using System.Drawing;
using System.Reflection;
using Priority_Queue;

namespace Day_12___Hill_Climbing_Algorithm
{
	internal class Program
	{
		private const int LCaseOffset = 96;
		static void Main(string[] args)
		{
			var pt1 = FindFewest(1);
			Console.WriteLine($"Part One Fewest Steps: {pt1}");

			var pt2 = FindFewest(2);
			Console.WriteLine($"Part Two Fewest Steps: {pt2}");

			Console.ReadKey();
		}

		static int FindFewest(int part)
		{
			var inputPath = $@"{Environment.CurrentDirectory}\input.txt";
			var inputText = File.ReadAllLines(inputPath).ToList();

			int cols = inputText.First().Length;
			int rows = inputText.Count;

			var start = new Point();
			var end = new Point();

			int[,] map = new int[cols, rows];
			var queue = new SimplePriorityQueue<Point>();

			for (int r = 0; r < rows; r++)
			{
				for (int c = 0; c < cols; c++)
				{
					var ele = inputText[r][c];

					if (ele == 'S')
					{
						start = new Point(c, r);
						map[c, r] = 1;

						continue;
					}
					else if (ele == 'E')
					{
						end = new Point(c, r);
						map[c, r] = 26;
						continue;
					}
					else
						map[c, r] = (int)ele - LCaseOffset;

					if (part == 2)
						if (map[c, r] == 1)
							queue.Enqueue(new Point(c, r), 0);
				}
			}

			queue.Enqueue(start, 0);

			int fewest = 0;
			var path = new List<Point>();

			while (queue.Count > 0)
			{
				var d = queue.GetPriority(queue.First());
				var pnt = queue.Dequeue();

				if (path.Contains(pnt))
					continue;

				path.Add(pnt);

				if (pnt == end)
				{
					fewest = (int)d;
					break;
				}

				var ns = GetValidNs(map, pnt);
				foreach (var n in ns)
				{
					queue.Enqueue(n, d + 1);
				}
			}

			return fewest;
		}


		static List<Point> GetValidNs(int[,] map, Point pnt)
		{
			var ns = new List<Point>();
			var cur = map[pnt.X, pnt.Y];

			for (int xo = -1; xo <= 1; xo++)
			{
				for (int yo = -1; yo <= 1; yo++)
				{
					if (xo == 0 || yo == 0)
					{
						int nx = xo + pnt.X;
						int ny = yo + pnt.Y;

						if (nx == pnt.X && ny == pnt.Y)
							continue;

						if (nx >= 0 && ny >= 0 && nx < map.GetLength(0) && ny < map.GetLength(1))
						{
							var n = map[nx, ny];
							if (n <= cur + 1)
								ns.Add(new Point(nx, ny));
						}
					}
				}
			}

			return ns;
		}
	}
}