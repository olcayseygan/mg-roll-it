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
        [SerializeField] private Button _purchaseButton;
        [SerializeField] private Button _notEnoughGoldButton;
        [SerializeField] private Button _ownedButton;

        [SerializeField] private Image _purchaseButtonImage;
        [SerializeField] private TMP_Text _purchaseButtonText;

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
                _priceText.text = price.ToString();
            }
        }

        public void MaskAsPurchased() {
            _purchaseButton.gameObject.SetActive(false);
            _ownedButton.gameObject.SetActive(true);
            _notEnoughGoldButton.gameObject.SetActive(false);
        }

        public void MaskAsNotEnoughGold() {
            _purchaseButton.gameObject.SetActive(false);
            _ownedButton.gameObject.SetActive(false);
            _notEnoughGoldButton.gameObject.SetActive(true);
        }

        public void Purchase()
        {
            PlayerController.I.OwnCubeSkin(key);
            PlayerController.I.RemoveGold(data.price);
            MaskAsPurchased();
        }
    }
}
