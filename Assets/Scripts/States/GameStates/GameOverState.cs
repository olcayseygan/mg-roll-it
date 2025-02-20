using Assets.Scripts.Patterns.StatePattern;
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
            if (self.isContinuationEnabled && (true || (Game.I.rewardedAd != null && Game.I.rewardedAd.CanShowAd())))
            {
                GameUI.I.gameOverPanel.ShowContinueButton();
            }
            else
            {
                GameUI.I.gameOverPanel.HideContinueButton();
            }

            if (self.GetCurrentRunGolds() > 0 || (Game.I.rewardedAd != null && Game.I.rewardedAd.CanShowAd()))
            {
                GameUI.I.gameOverPanel.ShowDoubleGoldButton();
            }
            else
            {
                GameUI.I.gameOverPanel.HideDoubleGoldButton();
            }

            GameUI.I.StateProvider.SwitchTo<GameUIStates.GameOverState>();
            return base.OnEnter(self);
        }
    }
}