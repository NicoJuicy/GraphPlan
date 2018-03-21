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
		internal List<Models.IPlanningAction<T>> PlanningActions { get; set; }

		public Context(GraphPlan<T> Base)
		{
			this.Base = Base;
			this.PlanningActions = new List<Models.IPlanningAction<T>>();
		}
	

		public Context<T> AddState(Models.IPlanningAction<T> State)
		{
			PlanningActions.Add(State);
			return this;
		}

        public Context<T> AddStates(List<Models.IPlanningAction<T>> States)
        {
            PlanningActions.AddRange(States);
            return this;
        }


        public GraphPlan<T> Finish()
		{
			return Base;
		}
	}
}
