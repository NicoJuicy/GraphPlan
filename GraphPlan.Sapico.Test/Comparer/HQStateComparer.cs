using GraphPlan.Models;
using GraphPlan.Sapico.Test.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraphPlan.Sapico.Test.Comparer
{
    public class HQStateComparer : IPlanningStateComparer<Models.HQState>
    {
        public double Distance(HQState state1, HQState state2) => 1;

        public bool Equals(HQState x, HQState y)
        {
            return String.Join("", x.Servers.Select(dl => new { ServerState = dl.ServerState }).ToList()).Equals(string.Join("",y.Servers.Select(dl => new { ServerState = dl.ServerState }).ToList()));
        }

        public int GetHashCode(HQState obj) => obj.GetHashCode();
    }
}
