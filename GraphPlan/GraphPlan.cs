using GraphPlan.Models;
using System;
using System.Collections.Generic;
using System.Linq;
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
			Context = new Context<T>(this);
		}

		public Context<T> Prepare()
		{
			//Context.GraphPlan = this;
			return Context;
		}

		//Methods

		public GraphPlan<T> SetSearchMethod(Enums.SearchMethod SearchMethod = Enums.SearchMethod.BreathFirst)
		{
			this.SearchMethod = SearchMethod;
			return this;
		}

		public List<PlanningAction<T>> Solve(
			T initialState, 
			T targetState)
		{

			return Context.PlanningActions;//new List<PlanningAction<T>>(Context.);
		}
	}

	public static class PlanningActionExtensions
	{
		public static List<PlanningAction<T>> PrintToConsole<T>(this List<PlanningAction<T>> actions)
		{
			int i = 0;

			Console.WriteLine($"{actions.Count()} step(s) suggested");
			foreach(var action in actions)
			{
				i++;
				Console.WriteLine($"Step {i}: {action.name}");
			}

			return actions;
		}
	}

	
}
