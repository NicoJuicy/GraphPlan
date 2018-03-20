using GraphPlan.Test.Logic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraphPlan.Test.Models
{
	//public class Wellness : LocateableImplementation, IBuilding
	//{
	//	public void Land(Farmer player)
	//	{
	//		if (player.Energy < 100)
	//		{
	//			player.Energy++;
	//		}
	//	}
	//}

	//public class Forest : ResourceableImplementation, IBuilding, IResourceable
	//{
	//	public Forest()
	//	{
	//		this.Resources.Wood = 1000;
	//	}


	//	public void Land(Farmer player)
	//	{
	//		//if(base.canCarry(player.R)
	//	}



	//}

	//public abstract class Building : LocateableImplementation, IBuilding
	//{
	//	public IBuilding SetCoords(Coords coords)
	//	{
	//		this.CurrentCoords = coords;
	//		return this;
	//	}

	//	public override void Land(Farmer player)
	//	{
	//		string t = "";
	//	}
	//}

	public interface IBuilding : ILocateable
	{
		void Land(Farmer player);
		IBuilding SetCoords(Models.Coords coords);
		
	}
}
