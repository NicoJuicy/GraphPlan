namespace GraphPlan.Sapico.Test
{
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using System;
    using System.Collections.Generic;
    using System.Linq;

    [TestClass]
    public class HQTests
    {
        internal Func<Models.HQState, Models.HQState> AtWork = (beginState) =>
        {
            var endState = (Models.HQState)beginState.Clone();

            if (endState.Employees.Any(d => d.IsAtWork))
            {
                foreach (var workServer in endState.Servers.Where(el => el.Behaviour.HasFlag(Models.ServerBehaviour.WhenAtWork)))
                {
                    workServer.ServerState = Models.ServerState.On;
                }

            }
            return endState;
        };

        private GraphPlan<Models.HQState> planner;

        [TestInitialize]
        public void Init()
        {


            var restoreServer = PlanningActions.ServerActions.Plan_RestoreServer();
            var server_online = PlanningActions.ServerActions.Plan_GoOnline();
        }

        [TestMethod]
        public void NicoArrivesAtWork()
        {
            var beginState = new Models.HQState()
            {
                Employees = new List<Models.Employee>()
                {
                    new Models.Employee()
                    {
                         IsAtWork = true,
                            Name = "nico"
                    }

                }
            ,
                Servers = new List<Models.Server>()
                {
                    new Models.Server()
                    {
                        Behaviour = Models.ServerBehaviour.WhenAtWork,
                        ServerState = Models.ServerState.NotFound,
                        snapshot_id = "snapshot_x",
                        provider = "digitalocean",
                        ServerName = "server-AtWork"
                    },
                    new Models.Server()
                    {
                        Behaviour = Models.ServerBehaviour.AlwaysOn,
                        ServerState = Models.ServerState.On,
                        snapshot_id = "snapshot_x",
                        provider = "digitalocean",
                        ServerName = "server-AlwaysOn"
                    },

                }
            };


            var desiredEndState = AtWork(beginState);

            var actions = planner.MakePlan(beginState, desiredEndState);

            string t = "";
        }
    }
}
