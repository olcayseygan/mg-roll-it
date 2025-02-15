using System.Collections;
using Assets.Scripts.Patterns.StatePattern;
using UnityEngine;

namespace Assets.Scripts.States.CubeStates
{
    public class WaitForActionState : State<Cube>
    {
        public override StateTransition<Cube> OnEnter(Cube self)
        {
            Debug.Log("WaitForActionState");
            return base.OnEnter(self);
        }

        public override StateTransition<Cube> Update(Cube self)
        {
            if (Input.GetMouseButtonDown(0))
            {
                Debug.Log("WaitForActionState -> IdleState");
                self.StateProvider.SwitchTo<IdleState>();
            }

            return base.Update(self);
        }
    }
}