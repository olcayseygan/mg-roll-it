using Assets.Scripts.Patterns.StatePattern;
using UnityEngine;

namespace Assets.Scripts.States.CubeStates {
    public class DeathState: State<Cube> {
        public override StateTransition<Cube> OnEnter(Cube self)
        {
            self.deathPosition = self.modelTransform.position;
            self.modelRigidbody.isKinematic = false;
            self.modelRigidbody.useGravity = true;
            Game.Instance.StateProvider.SwitchTo<GameStates.GameOverState>();
            return base.OnEnter(self);
        }
    }
}