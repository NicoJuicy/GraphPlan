using GraphPlan.Test.Logic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraphPlan.Test.Models
{

	public class Farmer : FarmerImplementation, IPlayer
	{
		public Farmer()
		{
			Energy = 100;
		}

		public int Energy { get; set; }

		public bool canGoTo(Coords coords)
		{
			
			throw new NotImplementedException();
		}

		public void GoTo(Coords coords)
		{
			throw new NotImplementedException();
		}
	}

	public interface IPlayer : ILocateable, IMoveable, IResourceable
	{

	}

}

