using System;
using System.Collections.Generic;

namespace Assets.Scripts.Patterns.StatePattern
{
    public class StateProvider<MemberType>
    {
        private MemberType Member { get; }
        private readonly Dictionary<Type, State<MemberType>> States = new();
        private State<MemberType> _currentState = null;

        public List<State<MemberType>> GetStates() => new(States.Values);
        public bool IsInState<StateType>() where StateType : State<MemberType> => _currentState.GetType() == typeof(StateType);

        public StateProvider(MemberType member)
        {
            Member = member;
        }

        public void RegisterState(State<MemberType> state)
        {
            var stateType = state.GetType();
            if (States.ContainsKey(stateType))
                return;
            state.Provider = this;
            States[stateType] = state;
        }

        public State<MemberType> GetState<StateType>() where StateType : State<MemberType>
        {
            var stateType = typeof(StateType);
            if (States.ContainsKey(stateType))
                return States[stateType];
            return null;
        }

        public StateTransition<MemberType> FindState<StateType>() where StateType : State<MemberType> => new(GetState<StateType>());
        public StateTransition<MemberType> FindState<StateType>(StateProperties properties) where StateType : State<MemberType> => new(GetState<StateType>(), properties);
        public StateTransition<MemberType> FindState() => new(_currentState);

        public void SwitchTo<StateType>() where StateType : State<MemberType> => ChangeState(GetState<StateType>());
        public void SwitchTo<StateType>(StateProperties properties) where StateType : State<MemberType> => ChangeState(GetState<StateType>(), properties);

        public void ChangeState(State<MemberType> state, StateProperties properties = null)
        {
            _currentState?.OnExit(Member);
            var transition = properties != null ? state.OnEnter(Member, properties) : state.OnEnter(Member);
            _currentState = transition.State;
            if (transition.State != state)
                ChangeState(transition.State, transition.StateProperties);
        }

        public void FixedUpdate() => UpdateState(_currentState.FixedUpdate(Member));
        public void Update() => UpdateState(_currentState.Update(Member));
        public void LateUpdate() => UpdateState(_currentState.LateUpdate(Member));

        public void UpdateState(StateTransition<MemberType> transition)
        {
            if (transition.State == _currentState)
                return;

            if (transition.StateProperties != null)
                ChangeState(transition.State, transition.StateProperties);
            else
                ChangeState(transition.State);
        }
    }
}
