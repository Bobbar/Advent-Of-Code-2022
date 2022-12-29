using System.Diagnostics;

namespace Day_13___Distress_Signal
{
	internal class Program
	{
		static void Main(string[] args)
		{
			var inputPath = $@"{Environment.CurrentDirectory}\input.txt";
			var inputText = File.ReadAllLines(inputPath).ToList();
			var root = new List<object>();

			//var A = new List<int>();
			//var B = new List<int>();


			var pack = new List<int>[2];
			pack[0] = new List<int>();
			pack[1] = new List<int>();


			int curPack = 0;

			foreach (var input in inputText)
			{
				if (input.Length == 0)
				{
					// Start new packet?

					pack[0] = new List<int>();
					pack[1] = new List<int>();
					curPack = 0;

					continue;
				}
				
				var p = new List<List<int>>();
				var t = ParseLine(input, p);

				for (int i = 0; i < input.Length; i++)
				{
					var c = input[i];


					switch (c)
					{
						case '[':
							// Start new list.
							pack[curPack] = new List<int>();
							continue;
							break;

						case ',':
							// List sparator.
							continue;
							break;

						case ']':
							// End current list.
							//Compare(pack[0], pack[1]);
							if (curPack == 1)
								Compare(pack[0], pack[1]);

							break;

						default:
							// Add integer to list?
							pack[curPack].Add(int.Parse(c.ToString()));
							break;
					}

				}

				curPack = 1;
			}


			Console.ReadKey();
		}

		static List<List<int>> ParseLine(string line, List<List<int>> packet = null)
		{
			var trim = line.Remove(0, 1);//.Remove(line.Length, 1);
			trim = trim.Remove(trim.Length - 1, 1);

			if (trim[0] == '[')
			{
				ParseLine(trim);
			}


			var split = trim.Split(',');


			//foreach(var c in trim)
			//{
			//	if (int.TryParse(c.ToString(), out int num))
			//	{
			//		packet.
			//	}
			//}


			return new List<List<int>>();
		}

		static void Compare(List<int> A, List<int> B)
		{

		}
	}
}