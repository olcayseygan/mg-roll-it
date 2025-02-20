using System.Collections;
using System.Collections.Generic;
using Assets.Scripts.Patterns.SingletonPattern;
using UnityEngine;
using UnityEngine.Localization;
using UnityEngine.Localization.Components;

namespace Assets.Scripts
{
    public class ShopManager : SingletonProvider<ShopManager>
    {
        [SerializeField] private GameObject _shopItemPrefab;
        [SerializeField] private Transform _contentTranform;

        public Color defaultColor;
        public Color purchasedColor;
        public Color notEnoughGoldColor;

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
                var stringEvent = shopItem.GetNameText().GetComponent<LocalizeStringEvent>();
                stringEvent.StringReference = new LocalizedString { TableReference = "Table", TableEntryReference = $"SKIN_{shopItem.data.name}" };
                shopItem.SetPriceText(shopItem.data.price);
                if (ownedKeys.Contains(key))
                {
                    shopItem.MaskAsPurchased();
                }
                else if (PlayerController.I.GetGolds() < shopItem.data.price)
                {
                    shopItem.MaskAsNotEnoughGold();
                }

                _shopItems.Add(shopItem);
            }
        }

        public void ClearList()
        {
            foreach (var item in _shopItems)
            {
                Destroy(item.gameObject);
            }

            _shopItems.Clear();
        }
    }
}
