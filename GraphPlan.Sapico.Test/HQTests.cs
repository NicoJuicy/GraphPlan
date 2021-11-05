using GraphPlan.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraphPlan.Sapico.Test
{
    [TestClass]
    public class HQTests
    {
        private GraphPlan<Models.HQState> planner;

        [TestInitialize]
        public void Init()
        {
            planner = new GraphPlan<Models.HQState>();

            var restoreServer = PlanningActions.ServerActions.Plan_RestoreServer();
            var server_online = PlanningActions.ServerActions.Plan_GoOnline();

            // planner.Prepare(new IPlanningAction<Models.Server>[] { restoreServer, server_online });
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

            var actions = planner.Solve(beginState, desiredEndState);

            string t = "";
        }


        Func<Models.HQState, Models.HQState> AtWork = (beginState) =>
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

    }
}
