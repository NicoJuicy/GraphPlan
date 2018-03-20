using GraphPlan.Test.Logic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraphPlan.Test.Models
{
	public class Wellness : LocateableImplementation, IBuilding
	{
		public void Land(Player player)
		{
			if (player.Energy < 100)
			{
				player.Energy++;
			}
		}
	}

	public class Forest :  ResourceableImplementation, IBuilding, IResourceable
	{
		public Forest()
		{
			this.Resources.Wood = 1000;
		}

		public void Land(Player player)
		{
			if(base.canCarry(player.R)
		}
	}



	public interface IBuilding : ILocateable
	{
		void Land(Player player);
	}
}
