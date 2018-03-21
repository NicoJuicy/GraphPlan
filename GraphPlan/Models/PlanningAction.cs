using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraphPlan.Models
{

	public interface IPlanningAction<T>
	{

		string name { get; }
		bool CanExecute(T state);
		T Execute(T state);
	}

	[Serializable]
	public class PlanningAction<T> : IPlanningAction<T> where T : ICloneable
	{
		public PlanningAction()
		{

		}

		public PlanningAction(string name, Predicate<T> conditions, Action<T> effects)
		{
			this.name = name;
			this.conditions = conditions;
			this.effects = effects;
		}


		public string name { get; private set; }

		private readonly Predicate<T> conditions;

		private readonly Action<T> effects;

		public bool CanExecute(T state)
		{

			return conditions(state);

		}

		public T Execute(T state)
		{
			var newState = (T)state.Clone(); 
			effects(newState);
			return newState;
		}

		public override string ToString()
		{
			return name;
		}
	}
}
