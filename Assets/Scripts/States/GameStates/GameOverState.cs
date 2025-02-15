using Assets.Scripts.Patterns.StatePattern;
using UnityEngine;

namespace Assets.Scripts.States.GameStates
{
    public class GameOverState : State<Game>
    {
        public override StateTransition<Game> OnEnter(Game self)
        {
            if (Game.I.GetScore() > Game.I.GetHighScore())
            {
                Game.I.SetHighScore(Game.I.GetScore());
            }

            if (self.canContinue)
            // if (self.canContinue && AdsInitializer.I.isInitialized)
            {
                GameUI.I.gameOverPanel.ShowWatchAdButton();
            }
            else
            {
                GameUI.I.gameOverPanel.HideWatchAdButton();
            }

            GameUI.I.gameOverPanel.HideContinueButton();
            GameUI.I.gameOverPanel.SetScoreText(self.GetScore());
            GameUI.I.gameOverPanel.SetHighScoreText(self.GetHighScore());
            GameUI.I.mainMenuPanel.SetHighScoreText(self.GetHighScore());
            GameUI.I.gameOverPanel.Show();
            return base.OnEnter(self);
        }

        public override void OnExit(Game self)
        {
            base.OnExit(self);
            GameUI.I.gameOverPanel.Hide();
        }
    }
}