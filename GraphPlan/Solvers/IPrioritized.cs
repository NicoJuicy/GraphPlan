using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraphPlan.Solvers
{
	internal interface IPrioritized<P, V>
	{
		bool HasElements { get; }
		void Add(P priority, V value);
		V Get();
	}
}
