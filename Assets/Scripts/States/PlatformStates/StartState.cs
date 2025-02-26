using System.Collections;
using Assets.Scripts.Patterns.StatePattern;
using UnityEngine;

namespace Assets.Scripts.States.PlatformStates
{
    public class StartState : State<Platform>
    {
        public override StateTransition<Platform> OnEnter(Platform self)
        {
            self.transform.position = new Vector3(self.transform.position.x, -100f, self.transform.position.z);
            return base.OnEnter(self);
        }

        public override StateTransition<Platform> Update(Platform self)
        {
            var newY = self.transform.position.y + 100f * Time.deltaTime;
            newY = Mathf.Min(newY, 0f);
            self.transform.position = new Vector3(self.transform.position.x, newY, self.transform.position.z);
            if (self.transform.position.y >= 0f)
            {
                return self.StateProvider.FindState<IdleState>();
            }

            return base.Update(self);
        }
    }
}