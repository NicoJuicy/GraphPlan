namespace GraphPlan
{
    using GraphPlan.Models;
    using GraphPlan.Solvers;
    using System;
    using System.Collections.Generic;
    using System.Linq;

    //https://github.com/FoC-/GOAP/tree/master/src/Goap
    public class GraphPlan<T>
        where T : class
    {
        private readonly IEnumerable<IPlanningAction<T>> planningActions;

        private readonly IPlanningStateComparer<T> planningStateComparer;

        public GraphPlan(Enums.PlanningMethod planningMethod, IEnumerable<IPlanningAction<T>> planningActions, IPlanningStateComparer<T> planningStateComparer )
        {

            this.planningMethod = Enums.PlanningMethod.BreadthFirst;
            this.planningActions = planningActions;
            this.planningStateComparer = planningStateComparer;
        }

        private Enums.PlanningMethod planningMethod { get; set; }

        public IEnumerable<IPlanningAction<T>> MakePlan(
            T initialState,
            T goalState)
        {

            //Nothing to change, skip
            if(initialState == goalState )
            {
                return Enumerable.Empty<IPlanningAction<T>>();
            }

            var visitedStates = new HashSet<T>() { initialState };
            var unvisitedPaths = UnvisitedPathes<Path<IPlanningAction<T>>>();

            var actions = planningActions;

            var possibleActions = actions
                .Where(el => el.CanExecute(initialState))
                .Select(action => new Path<IPlanningAction<T>>(action));

            foreach (var action in possibleActions)
            {
                unvisitedPaths.Add(0, action);
            }

            while (unvisitedPaths.HasElements)
            {
                var path = unvisitedPaths.Get();

                var reachedByPath = path.Reverse().Aggregate(initialState, (current, action) => action.Execute(current));
                if (visitedStates.Contains(reachedByPath)) continue;
                if (planningStateComparer.Equals(reachedByPath, goalState))
                {
                    return path.Reverse().ToList(); 
                }

                visitedStates.Add(reachedByPath);

                var plans = actions.Where(action => action.CanExecute(reachedByPath)).ToList();

                foreach (var action in plans)
                {
                    var distance = planningStateComparer.Distance(action.Execute(reachedByPath), goalState);
                    var plan = path.AddChild(action, distance);
                    unvisitedPaths.Add(plan.Cost, plan);
                }
            }

            return Enumerable.Empty<IPlanningAction<T>>();
        }

        private IPrioritized<double, S> UnvisitedPathes<S>()
        {
            switch (this.planningMethod)
            {
                case Enums.PlanningMethod.BreadthFirst:
                    return new PrioritizedQueue<double, S>();
                case Enums.PlanningMethod.DepthFirst:
                    return new PrioritizedStack<double, S>();
                default:
                    return new PrioritizedQueue<double, S>();
            }
        }
    }

    public static class GraphPlanExtensions
    {
        public static T Do<T>(this IEnumerable<IPlanningAction<T>> actions, T initalState)
        {

            Console.WriteLine($"{actions.Count()} step(s) suggested");
            actions.ToList().ForEach(a => Console.WriteLine($"{a.name}"));
            T currentState = initalState;
            foreach (var action in actions)
            {
                if (action.CanExecute(currentState))
                {
                    currentState = action.Execute(currentState);
                }
            }

            return currentState; // is final state
        }
    }
}
