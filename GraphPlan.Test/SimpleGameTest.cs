using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using GraphPlan;
using GraphPlan.Models;

namespace GraphPlan.Test
{

	//Insipiration by https://github.com/wmdmark/goap-js/blob/master/__test__/sim1.js
	[TestClass]
	public class SimpleGameTest
	{
		[TestMethod]
		public void TestSimpleGame()
		{

			var initState = new SimpleGame.SimpleGameState();

			var endState = new SimpleGame.SimpleGameState();
			endState.Player.hasAxe = false;
			endState.Player.Wood = 1;




			var plan = new GraphPlan<SimpleGame.SimpleGameState>();
			var actions = plan.Prepare()
				.AddState(new PlanningAction<SimpleGame.SimpleGameState>(
					name: "chopWood",
					conditions: x =>
					{
						return x.Player.hasAxe;
					},
					effects: x =>
					{
						Console.WriteLine("action: Chopping wood");
						x.Player.Wood++;
					}
				))
				.AddState(new PlanningAction<SimpleGame.SimpleGameState>(
					name: "getAxe",
					conditions: x =>
					{
						return !x.Player.hasAxe && x.axeAvailable;
					},
					effects: x =>
					{
						x.Player.hasAxe = true;
						Console.WriteLine("action: Get an axe");
					}
				))
				.AddState(new PlanningAction<SimpleGame.SimpleGameState>(
					name: "gatherWood",
					conditions: x => true,
					effects: x =>
					{
						x.Player.Wood++;
						Console.WriteLine("action: Gathering wood");
					}))
				.Finish()
				.Solve(initState, endState)
				.Do(initState);
		}
	}


}