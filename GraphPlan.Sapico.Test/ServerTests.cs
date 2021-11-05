namespace GraphPlan.Sapico.Test
{
    using GraphPlan.Models;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using System.Linq;

    [TestClass]
    public class ServerTests
    {
        private Models.Server _serverOffline = new Models.Server()
        {
            snapshot_id = "snapshot-x",
            Behaviour = Models.ServerBehaviour.WhenAtWork,
            provider = "do",
            ServerState = Models.ServerState.NotFound
        };

        private GraphPlan<Models.Server> planner;

        [TestInitialize]
        public void Init()
        {
           

            var restoreServer = PlanningActions.ServerActions.Plan_RestoreServer();
            var server_online = PlanningActions.ServerActions.Plan_GoOnline();
            var restoration = PlanningActions.ServerActions.Plan_WaitingForRestoration();

            var actions = new IPlanningAction<Models.Server>[] { restoreServer, server_online, restoration };

            planner = new GraphPlan<Models.Server>(Enums.PlanningMethod.DepthFirst, actions, new ServerComparer());
        }

        internal class ServerComparer : GraphPlan.Comparer.ValueObjectComparer, IPlanningStateComparer<Models.Server>
        {
            public double Distance(Models.Server state1, Models.Server state2) => base.Distance(state1, state2);

            public bool Equals(Models.Server x, Models.Server y) => x.ServerState == y.ServerState;

            public int GetHashCode(Models.Server obj) => base.GetHashCode(obj);
        }

        [TestMethod]
        public void Scenario_Work()
        {
            var beginServerState = _serverOffline;

            var endServerState = (Models.Server)beginServerState.Clone();
            endServerState.ServerState = Models.ServerState.On;

            var actions = planner.MakePlan(beginServerState, endServerState).ToArray();

            Assert.AreEqual(actions.Count(), 3);
            Assert.IsTrue(actions[0].name == "restore_server");
            Assert.IsTrue(actions[1].name == "restoring....");
            Assert.IsTrue(actions[2].name == "go_online");
        }

      
    }
}
