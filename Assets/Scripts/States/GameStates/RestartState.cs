using System.Collections;
using Assets.Scripts.Patterns.StatePattern;
using UnityEngine;

namespace Assets.Scripts.States.GameStates
{
    public class RestartState : State<Game>
    {
        public override StateTransition<Game> OnEnter(Game self)
        {
            GameOverPanel.Instance.Hide();
            PlatformManager.Instance.ClearPlatforms();
            Object.Destroy(Cube.Instance.gameObject);
            Debug.Log("Restarting game");
            self.StartCoroutine(RestartGame(self));
            return base.Update(self);
        }

        // IMPORTANT: Burayi silme, bura cokomelli.
        private IEnumerator RestartGame(Game self)
        {
            yield return null;
            self.StateProvider.SwitchTo<InitializationState>();
        }
    }
}