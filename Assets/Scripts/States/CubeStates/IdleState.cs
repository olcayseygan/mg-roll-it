using System.Collections;
using Assets.Scripts.Patterns.StatePattern;
using UnityEngine;

namespace Assets.Scripts.States.CubeStates
{
    public class IdleState : State<Cube>
    {
        public override StateTransition<Cube> OnEnter(Cube self)
        {
            var lastVisitedPlatform = PlatformManager.I.GetPlatforms().Find(platform => platform.IsCubeOnMe());
            if (lastVisitedPlatform == null)
            {
                return self.StateProvider.FindState<FellOffState>();
            }

            self.lastVisitedPlatform = lastVisitedPlatform;
            if (Game.I.inputList.Count > 0)
            {
                Cube.I.ChangeDirection();
                Game.I.inputList.RemoveAt(0);
            }

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