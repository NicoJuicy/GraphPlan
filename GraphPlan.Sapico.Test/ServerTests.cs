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
            planner = new GraphPlan<Models.Server>();

            var restoreServer = PlanningActions.ServerActions.Plan_RestoreServer();
            var server_online = PlanningActions.ServerActions.Plan_GoOnline();
            planner.Prepare(new IPlanningAction<Models.Server>[] { restoreServer, server_online });
        }

        [TestMethod]
        public void Scenario_Work()
        {
            var beginServerState = _serverOffline;

            var endServerState = (Models.Server)beginServerState.Clone();
            endServerState.ServerState = Models.ServerState.On;

            var actions = planner.Solve(beginServerState, endServerState);

            Assert.AreEqual(actions.Count(), 2);
            Assert.IsTrue(actions[0].name == "restore_server");
            Assert.IsTrue(actions[1].name == "go_online");
        }
    }
}
