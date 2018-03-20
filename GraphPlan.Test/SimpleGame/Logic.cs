using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraphPlan.Test.SimpleGame
{
	public class SimpleGameState
	{
		public SimpleGameState()
		{
			axeAvailable = true;
			Player = new Player();
		}

		public Player Player { get; set; }
		public bool axeAvailable { get; set; }
	}

	public class Player
	{
		public Player()
		{
			Wood = 0;
			hasAxe = false;
		}

		public int Wood { get; set; }
		public bool hasAxe { get; set; }
	}
}
