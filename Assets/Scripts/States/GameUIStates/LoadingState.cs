using Assets.Scripts.Patterns.StatePattern;
using UnityEngine;

namespace Assets.Scripts.States.GameUIStates
{
    public class LoadingState : State<GameUI>
    {
        public override StateTransition<GameUI> OnEnter(GameUI self)
        {
            self.loadingPanel.Show();
            return base.OnEnter(self);
        }

        public override void OnExit(GameUI self)
        {
            base.OnExit(self);
            self.loadingPanel.Hide();
        }
    }
}