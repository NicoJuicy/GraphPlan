using GraphPlan.Test.Logic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraphPlan.Test.Models
{

	public abstract class Player : ResourceableImplementation, IPlayer
	{
		public Player()
		{
			this.Energy = 100;
			this.SetMaxResources(new Resources(0, 0, 0, 0));
		}

		public int Energy { get; set; }

		public bool canGoTo(Coords coords)
		{

			return Energy > 0;
		}

		public void GoTo(Coords coords)
		{
			Energy--;
			this.CurrentCoords = coords; //Flyyyy
		}
	}

	public class Farmer : Player
	{
		public Farmer() : base()
		{
			this.SetMaxResources(new Resources(100, 100, 100, 100));
		}
	}

	public interface IPlayer : ILocateable, IMoveable, IResourceable
	{

	}

}

