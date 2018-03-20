using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraphPlan.Test.Models
{
	public class Coords
	{
		public Coords(int x, int y)
		{
			this.x = x;
			this.y = y;
		}
		public int x { get; set; }
		public int y { get; set; }
	}
}
