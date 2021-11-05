namespace GraphPlan.Sapico.Test.PlanningActions
{
    using GraphPlan.Models;

    public static class ServerActions
    {
        public static PlanningAction<Models.Server> Plan_GoOnline()
        {
            return new PlanningAction<Models.Server>(
             name: "go_online",
             conditions: (s) =>
             {
                 return s.ServerState == Models.ServerState.NotFound && !string.IsNullOrEmpty(s.snapshot_id);
             },
             effects: s =>
             {
                 s.ServerState = Models.ServerState.Restoring;

             }
             );
        }

        public static PlanningAction<Models.Server> Plan_RestoreServer()
        {
            return new PlanningAction<Models.Server>(
                name: "restore_server",
                conditions: (s) =>
                {
                    return s.ServerState == Models.ServerState.NotFound && !string.IsNullOrEmpty(s.snapshot_id);
                },
                effects: s =>
                {
                    s.ServerState = Models.ServerState.Restoring;

                }
                );
        }
    }
}
