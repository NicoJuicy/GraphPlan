using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraphPlan.Models
{
	public class PlanningAction<T> //where T : ICloneable
	{
		public PlanningAction()
		{

		}

		public PlanningAction(string name, Predicate<T> validator, Action<T> executor)
		{
			this.name = name;
			this.validator = validator;
			this.executor = executor;
		}

		public string name { get; private set; }
		private readonly Predicate<T> validator;
		private readonly Action<T> executor;

		public bool CanExecute(T state)
		{
			
			return validator(state);
			
		}

		public T Execute(T state)
		{
			var newState = (T)state; //should be copied as value
			//T newState = (T)state.Clone();
			executor(newState);
			return newState;
		}

		public override string ToString()
		{
			return name; 
		}
	}
}
