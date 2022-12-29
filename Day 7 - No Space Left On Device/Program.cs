using System.Diagnostics;

namespace Day_7___No_Space_Left_On_Device
{
	internal class Program
	{
		static void Main(string[] args)
		{
			var inputPath = $@"{Environment.CurrentDirectory}\input.txt";
			var inputText = File.ReadAllLines(inputPath).ToList();

			var root = new MyDirectory() { Name = "/" };

			var current = root;

			for (int i = 0; i < inputText.Count; i++)
			{
				var line = inputText[i];

				if (line.Contains("$"))
				{
					// Command.
					var text = line.Remove(0, 1).Trim();
					var parts = text.Split(' ');

					var cmd = parts[0];

					if (cmd == "cd")
					{
						var dest = parts[1];

						if (dest == "..")
						{
							current = current.Parent;
						}
						else if (dest != ".." && dest != "/")
						{
							current = current.Dirs.Where(d => d.Name == dest).First();
						}

						Log($"{i + 1}  {line}     {current.Name}[{current.ID}]");
					}
					else if (cmd == "ls")
					{
						Log($"{i + 1}  {line}");

						i++;
						line = inputText[i];
						Log($"{i + 1}  {line}");

						while (line.Contains("$") == false)
						{

							var lsParts = line.Split(" ");

							if (lsParts[0] == "dir")
							{
								var newDir = new MyDirectory(lsParts[1], current) { ID = i };
								Log($"[New] {newDir.Name} [{newDir.ID}] - Parent: {current.Name} [{current.ID}]");

								current.Dirs.Add(newDir);
							}
							else
							{
								var size = int.Parse(lsParts[0]);
								var name = lsParts[1];

								current.Files.Add(new MyFile(name, size));
							}

							if (i + 1 >= inputText.Count || inputText[i + 1].Contains("$"))
								break;
							else
							{
								i++;
								line = inputText[i];
							}

							Log($"{i + 1}  {line}");

						}
					}
				}
			}

			var sum = WalkAndSum(root);
			Console.WriteLine($"Part One Sum: {sum}");


			const int available = 70000000;
			const int required = 30000000;

			var totalDir = root.Size();
			var remaining = available - totalDir;
			var needed = required - remaining;
			var foundDirs = new List<MyDirectory>();

			FindLargest(root, needed, foundDirs);

			foundDirs = foundDirs.OrderBy(d => d.Size()).ToList();

			Console.WriteLine($"Part Two Smallest: {foundDirs.First().Size()}");


			Console.ReadKey();
		}


		static void FindLargest(MyDirectory dir, int sizeNeeded, List<MyDirectory> found)
		{
			foreach (var d in dir.Dirs)
			{
				if (d.Size() >= sizeNeeded)
					found.Add(d);

				FindLargest(d, sizeNeeded, found);
			}
		}

		static int WalkAndSum(MyDirectory dir)
		{
			int sum = 0;

			foreach (var d in dir.Dirs)
			{
				if (d.Size() <= 100000)
					sum += d.Size();

				sum += WalkAndSum(d);
			}

			return sum;
		}

		static void Log(string msg)
		{
			//Debug.WriteLine(msg);
		}


	}



	public class MyDirectory
	{
		public string Name { get; set; }
		public MyDirectory Parent { get; set; }
		public List<MyDirectory> Dirs { get; set; } = new List<MyDirectory>();
		public List<MyFile> Files { get; set; } = new List<MyFile>();
		public int ID {  get; set; }

		public MyDirectory() { }

		public MyDirectory(string name, MyDirectory parent)
		{
			Name = name;
			Parent = parent;
		}

		public int Size()
		{
			int size = 0;

			foreach(var file in Files)
			{
				size += file.Size;
			}

			foreach (var dir in Dirs)
			{
				size += dir.Size();
			}

			return size;
		}

		public override string ToString()
		{
			return $"{Name}  ({Size()})   [{ID}]";
		}
	}

	public class MyFile
	{
		public string Name { get; set; }
		public int Size { get; set; }

		public MyFile(string name, int size)
		{
			Name = name;
			Size = size;
		}

		public override string ToString()
		{
			return $"{Name} ({Size})";
		}

	}

}