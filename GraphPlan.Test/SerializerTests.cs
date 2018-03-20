using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using GraphPlan;
using GraphPlan.Models;

namespace GraphPlan.Test
{
	[TestClass]
	public class SerializerTests
	{
		string file = System.IO.Path.GetTempFileName();

		[TestMethod]
		public void Save()
		{
			var plan = new GraphPlan<string>();

			var actions = plan.Prepare()
				.AddState(new PlanningAction<string>(
					name: "init",
					preconditions: x => true,
					effects: x =>
				   {
					   Console.WriteLine("Do init");
				   }
				))
				.AddState(new PlanningAction<string>(
					name: "execute",
					preconditions: x => true,
					effects: x =>
					{
						Console.WriteLine("Do Execute");
					}
				)).Finish()
				.Solve("init", "finish")
				.Save(file);


			Assert.IsTrue(System.IO.File.Exists(file));

		}

		[TestMethod]
		public void Load()
		{


			var plan = new GraphPlan<string>();
			var actions = plan.Prepare()
				.AddState(new PlanningAction<string>(
					name: "init",
					preconditions: x => true,
					effects: x =>
					{
						Console.WriteLine("Do init");
					}
				))
				.AddState(new PlanningAction<string>(
					name: "execute",
					preconditions: x => true,
					effects: x =>
					{
						Console.WriteLine("Do Execute");
					}
				)).Finish()
				.Load(file)
				.Do("init");
		}
	}
}
