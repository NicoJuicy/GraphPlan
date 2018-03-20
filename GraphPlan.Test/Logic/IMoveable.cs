using GraphPlan.Test.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraphPlan.Test.Logic
{
	
	

	public abstract class MoveableImplementation : LocateableImplementation, ILocateable
	{

		public MoveableImplementation()
		{
			this.CurrentCoords = new Coords(0, 0);
		}

		public bool canGoTo(Coords coords)
		{
			return true;
		}

		public void GoTo(Coords coords)
		{
			if (canGoTo(coords))
			{
				this.CurrentCoords = coords;
			}
		}


	}

	public interface IMoveable
	{
		void GoTo(Coords coords);
		bool canGoTo(Coords coords);
	}
}
