using GraphPlan.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraphPlan.Comparer
{
    public class ValueObjectComparer : IPlanningStateComparer<ValueObject>
    {
        public double Distance(ValueObject state1, ValueObject state2) => 1;

        public bool Equals(ValueObject x, ValueObject y) => x == y;

        public int GetHashCode(ValueObject obj)
        {
            return obj.GetHashCode();
        }
    }
}
