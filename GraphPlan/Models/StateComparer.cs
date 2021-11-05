namespace GraphPlan.Models
{
    using System.Collections.Generic;

    public interface IPlanningStateComparer<in T> : IEqualityComparer<T>
    {
        double Distance(T state1, T state2);
    }
}
