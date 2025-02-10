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
                    break;
                }
            }

            if (!isGrounded)
            {
                return self.StateProvider.FindState<DeathState>();
            }

            Debug.Log("Idle state");
            return self.StateProvider.FindState<MotionState>();
        }
    }
}