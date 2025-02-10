using Assets.Scripts.Patterns.StatePattern;
using UnityEngine;

namespace Assets.Scripts.States.GameStates
{
    public class PlayingState : State<Game>
    {
        public override StateTransition<Game> OnEnter(Game self)
        {
            self.isPaused = false;
            PlayingPanel.Instance.Show();
            return base.OnEnter(self);
        }

        public override void OnExit(Game self)
        {
            base.OnExit(self);
            self.isPaused = true;
            PlayingPanel.Instance.Hide();
        }

        public override StateTransition<Game> Update(Game self)
        {
            if (Input.GetMouseButtonDown(0))
            {
                Cube.Instance.ChangeDirection();
            }

            PlatformManager.Instance.UpdatePlatforms();

            return base.Update(self);
        }
    }
}