using Assets.Scripts.Patterns.StatePattern;
using UnityEngine;

namespace Assets.Scripts.States.GameUIStates
{
    public class SettingsState : State<GameUI>
    {
        public override StateTransition<GameUI> OnEnter(GameUI self)
        {
            self.settingsPanel.graphicsQualityGroup.SetSelectedIndex(PlayerController.I.GetQualityLevelIndex());
            self.settingsPanel.maxFpsGroup.SetSelectedIndex(PlayerController.I.GetMaxFPSIndex());
            self.settingsPanel.Show();
            return base.OnEnter(self);
        }

        public override void OnExit(GameUI self)
        {
            base.OnExit(self);
            self.settingsPanel.Hide();
        }
    }
}