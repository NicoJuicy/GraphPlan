using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using GraphPlan;
using GraphPlan.Models;
using GraphPlan.Extensions;
using System.Collections.Generic;

namespace GraphPlan.Test
{
    [TestClass]
    public class BasicTests
    {
        [TestMethod]
        public void BasicTest()
        {
            var planningActions = new List<PlanningAction<string>>()
            {
                new PlanningAction<string>(
                    name: "init",
                    conditions: x => true,
                    effects: x =>
                    {
                        x = "init";
                    }
                ),
                new PlanningAction<string>(
                    name: "execute",
                    conditions: x => x == "init",
                    effects: x =>
                    {
                        x = "finish";
                    }) };

            //var actions = plan.Prepare()
            //        .AddState().Finish()
            //        .Solve("init", "finish")
            //        .PrintToConsole();

            var plan = new GraphPlan<string>(Enums.PlanningMethod.DepthFirst, planningActions, new StringComparerer());
            var actions = plan.MakePlan("init", "finish");
            actions.PrintToConsole();
            //.Do("init")



        }

        public class StringComparerer : IPlanningStateComparer<string>
        {
            public double Distance(string state1, string state2) => 1;

            public bool Equals(string x, string y) => x.Equals(y);

            public int GetHashCode(string obj) => obj.GetHashCode();
        }
    }
}
