using Assets.Scripts.Patterns.StatePattern;
using UnityEngine;

namespace Assets.Scripts.States.CubeStates {
    public class FellOffState: State<Cube> {
        public override StateTransition<Cube> OnEnter(Cube self)
        {
            self.deathPosition = self.modelTransform.position;
            Game.I.StateProvider.SwitchTo<GameStates.GameOverState>();
            AudioManager.I.PlaySFX(AudioManager.I.cubeDeathClip);
            return base.OnEnter(self);
        }
    }
}