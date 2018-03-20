﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraphPlan.Test.Models
{
	public class Resources
	{
		public Resources()
		{
			Wood = 0;
			Gold = 0;
			Food = 0;
			Stone = 0;
		}

		public Resources(int Wood, int Gold, int Food, int Stone)
		{
			this.Wood = Wood;
			this.Gold = Gold;
			this.Food = Food;
			this.Stone = Stone;
		}

		public int Wood { get; set; }
		public int Gold { get; set; }
		public int Food { get; set; }
		public int Stone { get; set; }
	}
}
