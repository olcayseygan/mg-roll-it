using Assets.Scripts.Patterns.StatePattern;
using UnityEngine;

namespace Assets.Scripts.States.GameStates
{
    public class GameOverState : State<Game>
    {

        public override StateTransition<Game> OnEnter(Game self)
        {
            PlayerController.I.SetHighScoreIfHigher(self.GetCurrentRunScore());
            PlayerController.I.AddCoins(self.GetCurrentRunCoins());
            self.SetCurrentRunHighScore(PlayerController.I.GetHighScore());
            if (self.isContinuationEnabled && (true || (Game.I.rewardedAd != null && Game.I.rewardedAd.CanShowAd())))
            {
                GameUI.I.gameOverPanel.ShowContinueButton();
            }
            else
            {
                GameUI.I.gameOverPanel.HideContinueButton();
            }

            if (self.GetCurrentRunCoins() > 0 || (Game.I.rewardedAd != null && Game.I.rewardedAd.CanShowAd()))
            {
                GameUI.I.gameOverPanel.ShowDoubleCoinsButton();
            }
            else
            {
                GameUI.I.gameOverPanel.HideDoubleCoinsButton();
            }

            GameUI.I.StateProvider.SwitchTo<GameUIStates.GameOverState>();
            return base.OnEnter(self);
        }
    }
}