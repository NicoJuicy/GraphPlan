using GraphPlan.Models;
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
		//private Enums.Output OuputMethod {get;set;}
		private Context<T> Context { get; set; }

		//Constructor
		public GraphPlan()
		{

			this.SearchMethod = Enums.SearchMethod.BreathFirst;
			//this.OuputMethod = Enums.Output.Binary;

			Context = new Context<T>(this);
		}

		public Context<T> Prepare()
		{
			//Context.GraphPlan = this;
			return Context;
		}

		//Methods

		public GraphPlan<T> SetSearchMethod(Enums.SearchMethod SearchMethod)
		{
			this.SearchMethod = SearchMethod;
			return this;
		}

		//public GraphPlan<T> SetSerializationType(Enums.Output OutputMethod )
		//{
		//	this.OuputMethod = OutputMethod;
		//	return this;
		//}

		public List<PlanningAction<T>> Solve(
			T initialState,
			T targetState)
		{

			return Context.PlanningActions;//new List<PlanningAction<T>>(Context.);
		}



		public List<PlanningAction<T>> Load(string Path)
		{
			using (StreamReader sr = new StreamReader(Path))
			{
				return Load(sr.BaseStream);
			}
		}
		public List<PlanningAction<T>> Load(Stream stream)
		{

			IFormatter bf = new BinaryFormatter(); //= this.OuputMethod == Enums.Output.Binary ? new BinaryFormatter() // : new XmlFormatter(typeof();// new IFormatter();
			Context.PlanningActions = (List<PlanningAction<T>>)bf.Deserialize(stream);

			return Context.PlanningActions;
		}
	}

	public static class PlanningActionExtensions
	{
		public static List<PlanningAction<T>> Save<T>(this List<PlanningAction<T>> actions, string Path)
		{
			using (StreamWriter sr = new StreamWriter(Path))
			{
				return actions.Save(sr.BaseStream);
			}
		}
		public static List<PlanningAction<T>> Save<T>(this List<PlanningAction<T>> actions, Stream writer)
		{ 
			BinaryFormatter bf = new BinaryFormatter();
			bf.Serialize(writer, actions);

			return actions;
		}

		public static List<PlanningAction<T>> PrintToConsole<T>(this List<PlanningAction<T>> actions)
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

		public static T Do<T>(this List<PlanningAction<T>> actions, T initalState)
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
