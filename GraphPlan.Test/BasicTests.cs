using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using GraphPlan;
using GraphPlan.Models;

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
				.AddState(new PlanningAction<string>(
					name: "init",
					conditions: x => true,
					effects: x =>
				   {
					   Console.WriteLine("Do init");
				   }
				))
				.AddState(new PlanningAction<string>(
					name: "execute",
					conditions: x => true,
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
