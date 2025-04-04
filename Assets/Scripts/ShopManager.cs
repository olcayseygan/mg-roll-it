using System.Collections;
using System.Collections.Generic;
using Assets.Scripts.Patterns.SingletonPattern;
using Assets.Scripts.StateViews;
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
                shopItem.SetPriceText(shopItem.data.GetPrice());
                if (ownedKeys.Contains(key))
                {
                    shopItem.ShowOwnedText();
                }

                _shopItems.Add(shopItem);
            }

            _shopItems.Sort((a, b) => a.data.price.CompareTo(b.data.price));
            foreach (var item in _shopItems)
            {
                item.transform.SetAsLastSibling();
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

        public void UpdateList() {
            foreach (var item in _shopItems)
            {
                var ownedKeys = PlayerController.I.GetOwnedCubeSkinKeys();
                // if (ownedKeys.Contains(item.key))
                // {
                //     item.MaskAsPurchased();
                // }
                // else if (PlayerController.I.GetGolds() < item.data.price)
                // {
                //     item.MaskAsNotEnoughGold();
                // }
                // else
                // {
                //     item.MaskAsDefault();
                // }
            }
        }

        public void InspectItem(string key)
        {
            var shopItem = _shopItems.Find(item => item.key == key);
            var shopPanel = Game.I.StateViewHandler.Get<ShopPanel>();
            shopPanel.shopItemInspector.LoadShopItem(shopItem);
            shopPanel.shopItemInspector.Show();
        }
    }
}
