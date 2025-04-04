using System.Collections;
using System.Collections.Generic;
using Assets.Scripts.Patterns.SingletonPattern;
using UnityEngine;
using UnityEngine.Localization;
using UnityEngine.Localization.Components;

namespace Assets.Scripts
{
    public class InventoryManager : SingletonProvider<InventoryManager>
    {
        [SerializeField] private GameObject _inventoryItemPrefab;
        [SerializeField] private Transform _contentTranform;

        public Color unequippedColor;
        public Color equippedColor;

        private readonly List<InventoryItem> _inventoryItems = new();

        public void LoadOwnedCubeSkins()
        {
            ClearList();

            var equippedKey = PlayerController.I.GetEquippedCubeSkinKey();
            foreach (var key in PlayerController.I.GetOwnedCubeSkinKeys())
            {
                var inventoryItem = Instantiate(_inventoryItemPrefab, _contentTranform).GetComponent<InventoryItem>();
                inventoryItem.key = key;
                inventoryItem.data = SkinManager.I.GetSkinData(key);
                var stringEvent = inventoryItem.GetNameText().GetComponent<LocalizeStringEvent>();
                stringEvent.StringReference = new LocalizedString { TableReference = "Table", TableEntryReference = $"SKIN_{inventoryItem.data.name}" };
                if (key == equippedKey)
                {
                    inventoryItem.MarkAsEquipped();
                }

                _inventoryItems.Add(inventoryItem);
            }

            _inventoryItems.Sort((a, b) => a.data.price.CompareTo(b.data.price));
            foreach (var item in _inventoryItems)
            {
                item.transform.SetAsLastSibling();
            }
        }

        public void MarkAsEquipped(InventoryItem inventoryItem)
        {
            foreach (var item in _inventoryItems)
            {
                item.MarkAsUnequipped();
            }

            inventoryItem.MarkAsEquipped();
        }

        public void ClearList()
        {
            foreach (Transform child in _contentTranform)
            {
                Destroy(child.gameObject);
            }

            _inventoryItems.Clear();
        }
    }
}
