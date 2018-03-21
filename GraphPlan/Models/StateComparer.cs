using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraphPlan.Models
{
    public interface IPlanningStateComparer<in T> : IEqualityComparer<T>
    {
        double Distance(T state1, T state2);
    }
    
}
