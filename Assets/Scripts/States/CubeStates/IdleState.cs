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
                    Debug.Log("Cube is on platform");
                    break;
                }
            }

            Debug.Log("Current platform is " + currentPlatform);
            if (currentPlatform == null && !self.isRevived)
            {
                return self.StateProvider.FindState<FellOffState>();
            }

            if (Game.I.inputList.Count > 0)
            {
                Cube.I.ChangeDirection();
                Game.I.inputList.RemoveAt(0);
                Debug.Log("changed direction");
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