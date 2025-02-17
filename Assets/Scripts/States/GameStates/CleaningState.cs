using System.Collections;
using Assets.Scripts.Patterns.StatePattern;
using UnityEngine;

namespace Assets.Scripts.States.GameStates
{
    public class CleaningStateProperties : StateProperties
    {
        public bool canSkipToPlaying;
    }

    public class CleaningState : State<Game, CleaningStateProperties>
    {
        public override StateTransition<Game> OnEnter(Game self, CleaningStateProperties properties)
        {
            self.StartCoroutine(CleanScene(self, properties));
            return base.Update(self);
        }

        // IMPORTANT: Burayi silme, bura cokomelli.
        private IEnumerator CleanScene(Game self, CleaningStateProperties properties)
        {
            PlatformManager.I.ClearPlatforms();
            Object.Destroy(Cube.I.gameObject);
            yield return null;
            self.StateProvider.SwitchTo<InstantiationState>(new InstantiationStateProperties() { canSkipToPlaying = properties.canSkipToPlaying });
        }
    }
}