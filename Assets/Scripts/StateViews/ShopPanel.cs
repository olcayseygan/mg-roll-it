using Assets.Scripts.Patterns.StatePattern.Plugins;
using UnityEngine;

namespace Assets.Scripts.StateViews
{
    public class ShopPanel : StateViewPanel
    {
        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                BackButton_Click();
            }
        }

        public override void Show()
        {
            ShopManager.I.LoadCubeSkins();
            base.Show();
        }

        public void BackButton_Click()
        {
            Game.I.StateViewHandler.SwitchTo<MainMenuPanel>();
        }
    }
}
