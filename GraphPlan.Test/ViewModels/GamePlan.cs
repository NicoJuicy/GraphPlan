using GraphPlan.Test.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraphPlan.Test.ViewModels
{
	public class GamePlan
	{
		public List<Player> Players { get; set; }
		public List<IBuilding> Buildings { get; set; }
	}
}
