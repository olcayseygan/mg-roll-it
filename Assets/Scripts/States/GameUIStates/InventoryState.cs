using System.Collections;
using Assets.Scripts.Patterns.StatePattern;
using UnityEngine;

namespace Assets.Scripts.States.GameUIStates
{
    public class InventoryState : State<GameUI>
    {
        public override StateTransition<GameUI> OnEnter(GameUI self)
        {
            InventoryManager.I.LoadOwnedCubeSkins();
            self.inventoryPanel.Show();
            return base.OnEnter(self);
        }

        public override void OnExit(GameUI self)
        {
            base.OnExit(self);
            InventoryManager.I.ClearList();
            self.StartCoroutine(ChangeCubeSkin(self));
        }

        private IEnumerator ChangeCubeSkin(GameUI self)
        {
            Game.I.DestoryCube();
            yield return null;
            Game.I.CreateCube();
            self.inventoryPanel.Hide();
        }
    }
}