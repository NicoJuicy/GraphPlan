using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using GraphPlan;
namespace GraphPlan.Test
{
	[TestClass]
	public class Serializer
	{
		string file = System.IO.Path.GetTempFileName();

		[TestMethod]
		public void Save()
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
				.Save(file);


			Assert.IsTrue(System.IO.File.Exists(file));

		}

		[TestMethod]
		public void Load()
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
				.Load(file)
				.Do("init");
		}
	}
}
