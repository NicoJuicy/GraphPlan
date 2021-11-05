using GraphPlan.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;

namespace GraphPlan.Sapico.Test.Models
{
    [Serializable]
    public class HQState : ValueObject, ICloneable
    {
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
            yield return Servers;
            yield return Employees;
        }

        public List<Employee> Employees { get; set; }


        public List<Server> Servers { get; set; }
        
    }

    [Serializable]
    public class Employee : ValueObject
    {
        public string Name { get; set; }
        public bool IsAtWork { get; set; }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Name;
            yield return IsAtWork;
        }
    }
    [Serializable]
    public class Server : ValueObject, ICloneable
    {
        public string ServerName { get; set; }
        public ServerBehaviour Behaviour { get; set; }
        
        public ServerState ServerState { get; set; }
        public string snapshot_id { get; set; }
        public string provider { get; set; }

        public object Clone()
        {
            return this.MemberwiseClone();
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return ServerName;
            yield return Behaviour;
            yield return ServerState;
            yield return provider;

        }
    }

    [Flags]
    public enum ServerBehaviour
    {
        AlwaysOn,
        WhenAtWork
    }

    public enum ServerState
    {
        Off,
        On,
        NotFound,
        Restoring,
    }

}
