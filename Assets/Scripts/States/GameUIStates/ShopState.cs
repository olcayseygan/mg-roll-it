using System.Collections;
using Assets.Scripts.Patterns.StatePattern;
using UnityEngine;

namespace Assets.Scripts.States.GameUIStates
{
    public class ShopState : State<GameUI>
    {
        public override StateTransition<GameUI> OnEnter(GameUI self)
        {
            ShopManager.I.LoadCubeSkins();
            self.shopPanel.Show();
            return base.OnEnter(self);
        }

        public override void OnExit(GameUI self)
        {
            base.OnExit(self);
            self.shopPanel.Hide();
        }
    }
}