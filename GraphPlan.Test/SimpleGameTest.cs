using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using GraphPlan;
using GraphPlan.Models;
using System.Diagnostics;
using System.Collections.Generic;

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
            endState.Player.Wood = 20;
            endState.axeAvailable = false;

            var planningActions = new List<PlanningAction<SimpleGame.SimpleGameState>>()
            {
                new PlanningAction<SimpleGame.SimpleGameState>(
                    name: "chopWood",
                    conditions: x =>
                    {
                        return x.Player.hasAxe;
                    },
                    effects: x =>
                    {
                        x.Player.Wood++;
                    if (x.Player.axeLife >0) x.Player.axeLife--;
                    }
                ),
                new PlanningAction<SimpleGame.SimpleGameState>(
                    name: "getAxe",
                    conditions: x =>
                    {
                        return !x.Player.hasAxe && x.axeAvailable;
                    },
                    effects: x =>
                    {
                        x.Player.hasAxe = true;
                        x.axeAvailable = false;
                    }
                ),
                new PlanningAction<SimpleGame.SimpleGameState>(
                    name: "createAxe",
                    conditions: x =>
                    {
                        return x.Player.Wood >= 5;
                    },
                    effects: x =>
                    {
                        x.Player.hasAxe = true;
                        x.Player.Wood -= 2;
                    }
                )
            };

            var plan = new GraphPlan<SimpleGame.SimpleGameState>(Enums.PlanningMethod.DepthFirst, planningActions, new GameStateComparer());

            //plan.SetComparer(new GameStateComparer());
            //var actions = plan.Prepare()
            //	.AddState(new PlanningAction<SimpleGame.SimpleGameState>(
            //		name: "chopWood",
            //		conditions: x =>
            //		{
            //			return x.Player.hasAxe;
            //		},
            //		effects: x =>
            //		{
            //			x.Player.Wood++;
            //		if (x.Player.axeLife >0) x.Player.axeLife--;
            //		}
            //	))
            //	.AddState(new PlanningAction<SimpleGame.SimpleGameState>(
            //		name: "getAxe",
            //		conditions: x =>
            //		{
            //			return !x.Player.hasAxe && x.axeAvailable;
            //		},
            //		effects: x =>
            //		{
            //			x.Player.hasAxe = true;
            //			x.axeAvailable = false;
            //                 }
            //	))
            //             .AddState(new PlanningAction<SimpleGame.SimpleGameState>(
            //                 name: "createAxe",
            //                 conditions: x =>
            //                 {
            //                     return x.Player.Wood >= 5;
            //                 },
            //                 effects: x =>
            //                 {
            //                     x.Player.hasAxe = true;
            //                     x.Player.Wood -= 2;
            //                 }
            //             ))
            //.AddState(new PlanningAction<SimpleGame.SimpleGameState>(
            //    name: "gatherWood",
            //    conditions: x => true,
            //    effects: x =>
            //    {
            //        x.Player.Wood++;
            //    }))
            //            .Finish()
            //.Solve(initState, endState)
            //.Do(initState);

            var actions = plan.MakePlan(initState, endState);
            actions.Do(initState);
        }
    }

    public class GameStateComparer : IPlanningStateComparer<SimpleGame.SimpleGameState>
    {
        public double Distance(SimpleGame.SimpleGameState state1, SimpleGame.SimpleGameState state2) => (state1?.Player.Wood - state2?.Player.Wood) ?? 10;

        public bool Equals(SimpleGame.SimpleGameState x, SimpleGame.SimpleGameState y)
        {
            return (x?.Player.Wood == y?.Player.Wood);

        }

        public int GetHashCode(SimpleGame.SimpleGameState obj)
        {
            return obj.GetHashCode();
        }
    }


}