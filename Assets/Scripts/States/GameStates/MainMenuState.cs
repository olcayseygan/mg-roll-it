using Assets.Scripts.Patterns.StatePattern;
using UnityEngine;

namespace Assets.Scripts.States.GameStates
{
    public class MainMenuState : State<Game>
    {
        public override StateTransition<Game> OnEnter(Game self)
        {
            self.isPaused = true;
            ColorController.Instance.SetColor();
            ColorController.Instance.UpdateColor();
            MainMenuPanel.Instance.highScoreText.text = GameController.Instance.GetHighScore().ToString();
            self.postProcessingVolume.profile.GetSetting<UnityEngine.Rendering.PostProcessing.DepthOfField>().active = true;
            MainMenuPanel.Instance.Show();
            return base.OnEnter(self);
        }

        public override void OnExit(Game self)
        {
            base.OnExit(self);
            self.postProcessingVolume.profile.GetSetting<UnityEngine.Rendering.PostProcessing.DepthOfField>().active = false;
            MainMenuPanel.Instance.Hide();
            self.isPaused = false;
        }
    }
}