using System.Collections;
using Assets.Scripts.Patterns.StatePattern;
using UnityEngine;

namespace Assets.Scripts.States.GameStates
{
    public class RestartStateProperties : StateProperties
    {
        public bool isQuickPlayActive;
    }

    public class RestartState : State<Game, RestartStateProperties>
    {
        public override StateTransition<Game> OnEnter(Game self, RestartStateProperties properties)
        {
            PlatformManager.I.ClearPlatforms();
            Object.Destroy(Cube.I.gameObject);
            Game.I.SetScore(0);
            self.StartCoroutine(RestartGame(self, properties));
            return base.Update(self);
        }

        // IMPORTANT: Burayi silme, bura cokomelli.
        private IEnumerator RestartGame(Game self, RestartStateProperties properties)
        {
            yield return null;
            self.StateProvider.SwitchTo<InitializationState>(new InitializationStateProperties() { isQuickPlayActive = properties.isQuickPlayActive });
        }
    }
}