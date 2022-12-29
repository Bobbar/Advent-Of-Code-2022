using System.Diagnostics;
using System.Drawing;
using System.Numerics;

namespace Day_9___Rope_Bridge
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

			var start = new Vector2(0, 0);
			var head = start;
			var tail = start;
			var visited = new HashSet<Vector2>();

			foreach (var line in inputText)
			{
				var cmd = line.Split(' ');
				var dir = cmd[0];
				var steps = int.Parse(cmd[1]);

				var dirVec = GetDirVector(dir);

				for (int i = 0; i < steps; i++)
				{
					head += dirVec;

					AdjustTail(head, ref tail);

					visited.Add(tail);
				}
			}

			var numVisited = visited.Count();

			Console.WriteLine($"Part One Num Visited: {numVisited}");
		}

		static void PartTwo()
		{
			var inputPath = $@"{Environment.CurrentDirectory}\input.txt";
			var inputText = File.ReadAllLines(inputPath).ToList();

			var start = new Vector2(0, 0);
			var head = start;
			var segments = new Vector2[9];
			var visited = new HashSet<Vector2>();
			
			foreach (var line in inputText)
			{
				var cmd = line.Split(' ');
				var dir = cmd[0];
				var steps = int.Parse(cmd[1]);
				var dirVec = GetDirVector(dir);

				for (int i = 0; i < steps; i++)
				{
					head += dirVec;

					AdjustSegments(head, ref segments);

					visited.Add(segments.Last());
				}
			}

			var numVisited = visited.Count();

			Console.WriteLine($"Part Two Num Visited: {numVisited}");
		}

		static void AdjustSegments(Vector2 head, ref Vector2[] segments)
		{
			var next = head;
			for (int i = 0; i < segments.Length; i++)
			{
				if (IsAdjacent(next, segments[i]))
				{
					next = segments[i];
					continue;
				}

				var dir = next - segments[i];
				var sign = new Vector2(Math.Sign(dir.X), Math.Sign(dir.Y));

				segments[i] += sign;

				next = segments[i];
			}
		}

		static void AdjustTail(Vector2 head, ref Vector2 tail)
		{
			if (IsAdjacent(head, tail))
				return;

			var dir = head - tail;
			var sign = new Vector2(Math.Sign(dir.X), Math.Sign(dir.Y));

			tail += sign;
		}

		static bool IsAdjacent(Vector2 head, Vector2 tail)
		{
			var diff = Vector2.Abs(head - tail);
			if (diff.X > 1 || diff.Y > 1)
				return false;

			return true;
		}

		static Vector2 GetDirVector(string dir)
		{
			switch (dir)
			{
				case "U": // UP
					return new Vector2(0, -1);
				case "D": // DOWN
					return new Vector2(0, 1);
				case "L": // LEFT
					return new Vector2(-1, 0);
				case "R": // RIGHT
					return new Vector2(1, 0);
				default:
					return new Vector2();
			}
		}

		static void DrawField(Vector2 head, Vector2 tail)
		{
			int rows = 5;
			int cols = 6;

			for (int r = 0; r < rows; r++)
			{
				for (int c = 0; c < cols; c++)
				{
					var pos = new Vector2(c, r);

					if (pos == head)
						Debug.Write("H");
					else if (pos == tail)
						Debug.Write("T");
					else
						Debug.Write(".");
				}
				Debug.WriteLine("");
			}

			Debug.WriteLine("");
		}

		static void DrawField(Vector2 head, Vector2[] segments)
		{
			int rows = 5;
			int cols = 6;

			for (int r = 0; r < rows; r++)
			{
				for (int c = 0; c < cols; c++)
				{
					var pos = new Vector2(c, r);

					bool hasSeg = false;
					for (int i = 0; i < segments.Length; i++)
					{
						if (pos == segments[i])
						{
							Debug.Write(i);
							hasSeg = true;
							break;
						}

					}

					if (pos == head)
						Debug.Write("H");
					else if (pos != head && !hasSeg)
						Debug.Write(".");
				}
				Debug.WriteLine("");
			}

			Debug.WriteLine("");
		}
	}
}