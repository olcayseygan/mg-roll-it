using System.Collections;
using Assets.Scripts.Patterns.StatePattern;
using UnityEngine;

namespace Assets.Scripts.States.PlatformStates
{
    public class DestroyState : State<Platform>
    {
        public override StateTransition<Platform> Update(Platform self)
        {
            self.transform.position = new Vector3(self.transform.position.x, self.transform.position.y - 1f, self.transform.position.z);
            return base.Update(self);
        }
    }
}