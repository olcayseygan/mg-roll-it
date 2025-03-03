using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.Localization.Components;
using UnityEngine.Localization;

namespace Assets.Scripts
{
    public class ShopItem : MonoBehaviour
    {
        public string key;
        public SkinDataSO data;

        [SerializeField] private TMP_Text _nameText;
        [SerializeField] private TMP_Text _priceText;
        [SerializeField] private TMP_Text _freeText;
        [SerializeField] private TMP_Text _ownedText;

        public void SetNameText(string name)
        {
            _nameText.text = name;
        }

        public TMP_Text GetNameText()
        {
            return _nameText;
        }

        public void SetPriceText(int price)
        {
            if (price == 0)
            {
                _priceText.gameObject.SetActive(false);
                _freeText.gameObject.SetActive(true);
            }
            else
            {
                var stringEvent = _priceText.GetComponent<LocalizeStringEvent>();
                stringEvent.StringReference = new LocalizedString { TableReference = "Table", TableEntryReference = "UI_SHOP_ITEM_PRICE_TEXT", Arguments = new[] { price.ToString() } };
                _priceText.text = price.ToString();
            }
        }

        public void ShowOwnedText()
        {
            _ownedText.gameObject.SetActive(true);
        }

        public void Inspect()
        {
            ShopManager.I.InspectItem(key);
        }
    }
}
