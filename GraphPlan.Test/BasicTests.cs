using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using GraphPlan;
namespace GraphPlan.Test
{
	[TestClass]
	public class BasicTests
	{
		[TestMethod]
		public void GraphPlanTest()
		{
			var plan = new GraphPlan<string>();

			var actions = plan.Prepare()
				.AddState(new Models.PlanningAction<string>(
					name: "init",
					preconditions: x => true,
					effects: x =>
				   {
					   Console.WriteLine("Do init");
				   }
				))
				.AddState(new Models.PlanningAction<string>(
					name: "execute",
					preconditions: x => true,
					effects: x =>
					{
						Console.WriteLine("Do Execute");
					}
				)).Finish()
				.Solve("init", "finish")
				.Do("init");
				//.PrintToConsole();


		}
	}
}
