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
            if (self.isContinuationEnabled && LevelPlayManager.I.CheckRewardedVideoAvailability("Game_Over__Continue"))
            {
                GameUI.I.gameOverPanel.ShowContinueButton();
            }
            else
            {
                GameUI.I.gameOverPanel.HideContinueButton();
            }

            if (self.GetCurrentRunGolds() > 0 && LevelPlayManager.I.CheckRewardedVideoAvailability("Game_Over__Double_Gold"))
            {
                GameUI.I.gameOverPanel.ShowDoubleGoldButton();
            }
            else
            {
                GameUI.I.gameOverPanel.HideDoubleGoldButton();
            }

            Game.I.StateViewHandler.SwitchTo<GameOverPanel>();
            return base.OnEnter(self);
        }
    }
}