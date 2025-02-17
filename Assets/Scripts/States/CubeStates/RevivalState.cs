using System.Collections;
using Assets.Scripts.Patterns.StatePattern;
using UnityEngine;

namespace Assets.Scripts.States.CubeStates {
    public class RevivalState : State<Cube> {
        public override StateTransition<Cube> OnEnter(Cube self)
        {
            self.transform.SetPositionAndRotation(self.lastVisitedPlatform.transform.position, Quaternion.identity);
            self.modelTransform.SetLocalPositionAndRotation(Vector3.up, Quaternion.identity);
            var platforms = PlatformManager.I.GetPlatforms();
            var index = platforms.IndexOf(self.lastVisitedPlatform);
            var direction = platforms[index + 1].transform.position - self.lastVisitedPlatform.transform.position;
            self.direction = direction.normalized;
            self.lastKnownPosition = self.lastVisitedPlatform.transform.position;
            return self.StateProvider.FindState<WaitForActionState>();
        }
    }
}