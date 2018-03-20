using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using GraphPlan;
namespace GraphPlan.Test
{
	[TestClass]
	public class Basic
	{
		[TestMethod]
		public void TestMethod1()
		{
			var plan = new GraphPlan<string>();

			var actions = plan.Prepare()
				.AddState(new Models.PlanningAction<string>(
					name: "init",
					executor: x =>
				   {
					   Console.WriteLine("init");
				   },
					validator: x => true
				)).Finish()
				.Solve("init", "finish")
				.PrintToConsole();


		}
	}
}
