using Assets.Scripts.Patterns.StatePattern;
using UnityEngine;

namespace Assets.Scripts.States.GameStates
{
    public class GameOverState : State<Game>
    {
        public override StateTransition<Game> OnEnter(Game self)
        {
            GameOverPanel.Instance.Show(GameController.Instance.score, GameController.Instance.GetHighScore());
            return base.OnEnter(self);
        }

        public override void OnExit(Game self)
        {
            GameOverPanel.Instance.Hide();
            base.OnExit(self);
        }
    }
}