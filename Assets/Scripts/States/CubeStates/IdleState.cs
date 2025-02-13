using System.Collections;
using Assets.Scripts.Patterns.StatePattern;
using UnityEngine;

namespace Assets.Scripts.States.CubeStates
{
    public class IdleState : State<Cube>
    {
        public override StateTransition<Cube> OnEnter(Cube self)
        {
            bool isGrounded = false;
            foreach (var platform in PlatformManager.Instance.GetPlatforms())
            {
                if (platform.IsCubeOnMe())
                {
                    isGrounded = true;
                    self.lastPlatform = platform;
                    break;
                }
            }

            if (!isGrounded)
            {
                return self.StateProvider.FindState<DeathState>();
            }

            return base.OnEnter(self);
        }


        public override StateTransition<Cube> Update(Cube self)
        {
            if (Game.Instance.isPaused)
            {
                return base.Update(self);
            }

            return self.StateProvider.FindState<MotionState>();
        }
    }
}