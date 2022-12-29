using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Day_4___Camp_Cleanup
{
	public static class Extensions
	{
		public static bool Contains(this Range range, Range other)
		{
			if (other.Start.Value >= range.Start.Value && other.End.Value <= range.End.Value)
				return true;

			if (range.Start.Value >= other.Start.Value && range.End.Value <= other.End.Value)
				return true;


			return false;
		}


		public static bool Overlaps(this Range range, Range other)
		{
			bool res = false;

			if (other.Start.Value >= range.Start.Value && other.Start.Value <= range.End.Value)
				res = true;

			if (other.End.Value >= range.Start.Value && other.End.Value <= range.End.Value)
				res = true;

			return res;
		}
	}
}
