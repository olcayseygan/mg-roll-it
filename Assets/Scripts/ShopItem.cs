using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

namespace Assets.Scripts
{
    public class ShopItem : MonoBehaviour
    {
        public string key;
        public SkinDataSO data;

        [SerializeField] private TMP_Text _nameText;
        [SerializeField] private TMP_Text _priceText;
        [SerializeField] private Button _purchaseButton;
        [SerializeField] private Image _purchaseButtonImage;
        [SerializeField] private TMP_Text _purchaseButtonText;

        public void SetNameText(string name)
        {
            _nameText.text = name;
        }

        public void SetPriceText(int price)
        {
            _priceText.text = price.ToString();
        }

        public void MaskAsPurchased() {
            _purchaseButton.interactable = false;
            _purchaseButtonImage.color = ShopManager.I.purchasedColor;
            _purchaseButtonText.text = "OWNED";
        }

        public void MaskAsNotEnoughCoin() {
            _purchaseButton.interactable = false;
            _purchaseButtonImage.color = ShopManager.I.notEnoughCoinColor;
            _purchaseButtonText.text = "NOT ENOUGH COINS";
        }

        public void Purchase()
        {
            PlayerController.I.OwnCubeSkin(key);
            PlayerController.I.RemoveCoins(data.price);
            MaskAsPurchased();
        }
    }
}
