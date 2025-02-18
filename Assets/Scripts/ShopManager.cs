using System.Collections;
using System.Collections.Generic;
using Assets.Scripts.Patterns.SingletonPattern;
using UnityEngine;

namespace Assets.Scripts
{
    public class ShopManager : SingletonProvider<ShopManager>
    {
        [SerializeField] private GameObject _shopItemPrefab;
        [SerializeField] private Transform _contentTranform;

        public Color defaultColor;
        public Color purchasedColor;
        public Color notEnoughCoinColor;

        private readonly List<ShopItem> _shopItems = new();

        public void LoadCubeSkins()
        {
            ClearList();
            var ownedKeys = PlayerController.I.GetOwnedCubeSkinKeys();
            foreach (var key in PlayerController.I.GetAllSkinKeys())
            {
                var shopItem = Instantiate(_shopItemPrefab, _contentTranform).GetComponent<ShopItem>();
                shopItem.key = key;
                shopItem.data = SkinManager.I.GetSkinData(key);
                shopItem.SetNameText(shopItem.data.name);
                shopItem.SetPriceText(shopItem.data.price);
                if (ownedKeys.Contains(key))
                {
                    shopItem.MaskAsPurchased();
                }
                else if (PlayerController.I.GetCoins() < shopItem.data.price)
                {
                    shopItem.MaskAsNotEnoughCoin();
                }

                _shopItems.Add(shopItem);
            }
        }

        public void ClearList()
        {
            foreach (Transform child in _contentTranform)
            {
                Destroy(child.gameObject);
            }

            _shopItems.Clear();
        }
    }
}
