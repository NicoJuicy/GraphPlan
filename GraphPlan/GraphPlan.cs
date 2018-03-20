using GraphPlan.Models;
using GraphPlan.Solvers;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;

namespace GraphPlan
{
    public class GraphPlan<T>
    {
        private Enums.SearchMethod SearchMethod { get; set; }
        private Context<T> Context { get; set; }

        //Constructor
        public GraphPlan()
        {

            this.SearchMethod = Enums.SearchMethod.BreathFirst;


            Context = new Context<T>(this);
        }

        public Context<T> Prepare()
        {
            return Context;
        }

        //Methods

        public GraphPlan<T> SetSearchMethod(Enums.SearchMethod SearchMethod)
        {
            this.SearchMethod = SearchMethod;
            return this;
        }

        public List<IPlanningAction<T>> Solve(
            T initialState,
            T goalState)
        {
            var visitedStates = new HashSet<T>() { initialState };
            var unvisitedPaths = UnvisitedPathes<Path<IPlanningAction<T>>>();

            var actions = Context.PlanningActions;

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
                //if (stateComparer.Equals(reachedByPath, goalState)) return path.Reverse();

                visitedStates.Add(reachedByPath);

                var plans = actions.Where(action => action.CanExecute(reachedByPath));

                foreach (var action in plans)
                {
                    var distance = 1;// stateComparer.Cost(action.Execute(reachedByPath), goalState);
                    var plan = path.AddChild(action, distance);
                    unvisitedPaths.Add(plan.Cost, plan);
                }
            }

            return Enumerable.Empty<IPlanningAction<T>>().ToList();

            //	return Context.PlanningActions;//return all possible actions
        }

        private IPrioritized<double, S> UnvisitedPathes<S>()
        {
            //switch (traverseMethod)
            //{
            //	case TraverseMethod.BreadthFirst:
            return new PrioritizedQueue<double, S>();
            //default:
            //	return new PrioritizedStack<double, S>();
        }




        #region serialization
        public List<IPlanningAction<T>> Load(string Path)
        {
            using (StreamReader sr = new StreamReader(Path))
            {
                return Load(sr.BaseStream);
            }
        }
        public List<IPlanningAction<T>> Load(Stream stream)
        {

            IFormatter bf = new BinaryFormatter(); //= this.OuputMethod == Enums.Output.Binary ? new BinaryFormatter() // : new XmlFormatter(typeof();// new IFormatter();
            Context.PlanningActions = (List<IPlanningAction<T>>)bf.Deserialize(stream);

            return Context.PlanningActions;
        }
        #endregion
    }

    public static class GraphPlanExtensions
    {
        public static T Do<T>(this List<IPlanningAction<T>> actions, T initalState)
        {
            int i = 0;

            Console.WriteLine($"{actions.Count()} step(s) suggested");

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