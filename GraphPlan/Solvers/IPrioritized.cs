﻿namespace GraphPlan.Solvers
{
    internal interface IPrioritized<P, V>
    {
        bool HasElements { get; }

        void Add(P priority, V value);

        V Get();
    }
}
