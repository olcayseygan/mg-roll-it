namespace Assets.Scripts.Patterns.StatePattern
{
    public class StateTransition<T>
    {
        public State<T> State { get; private set; }
        public StateProperties StateProperties { get; private set; } = null;

        public StateTransition(State<T> state)
        {
            State = state;
        }
        public StateTransition(State<T> state, StateProperties properties)
        {
            State = state;
            StateProperties = properties;
        }

    }
}
