using GraphPlan.Test.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraphPlan.Test.Logic
{
	public abstract class LocateableImplementation
	{
		internal Coords CurrentCoords { get; set; }

		public Coords getCoords()
		{
			return this.CurrentCoords;
		}
		
	}

	public interface ILocateable
	{
		Coords getCoords();
	}
}
