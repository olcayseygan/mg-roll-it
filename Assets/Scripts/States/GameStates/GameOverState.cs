using Assets.Scripts.Patterns.StatePattern;
using Assets.Scripts.StateViews;
using UnityEngine;

namespace Assets.Scripts.States.GameStates
{
    public class GameOverState : State<Game>
    {
        public override StateTransition<Game> OnEnter(Game self)
        {
            PlayerController.I.SetHighScoreIfHigher(self.GetCurrentRunScore());
            PlayerController.I.AddGold(self.GetCurrentRunGolds());
            self.SetCurrentRunHighScore(PlayerController.I.GetHighScore());
            Game.I.StateViewHandler.SwitchTo<GameOverPanel>();
            return base.OnEnter(self);
        }
    }
}