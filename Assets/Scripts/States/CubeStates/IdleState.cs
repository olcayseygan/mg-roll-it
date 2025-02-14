using System.Collections;
using Assets.Scripts.Patterns.StatePattern;
using UnityEngine;

namespace Assets.Scripts.States.CubeStates
{
    public class IdleState : State<Cube>
    {
        public override StateTransition<Cube> OnEnter(Cube self)
        {
            Platform currentPlatform = null;
            foreach (var platform in Game.I.GetPlatforms())
            {
                if (platform.IsCubeOnMe())
                {
                    currentPlatform = platform;
                    self.lastPlatform = platform;
                    self.isRevived = false;
                    break;
                }
            }

            if (currentPlatform == null && !self.isRevived)
            {
                return self.StateProvider.FindState<FellOffState>();
            }

            return base.OnEnter(self);
        }


        public override StateTransition<Cube> Update(Cube self)
        {
            if (Game.I.isPaused)
            {
                return base.Update(self);
            }

            return self.StateProvider.FindState<MotionState>();
        }
    }
}