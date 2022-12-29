using System.Diagnostics;
using System.Drawing;
using System.Xml;

namespace Day_14___Regolith_Reservoir
{
	public partial class Form1 : Form
	{
		private bool[,] _grains;
		private bool[,] _atRest;
		private bool[,] _nextGrains;
		private bool[,] _nextAtRest;
		private bool[,] _walls;

		private Point _sz = new Point(600, 600);
		private Point _start = new Point(500, 0);
		private Point _grainSz = new Point(1, 1);

		private Point _offset;

		private int _maxYPartOne;

		private int _gens = 0;

		public Form1()
		{
			InitializeComponent();

			_grains = new bool[_sz.X, _sz.Y];
			_atRest = new bool[_sz.X, _sz.Y];
			_walls = new bool[_sz.X, _sz.Y];
			_nextGrains = new bool[_sz.X, _sz.Y];
			_nextAtRest = new bool[_sz.X, _sz.Y];

			_grains[_start.X, _start.Y] = true;

			ParseWalls();
		}

		private void InitField()
		{
			_grains = new bool[_sz.X, _sz.Y];
			_atRest = new bool[_sz.X, _sz.Y];
			_walls = new bool[_sz.X, _sz.Y];
			_nextGrains = new bool[_sz.X, _sz.Y];
			_nextAtRest = new bool[_sz.X, _sz.Y];

			_grains[_start.X - _offset.X, 0] = true;

		}

		private void AddNewGrain()
		{
			_grains[_start.X - _offset.X, 0] = true;
		}

		private void PlayWithSand()
		{
			int gens = 0;

			this.Text = "Running...";

			while (!IsDonePartOne())
			{
				Update();

				gens++;

				if (gens % 200 == 0)
				{
					pictureBox1.Refresh();
					Application.DoEvents();
				}

				// Spawn a new grain every other generation.
				if (gens % 2 == 0)
					AddNewGrain();
			}

			this.Text = $"Num Resting Part One: {NumResting()}";

			pictureBox1.Refresh();
			Application.DoEvents();

			while (!IsDonePartTwo())
			{
				Update();

				gens++;

				if (gens % 200 == 0)
				{
					pictureBox1.Refresh();
					Application.DoEvents();
				}

				// Spawn a new grain every other generation.
				if (gens % 2 == 0)
					AddNewGrain();
			}

			this.Text += $"   Num Resting Part Two: {NumResting()}";

			pictureBox1.Refresh();
			Application.DoEvents();
		}


		private void ParseWalls()
		{
			var inputPath = $@"{Environment.CurrentDirectory}\input.txt";
			var inputText = File.ReadAllLines(inputPath).ToList();

			var segs = new List<List<Point>>();

			// Grab segment lists.
			foreach (var line in inputText)
			{
				var pnts = new List<Point>();

				var splitParts = line.Replace(" ", "").Split("->");
				foreach (var part in splitParts)
				{
					var splitCoords = part.Trim().Split(',');
					int x = int.Parse(splitCoords[0]);
					int y = int.Parse(splitCoords[1]);

					pnts.Add(new Point(x, y));
				}

				segs.Add(pnts);
			}

			// Find min/max.
			var minMaxX = new Point(int.MaxValue, int.MinValue);
			var minMaxY = new Point(int.MaxValue, int.MinValue);

			foreach (var seg in segs)
			{
				foreach (var pnt in seg)
				{
					minMaxX = new Point(Math.Min(minMaxX.X, pnt.X), Math.Max(minMaxX.Y, pnt.X));
					minMaxY = new Point(Math.Min(minMaxY.X, pnt.Y), Math.Max(minMaxY.Y, pnt.Y));

				}
			}

			// Field size and offset to try to fit the field
			// into the least possible amount of memory.
			_sz = new Point(minMaxX.X, minMaxY.Y + 10);
			_offset = new Point(minMaxX.Y / 2, 0);

			this.Size = new Size((int)(_sz.X * 2.5f) + 40, (int)(_sz.Y * 2.5f) + 40);

			_maxYPartOne = minMaxY.Y;
			int maxYPartTwo = minMaxY.Y + 2;

			InitField();

			// Add the floor.
			segs.Add(new List<Point>() { new Point(_offset.X, maxYPartTwo), new Point(_offset.X + _sz.X - 1, maxYPartTwo) });

			foreach (var seg in segs)
			{
				for (int i = 0; i < seg.Count - 1; i++)
				{
					var a = seg[i];
					var b = seg[i + 1];

					var diff = new Point(b.X - a.X, b.Y - a.Y);
					var sign = new Point(Math.Sign(diff.X), Math.Sign(diff.Y));

					int num = Math.Max(Math.Abs(diff.X), Math.Abs(diff.Y));
					int cnt = 0;

					_walls[a.X - _offset.X, a.Y - _offset.Y] = true;

					var start = a;

					while (cnt < num)
					{
						int x = start.X += sign.X;
						int y = start.Y += sign.Y;

						_walls[x - _offset.X, y - _offset.Y] = true;

						cnt++;
					}
				}
			}
		}

		private void Update()
		{
			Array.Copy(_grains, _nextGrains, _grains.Length);
			Array.Copy(_atRest, _nextAtRest, _atRest.Length);

			for (int x = 0; x < _sz.X; x++)
			{
				for (int y = 0; y < _sz.Y; y++)
				{
					if (_walls[x, y])
						continue;

					if (_atRest[x, y] == false && _grains[x, y])
					{
						if (y + 1 < _sz.Y && _grains[x, y + 1] == false && _walls[x, y + 1] == false) // Move down.
						{
							_nextGrains[x, y] = false;
							_nextGrains[x, y + 1] = true;
						}
						else
						{
							if (x - 1 >= 0 && y + 1 < _sz.Y && _grains[x - 1, y + 1] == false && _walls[x - 1, y + 1] == false) // Move down & left.
							{
								_nextGrains[x, y] = false;
								_nextGrains[x - 1, y + 1] = true;
								continue;
							}

							if (x + 1 < _sz.X && y + 1 < _sz.Y && _grains[x + 1, y + 1] == false && _walls[x + 1, y + 1] == false) // Move down & right.
							{
								_nextGrains[x, y] = false;
								_nextGrains[x + 1, y + 1] = true;
								continue;
							}

							_nextAtRest[x, y] = true; // Cant move, set at rest?
						}
					}
				}
			}

			// Swap in next states.
			var tmpG = _grains;
			_grains = _nextGrains;
			_nextGrains = tmpG;

			var tmpR = _atRest;
			_atRest = _nextAtRest;
			_nextAtRest = tmpR;
		}

		private bool IsDonePartTwo()
		{
			if (_atRest[_start.X - _offset.X, 0])
				return true;

			return false;
		}

		private bool IsDonePartOne()
		{
			for (int x = 0; x < _sz.X; x++)
			{
				for (int y = 0; y < _sz.Y; y++)
				{
					if (_grains[x, y] && y >= _maxYPartOne)
						return true;
				}
			}

			return false;
		}

		private int NumResting()
		{
			int nRest = 0;
			foreach (var rest in _atRest)
			{
				if (rest)
					nRest++;
			}

			return nRest;
		}

		private void pictureBox1_Paint(object sender, PaintEventArgs e)
		{
			e.Graphics.ScaleTransform(2.5f, 2.5f);

			for (int x = 0; x < _sz.X; x++)
			{
				for (int y = 0; y < _sz.Y; y++)
				{
					if (_grains[x, y])
					{
						e.Graphics.FillRectangle(Brushes.Beige, x - (_grainSz.X / 2), y - (_grainSz.Y / 2), _grainSz.X, _grainSz.Y);
					}

					if (_atRest[x, y])
						e.Graphics.FillRectangle(Brushes.BurlyWood, x - (_grainSz.X / 2), y - (_grainSz.Y / 2), _grainSz.X, _grainSz.Y);



					if (_walls[x, y])
						e.Graphics.FillRectangle(Brushes.Green, x - (_grainSz.X / 2), y - (_grainSz.Y / 2), _grainSz.X, _grainSz.Y);

				}
			}

			e.Graphics.DrawRectangle(Pens.Red, 0, 0, _sz.X, _sz.Y);
		}

		private void Form1_KeyPress(object sender, KeyPressEventArgs e)
		{
			PlayWithSand();
		}
	}
}