using System.Collections;
using System.Collections.Generic;
using Assets.Scripts.Patterns.SingletonPattern;
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
            UnityPurchasing.Initialize(this, builder);
        }

        public void OnInitialized(IStoreController controller, IExtensionProvider extensions)
        {
            _storeController = controller;
            _storeExtensionProvider = extensions;
            foreach (var product in controller.products.all)
            {
                Debug.Log($"Product ID: {product.definition.id}, Price: {product.metadata.localizedPriceString}");
            }
        }

        public void OnInitializeFailed(InitializationFailureReason error)
        {
        }

        public void OnInitializeFailed(InitializationFailureReason error, string message)
        {
        }

        public void OnPurchaseFailed(Product product, PurchaseFailureDescription failureDescription)
        {
        }

        public void OnPurchaseFailed(Product product, PurchaseFailureReason failureReason)
        {
        }

        public PurchaseProcessingResult ProcessPurchase(PurchaseEventArgs purchaseEvent)
        {
            Debug.Log($"Purchase successful: {purchaseEvent.purchasedProduct.definition.id}");
            return PurchaseProcessingResult.Complete;
        }

    }
}
