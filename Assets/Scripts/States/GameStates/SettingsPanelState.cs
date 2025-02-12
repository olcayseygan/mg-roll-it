using Assets.Scripts.Patterns.StatePattern;
using UnityEngine;

namespace Assets.Scripts.States.GameStates
{
    public class SettingsPanelState : State<Game>
    {
        public override StateTransition<Game> OnEnter(Game self)
        {
            SettingsPanel.Instance.Show();
            return base.OnEnter(self);
        }

        public override void OnExit(Game self)
        {
            base.OnExit(self);
            SettingsPanel.Instance.Hide();
        }
    }
}