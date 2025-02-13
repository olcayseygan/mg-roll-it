using Assets.Scripts.Patterns.StatePattern;
using UnityEngine;

namespace Assets.Scripts.States.GameStates
{
    public class GameOverState : State<Game>
    {
        public override StateTransition<Game> OnEnter(Game self)
        {
            if (GameController.Instance.score > GameController.Instance.GetHighScore())
            {
                GameController.Instance.SetHighScore(GameController.Instance.score);
            }

            if (self.canContinue)
            {
                GameOverPanel.Instance.ShowWatchAdButton();
            }
            else
            {
                GameOverPanel.Instance.HideWatchAdButton();
                GameOverPanel.Instance.HideContinueButton();
            }

            GameOverPanel.Instance.Show(GameController.Instance.score, GameController.Instance.GetHighScore());
            self.postProcessingVolume.profile.GetSetting<UnityEngine.Rendering.PostProcessing.DepthOfField>().active = true;
            return base.OnEnter(self);
        }

        public override void OnExit(Game self)
        {
            GameOverPanel.Instance.Hide();
            self.postProcessingVolume.profile.GetSetting<UnityEngine.Rendering.PostProcessing.DepthOfField>().active = false;
            base.OnExit(self);
        }
    }
}