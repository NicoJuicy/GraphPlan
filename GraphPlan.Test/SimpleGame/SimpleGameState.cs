using GraphPlan.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;

namespace GraphPlan.Test.SimpleGame
{
    [Serializable]
    public class SimpleGameState : ValueObject, ICloneable
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
            using (var stream = new MemoryStream())
            {
                BinaryFormatter formatter = new BinaryFormatter();
                formatter.Serialize(stream, this);
                stream.Position = 0;
                return formatter.Deserialize(stream);
            }
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Player;
            yield return axeAvailable;
        }
    }
    [Serializable]
    public class Player : ValueObject
    {
        public Player()
        {
            Wood = 0;
            axeLife = 0;
            //hasAxe = false;
        }

        public int Wood { get; set; }
        public bool hasAxe
        {
            get { return axeLife > 0; }
            set { if (value && axeLife == 0) axeLife += 10; }
        }
        public int axeLife { get; set; }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Wood;
            yield return hasAxe;
        }
    }
}
