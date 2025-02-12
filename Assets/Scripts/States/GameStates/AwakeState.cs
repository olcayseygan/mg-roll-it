using Assets.Scripts.Patterns.StatePattern;
using UnityEngine;

namespace Assets.Scripts.States.GameStates
{
    public class AwakeState : State<Game>
    {
        public override StateTransition<Game> OnEnter(Game self)
        {
            AudioManager.Instance.PlayBGM();
            return self.StateProvider.FindState<InitializationState>(new InitializationStateProperties() { isQuickPlayActive = false });
        }
    }
}