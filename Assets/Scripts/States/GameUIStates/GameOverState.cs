using Assets.Scripts.Patterns.StatePattern;
using UnityEngine;

namespace Assets.Scripts.States.GameUIStates
{
    public class GameOverState : State<GameUI>
    {
        public override StateTransition<GameUI> OnEnter(GameUI self)
        {
            self.gameOverPanel.SetCoinsText(PlayerController.I.GetCoins());
            self.gameOverPanel.SetScoreText(Game.I.GetCurrentRunScore());
            self.gameOverPanel.SetHighScoreText(PlayerController.I.GetHighScore());
            self.gameOverPanel.SetDoubleCoinsButtonText(Game.I.GetCurrentRunCoins());
            self.gameOverPanel.Show();
            return base.OnEnter(self);
        }

        public override void OnExit(GameUI self)
        {
            base.OnExit(self);
            self.gameOverPanel.Hide();
        }
    }
}