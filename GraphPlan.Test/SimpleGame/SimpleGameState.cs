using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraphPlan.Test.SimpleGame
{
    public class SimpleGameState : ICloneable
    {
        public SimpleGameState()
        {
            axeAvailable = true;
            Player = new Player();
        }

        public Player Player { get; set; }
        public bool axeAvailable { get; set; }

        public object Clone()
        {
            return new SimpleGameState()
            {
                axeAvailable = this.axeAvailable,
                Player = new Player()
                {
                    hasAxe = this.Player.hasAxe,
                    Wood = this.Player.Wood
                }
            };
        }
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
