using System.Collections;
using Assets.Scripts.Patterns.StatePattern;
using UnityEngine;

namespace Assets.Scripts.States.CubeStates
{
    public class IdleState : State<Cube>
    {
        public override StateTransition<Cube> OnEnter(Cube self)
        {
            if (Game.I.playerHasInteracted) {
                self.ChangeDirection();
            }

            Game.I.playerHasInteracted = false;
            return base.OnEnter(self);
        }


        public override StateTransition<Cube> Update(Cube self)
        {
            if (!Game.I.StateProvider.IsInState<GameStates.PlayingState>())
            {
                return base.Update(self);
            }

            return self.StateProvider.FindState<MotionState>();
        }
    }
}