using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using GraphPlan;
using GraphPlan.Models;
using GraphPlan.Extensions;
namespace GraphPlan.Test
{
	[TestClass]
	public class BasicTests
	{
		[TestMethod]
		public void BasicTest()
		{
			var plan = new GraphPlan<string>();

			var actions = plan.Prepare()
				.AddState(new PlanningAction<string>(
					name: "init",
					conditions: x => true,
					effects: x =>
				   {
                       x = "init";
				   }
				))
				.AddState(new PlanningAction<string>(
					name: "execute",
					conditions: x => x == "init",
					effects: x =>
					{
                        x = "finish";
					}
				)).Finish()
				.Solve("init", "finish")
                .PrintToConsole();
				//.Do("init")
				


		}
	}
}
