using Assets.Scripts.Patterns.StatePattern;
using UnityEngine;

namespace Assets.Scripts.States.GameStates
{
    public class PlayingState : State<Game>
    {
        private bool _isMovingForward = true;

        public override StateTransition<Game> OnEnter(Game self)
        {
            PlayingPanel.Instance.Show();
            return base.OnEnter(self);
        }

        public override void OnExit(Game self)
        {
            base.OnExit(self);
            PlayingPanel.Instance.Hide();
        }

        public override StateTransition<Game> Update(Game self)
        {
            if (Input.GetMouseButtonDown(0))
            {
                Cube.Instance.direction = _isMovingForward ? new Vector3(0, 0, 1) : new Vector3(1, 0, 0);
                _isMovingForward = !_isMovingForward;
            }

            PlatformManager.Instance.UpdatePlatforms();
            self.cameraTransform.position = new Vector3(Cube.Instance.modelTransform.position.x, 0f, Cube.Instance.modelTransform.position.z) + self.cameraOffset;
            return base.Update(self);
        }
    }
}