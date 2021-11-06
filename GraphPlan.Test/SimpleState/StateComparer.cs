using GraphPlan.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraphPlan.Test.SimpleState
{
    public class State : Dictionary<string, int>, ICloneable //
    {
        public State()
        {
        }

        public State(IDictionary<string, int> dictionary)
            : base(dictionary)
        {
        }

        public object Clone()
        {
            return new State(this.ToDictionary(p => p.Key, p => p.Value));
        }
    }

    public class StateComparer : IPlanningStateComparer<State>
    {
        public bool Equals(State x, State y)
        {
            return x.Count == y.Count && !x.Except(y).Any();
        }

        public int GetHashCode(State state)
        {
            var hash = 127;

            //a ^= b is shorthand for a = a ^ b
            //^ is bitwise XOR
            //0 ^ 0 = 0; 1 ^ 0 = 1; 0 ^ 1 = 1; 1 ^ 1 = 0
            foreach (var parameter in state)
            {
                hash ^= parameter.Key.GetHashCode();
                hash ^= parameter.Value.GetHashCode();
            }
            return hash;
        }

        public double Distance(State s1, State s2)
        {
            var same = s1.Keys.Intersect(s2.Keys).ToList();
            var percent = same.Where(k => s1[k] != s2[k]).Sum(k => (double)Math.Abs(s1[k] - s2[k]) / Math.Max(s1[k], s2[k]));
            var countDifferent = s1.Keys.Concat(s2.Keys).Except(same).Count();
            return (percent + countDifferent) / (same.Count() + countDifferent);
        }
    }
}
