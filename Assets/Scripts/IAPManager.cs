using System.Collections;
using System.Collections.Generic;
using Assets.Scripts.Patterns.SingletonPattern;
using Assets.Scripts.StateViews;
using UnityEngine;
using UnityEngine.Purchasing;
using UnityEngine.Purchasing.Extension;

namespace Assets.Scripts
{
    public class IAPManager : SingletonProvider<IAPManager>, IDetailedStoreListener
    {
        private IStoreController _storeController;
        private IExtensionProvider _storeExtensionProvider;

        private void Start()
        {
            var builder = ConfigurationBuilder.Instance(StandardPurchasingModule.Instance());
            builder.AddProduct("gold500", ProductType.Consumable);
            builder.AddProduct("gold1400", ProductType.Consumable);
            builder.AddProduct("gold3900", ProductType.Consumable);
            builder.AddProduct("gold12000", ProductType.Consumable);
            builder.AddProduct("gold40000", ProductType.Consumable);
            UnityPurchasing.Initialize(this, builder);
        }

        public void OnInitialized(IStoreController controller, IExtensionProvider extensions)
        {
            _storeController = controller;
            _storeExtensionProvider = extensions;
            var shopPanel = Game.I.StateViewHandler.Get<ShopPanel>();
            foreach (var product in controller.products.all)
            {
                var id = product.definition.id;
                Debug.Log($"Product: {id}");
                var price = product.metadata.localizedPrice.ToString().Replace(",", ".");
                switch(id) {
                    case "gold500":
                        shopPanel.gold500GameObject.SetActive(true);
                        shopPanel.gold500PriceText.text = price;
                        break;
                    case "gold1400":
                        shopPanel.gold1400GameObject.SetActive(true);
                        shopPanel.gold1400PriceText.text = price;
                        break;
                    case "gold3900":
                        shopPanel.gold3900GameObject.SetActive(true);
                        shopPanel.gold3900PriceText.text = price;
                        break;
                    case "gold12000":
                        shopPanel.gold12000GameObject.SetActive(true);
                        shopPanel.gold12000PriceText.text = price;
                        break;
                    case "gold40000":
                        shopPanel.gold40000GameObject.SetActive(true);
                        shopPanel.gold40000PriceText.text = price;
                        break;
                }
            }
        }

        public void OnInitializeFailed(InitializationFailureReason error)
        {
            NotificationController.I.ShowNotification(
                LocalizationController.I.GetLocalizedString("NOTIFICATION_GOLD_PACKAGE_PUCHASE_FAILED"),
                5f, NotificationType.Error);
        }

        public void OnInitializeFailed(InitializationFailureReason error, string message)
        {
            NotificationController.I.ShowNotification(
                LocalizationController.I.GetLocalizedString("NOTIFICATION_GOLD_PACKAGE_PUCHASE_FAILED"),
                5f, NotificationType.Error);
        }

        public void OnPurchaseFailed(Product product, PurchaseFailureDescription failureDescription)
        {
            NotificationController.I.ShowNotification(
                LocalizationController.I.GetLocalizedString("NOTIFICATION_GOLD_PACKAGE_PUCHASE_FAILED"),
                5f, NotificationType.Error);
        }

        public void OnPurchaseFailed(Product product, PurchaseFailureReason failureReason)
        {
            NotificationController.I.ShowNotification(
                LocalizationController.I.GetLocalizedString("NOTIFICATION_GOLD_PACKAGE_PUCHASE_FAILED"),
                5f, NotificationType.Error);
        }

        public PurchaseProcessingResult ProcessPurchase(PurchaseEventArgs purchaseEvent)
        {
            var id = purchaseEvent.purchasedProduct.definition.id;
            var amount = 0;
            switch(id) {
                case "gold500":
                    amount = 500;
                    break;
                case "gold1400":
                    amount = 1400;
                    break;
                case "gold3900":
                    amount = 3900;
                    break;
                case "gold12000":
                    amount = 12000;
                    break;
                case "gold40000":
                    amount = 40000;
                    break;
            }

            PlayerController.I.AddGold(amount);
            NotificationController.I.ShowNotification(
                LocalizationController.I.GetLocalizedString("NOTIFICATION_GOLD_PACKAGE_PUCHASE_SUCCESSFUL", amount.ToString()),
                5f, NotificationType.Success);
            return PurchaseProcessingResult.Complete;
        }

    }
}
