using Assets.Scripts.Patterns.StatePattern.Plugins;
using UnityEngine;
using TMPro;

namespace Assets.Scripts.StateViews
{
    public class ShopPanel : StateViewPanel
    {
        public ShopItemInspector shopItemInspector;

        public GameObject gold500GameObject;
        public TMP_Text gold500PriceText;
        public GameObject gold1400GameObject;
        public TMP_Text gold1400PriceText;
        public GameObject gold3900GameObject;
        public TMP_Text gold3900PriceText;
        public GameObject gold12000GameObject;
        public TMP_Text gold12000PriceText;
        public GameObject gold40000GameObject;
        public TMP_Text gold40000PriceText;

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
