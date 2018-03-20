using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraphPlan.Models
{
	[Serializable]
	public class PlanningAction<T> //where T : ICloneable
	{
		public PlanningAction()
		{

		}

		public PlanningAction(string name, Predicate<T> preconditions, Action<T> effects)
		{
			this.name = name;
			this.preconditions = preconditions;
			this.effects = effects;
		}

	
		public string name { get; private set; }

		private readonly Predicate<T> preconditions;

		private readonly Action<T> effects;

		public bool CanExecute(T state)
		{
			
			return preconditions(state);
			
		}

		public T Execute(T state)
		{
			var newState = (T)state; //should be copied as value
			//T newState = (T)state.Clone();
			effects(newState);
			return newState;
		}

		public override string ToString()
		{
			return name; 
		}
	}
}
