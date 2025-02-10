using Assets.Scripts.Patterns.StatePattern;
using UnityEngine;

namespace Assets.Scripts.States.GameStates
{
    public class MainMenuState : State<Game>
    {
        public override StateTransition<Game> OnEnter(Game self)
        {
            MainMenuPanel.Instance.highScoreText.text = GameController.Instance.GetHighScore().ToString();
            MainMenuPanel.Instance.Show();
            return base.OnEnter(self);
        }

        public override void OnExit(Game self)
        {
            base.OnExit(self);
            MainMenuPanel.Instance.Hide();
        }
    }
}