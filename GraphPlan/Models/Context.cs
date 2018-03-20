using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraphPlan.Models
{
	public class Context<T>
	{

		private readonly GraphPlan<T> Base;
		internal List<Models.PlanningAction<T>> PlanningActions { get; set; }

		public Context(GraphPlan<T> Base)
		{
			this.Base = Base;
			this.PlanningActions = new List<Models.PlanningAction<T>>();
		}
	

		public Context<T> AddState(Models.PlanningAction<T> State)
		{
			PlanningActions.Add(State);
			return this;
		}

		public GraphPlan<T> Finish()
		{
			return Base;
		}
	}
}
