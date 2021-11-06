namespace GraphPlan.Models
{
    using System;

    public interface IPlanningAction
    {
        string name { get; }
    }
    public interface IPlanningAction<T> : IPlanningAction
    {
        bool CanExecute(T state);

        T Execute(T state);
    }

    [Serializable]
    public class PlanningAction<T> : IPlanningAction<T>,IPlanningAction where T : ICloneable
    {
        private readonly Predicate<T> conditions;

        private readonly Action<T> effects;

        public PlanningAction()
        {
        }

        public PlanningAction(string name, Predicate<T> conditions, Action<T> effects)
        {
            this.name = name;
            this.conditions = conditions;
            this.effects = effects;
        }

        public string name { get; private set; }

        public bool CanExecute(T state)
        {
            return conditions(state);
        }

        public T Execute(T state)
        {
            var newState = (T)state.Clone();
            effects(newState);
            return newState;
        }

        public override string ToString()
        {
            return name;
        }
    }
}
