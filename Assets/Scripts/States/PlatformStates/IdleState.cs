using System.Collections;
using Assets.Scripts.Patterns.StatePattern;
using UnityEngine;

namespace Assets.Scripts.States.PlatformStates
{
    public class IdleState : State<Platform>
    {
        public override StateTransition<Platform> OnEnter(Platform self)
        {
            self.transform.position = new Vector3(self.transform.position.x, 0f, self.transform.position.z);
            return base.OnEnter(self);
        }
    }
}