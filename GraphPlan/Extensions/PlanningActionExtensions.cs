namespace GraphPlan.Extensions
{
    using GraphPlan.Models;
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public static class PlanningActionExtensions
    {
        public static IEnumerable<IPlanningAction<T>> PrintToConsole<T>(this IEnumerable<IPlanningAction<T>> actions)
        {
            int i = 0;

            Console.WriteLine($"{actions.Count()} step(s) suggested");
            foreach (var action in actions)
            {
                i++;
                Console.WriteLine($"Step {i}: {action.name}");
            }

            return actions;
        }
    }
}
