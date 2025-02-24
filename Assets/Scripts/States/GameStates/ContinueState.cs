using System.Collections;
using Assets.Scripts.Patterns.StatePattern;
using UnityEngine;

namespace Assets.Scripts.States.GameStates
{
    public class ContinueState : State<Game>
    {
        public override StateTransition<Game> OnEnter(Game self)
        {
            self.StartCoroutine(RestartGame(self));
            return base.Update(self);
        }

        // IMPORTANT: Burayi silme, bura cokomelli.
        private IEnumerator RestartGame(Game self)
        {
            self.isContinuationEnabled = false;
            Cube.I.StateProvider.SwitchTo<CubeStates.RevivalState>();
            yield return null;
            self.StateProvider.SwitchTo<PlayingState>(new PlayingStateProperties() { isFreshRun = false });
        }
    }
}