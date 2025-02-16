using System.Collections;
using Assets.Scripts.Patterns.StatePattern;
using UnityEngine;

namespace Assets.Scripts.States.GameStates
{
    public class ContinueState : State<Game>
    {
        public override StateTransition<Game> OnEnter(Game self)
        {
            self.canContinue = false;
            Cube.I.StateProvider.SwitchTo<CubeStates.RevivalState>();
            GameUI.I.StateProvider.SwitchTo<GameUIStates.ContinueState>();
            self.StartCoroutine(RestartGame(self));
            return base.Update(self);
        }

        // IMPORTANT: Burayi silme, bura cokomelli.
        private IEnumerator RestartGame(Game self)
        {
            yield return null;
            self.StateProvider.SwitchTo<PlayingState>();
        }
    }
}