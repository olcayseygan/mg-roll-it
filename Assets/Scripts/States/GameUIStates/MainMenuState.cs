using Assets.Scripts.Patterns.StatePattern;
using UnityEngine;

namespace Assets.Scripts.States.GameUIStates
{
    public class MainMenuState : State<GameUI>
    {
        public override StateTransition<GameUI> OnEnter(GameUI self)
        {
            self.mainMenuPanel.RefreshAudioToggleColors();
            self.mainMenuPanel.Show();
            self.mainMenuPanel.SetHighScoreText(Game.I.GetHighScore());
            return base.OnEnter(self);
        }

        public override void OnExit(GameUI self)
        {
            base.OnExit(self);
            self.mainMenuPanel.Hide();
        }
    }
}