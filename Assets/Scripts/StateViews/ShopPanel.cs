using Assets.Scripts.Patterns.StatePattern.Plugins;
using UnityEngine;

namespace Assets.Scripts.StateViews
{
    public class ShopPanel : StateViewPanel
    {
        public ShopItemInspector shopItemInspector;

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
            if (shopItemInspector.gameObject.activeSelf)
            {
                shopItemInspector.Hide();
                return;
            }

            Game.I.StateViewHandler.SwitchTo<MainMenuPanel>();
        }

        public void GoldPackagePurchaseButton_Click(int amount)
        {
            PlayerController.I.AddGold(amount);
            NotificationController.I.ShowNotification(
                LocalizationController.I.GetLocalizedString("NOTIFICATION_GOLD_PACKAGE_PUCHASE_SUCCESSFUL", amount.ToString()),
                5f, NotificationType.Success);
        }
    }
}
