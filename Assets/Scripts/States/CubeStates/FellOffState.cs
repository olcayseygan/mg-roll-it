using Assets.Scripts.Patterns.StatePattern;
using UnityEngine;

namespace Assets.Scripts.States.CubeStates {
    public class FellOffState: State<Cube> {
        public override StateTransition<Cube> OnEnter(Cube self)
        {
            AudioManager.I.PlaySFX(AudioManager.I.cubeDeathClip);
            return base.OnEnter(self);
        }
    }
}