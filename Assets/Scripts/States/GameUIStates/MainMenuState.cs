using Assets.Scripts.Patterns.StatePattern;
using UnityEngine;

namespace Assets.Scripts.States.GameUIStates
{
    public class MainMenuState : State<GameUI>
    {
        public override StateTransition<GameUI> OnEnter(GameUI self)
        {
            self.mainMenuPanel.SetCoinsText(PlayerController.I.GetCoins());
            self.mainMenuPanel.SetHighScoreText(PlayerController.I.GetHighScore());
            self.mainMenuPanel.SetGamesPlayedText(PlayerController.I.GetPlayedGames());
            self.mainMenuPanel.Show();
            return base.OnEnter(self);
        }

        public override void OnExit(GameUI self)
        {
            base.OnExit(self);
            self.mainMenuPanel.Hide();
        }
    }
}