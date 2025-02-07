using Assets.Scripts.Patterns.StatePattern;

namespace Assets.Scripts.States.CubeStates {
    public class IdleState: State<Cube> {
        public override StateTransition<Cube> OnEnter(Cube self)
        {
            return self.StateProvider.FindState<MotionState>();
            return base.OnEnter(self);
        }
    }
}