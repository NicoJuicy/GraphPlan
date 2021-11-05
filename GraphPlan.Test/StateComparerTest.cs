using System;
using System.Collections.Generic;
using System.Linq;
using GraphPlan.Models;
using GraphPlan.Test.SimpleState;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using GraphPlan.Extensions;

namespace GraphPlan.Test
{
    [TestClass]
    public class StateComparerTest : BaseContext
    {
        [TestMethod]
        public void SimpleState_hanoi_tower_solver_test()
        {
            //A1 1  B1 |  C1 |
            //A2 2  B2 |  C2 |
            //A3 3  B3 |  C3 |
            //A4 4  B4 |  C4 |
            //A5 5  B5 |  C5 |
            //A6 6  B6 |  C6 |
            //A7 7  B7 |  C7 |
            //  / \   / \   / \

            var initialState = new State
            {
                {"A1", 1},
                {"A2", 2},
                {"A3", 3},
                {"A4", 4},
                {"A5", 5},
                {"A6", 6},
                {"A7", 7},
            };

            var goalState = new State
            {
                {"C1", 1},
                {"C2", 2},
                {"C3", 3},
                {"C4", 4},
                {"C5", 5},
                {"C6", 6},
                {"C7", 7},
            };

            var planningActions = new List<IPlanningAction<State>>
            {
                new PlanningAction<State>(
                    name: "Move from A to B",
                    conditions: state => Validate(state, "A", "B"),
                    effects: state => Move(state, "A", "B")),
                new PlanningAction<State>(
                    name: "Move from A to C",
                    conditions: state => Validate(state, "A", "C"),
                    effects: state => Move(state, "A", "C")),
                new PlanningAction<State>(
                    name: "Move from B to A",
                    conditions: state => Validate(state, "B", "A"),
                    effects: state => Move(state, "B", "A")),
                new PlanningAction<State>(
                    name: "Move from B to C",
                    conditions: state => Validate(state, "B", "C"),
                    effects: state => Move(state, "B", "C")),
                new PlanningAction<State>(
                    name: "Move from C to A",
                    conditions: state => Validate(state, "C", "A"),
                    effects: state => Move(state, "C", "A")),
                new PlanningAction<State>(
                    name: "Move from C to B",
                    conditions: state => Validate(state, "C", "B"),
                    effects: state => Move(state, "C", "B")),
            };


            var plan = new GraphPlan<State>()
                .SetComparer(new StateComparer())
                .SetSearchMethod(Enums.SearchMethod.DepthFirst)
                .Prepare(planningActions.ToArray())
                .Solve(initialState, goalState)
                .PrintToConsole();

        }

        [TestMethod]
        public void SimpleState_WhenGoalReachable()
        {
            var planner = BaseContext.CreatePlanner();
            var initialState = new State
            {
                {"1" , 3},
                {"2" , 6},
            };
            var goalState = new State
            {
                {"1" , 5},
                {"2" , 4},
            };

            var plan = planner.Solve(initialState, goalState);

            Assert.AreEqual(plan.Count(), 2,"Plan should contain 2 steps");

        }

        [TestMethod]
        public void SimpleState_when_goal_unreachable()
        {
            var planner = BaseContext.CreatePlanner();
            var initialState = new State
            {
                {"1" , 3},
                {"2" , 6},
            };
            var goalState = new State
            {
                {"1" , 5},
                {"2" , 5},
            };

            var plan = planner.Solve(initialState, goalState);

            Assert.AreEqual(plan.Count(), 0, "Plan should be empty 2 steps");

        }

        private static bool Validate(State x, string srcName, string dstName)
        {
            var srcTop = 0;
            var srcTrigger = false;
            var dstTop = 0;
            var dstTrigger = false;

            for (var i = 1; i < 8; i++)
            {
                if (!srcTrigger) srcTrigger = x.TryGetValue(srcName + i, out srcTop);
                if (!dstTrigger) dstTrigger = x.TryGetValue(dstName + i, out dstTop);
            }

            return srcTrigger && (!dstTrigger || dstTop > srcTop);
        }

        private static void Move(State state, string srcName, string dstName)
        {
            var srcPosition = state
                .Where(_ => _.Key.StartsWith(srcName))
                .Select(_ => _.Key.Remove(0, 1))
                .Select(int.Parse)
                .Min();

            var dstPosition = state
                .Where(_ => _.Key.StartsWith(dstName))
                .Select(_ => _.Key.Remove(0, 1))
                .Select(int.Parse)
                .ToList();

            var a = srcName + srcPosition;
            var dstNormalized = dstPosition.Count == 0 ? 7 : dstPosition.Min() - 1;
            var b = dstNormalized == 0 ? "" : dstName + dstNormalized;

            state.Add(b, state[a]);
            state.Remove(a);
        }

    }

    public class BaseContext
    {
        protected static GraphPlan<State> CreatePlanner()
        {
            var planningActions = new List<IPlanningAction<State>>
                {
                    new PlanningAction<State>(
                        name: "swap 1 with 2",
                        conditions: x => x["1"] > 1,
                        effects: x =>
                            {
                                x["1"] -= 1;
                                x["2"] += 1;
                            }),
                    new PlanningAction<State>(
                        name:"swap 2 with 1",
                        conditions: x => x["2"] > 1,
                        effects: x =>
                            {
                                x["1"] += 1;
                                x["2"] -= 1;
                            }),
                };
            var stateComparer = new StateComparer();

            return new GraphPlan<State>().Prepare()
                .AddStates(planningActions.ToArray())
                .Finish()
                .SetComparer(stateComparer);
        }
    }

}
