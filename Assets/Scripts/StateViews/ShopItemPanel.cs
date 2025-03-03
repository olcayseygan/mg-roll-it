using Assets.Scripts.Patterns.StatePattern.Plugins;
using UnityEngine;
using TMPro;
using UnityEngine.Localization.Components;
using UnityEngine.Localization;
using UnityEngine.UI;

namespace Assets.Scripts.StateViews
{
    public class ShopItemPanel : StateViewPanel
    {
        [SerializeField] private TMP_Text _titleText;
        [SerializeField] private TMP_Text _nameText;
        [SerializeField] private TMP_Text _priceText;
        [SerializeField] private TMP_Text _freeText;


        [SerializeField] private Button _purchaseButton;
        [SerializeField] private Button _notEnoughGoldButton;
        [SerializeField] private Button _ownedButton;




        [SerializeField] private ShopItem _shopItem;
        [SerializeField] private GameObject _showcaseGameObject;
        [SerializeField] private Transform _standTransform;

        private GameObject _cubeGameObject;

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                BackButton_Click();
            }
        }

        public override void Show()
        {
            _showcaseGameObject.SetActive(true);
            base.Show();
        }

        public override void Hide()
        {
            _showcaseGameObject.SetActive(false);
            Destroy(_cubeGameObject);
            base.Hide();
        }

        public void BackButton_Click()
        {
            Game.I.StateViewHandler.SwitchTo<ShopPanel>();
        }


        public void MaskAsDefault()
        {
            _purchaseButton.gameObject.SetActive(true);
            _ownedButton.gameObject.SetActive(false);
            _notEnoughGoldButton.gameObject.SetActive(false);
        }

        public void MaskAsPurchased()
        {
            _purchaseButton.gameObject.SetActive(false);
            _ownedButton.gameObject.SetActive(true);
            _notEnoughGoldButton.gameObject.SetActive(false);
        }

        public void MaskAsNotEnoughGold()
        {
            _purchaseButton.gameObject.SetActive(false);
            _ownedButton.gameObject.SetActive(false);
            _notEnoughGoldButton.gameObject.SetActive(true);
        }

        public void Purchase()
        {
            PlayerController.I.OwnCubeSkin(_shopItem.key);
            PlayerController.I.RemoveGold(_shopItem.data.GetPrice());
            MaskAsPurchased();
        }

        public void LoadShopItem(ShopItem shopItem)
        {
            _shopItem = shopItem;
            var stringEvent = _titleText.GetComponent<LocalizeStringEvent>();
            stringEvent.StringReference = new LocalizedString { TableReference = "Table", TableEntryReference = $"SKIN_{shopItem.data.name}" };
            stringEvent = _nameText.GetComponent<LocalizeStringEvent>();
            stringEvent.StringReference = new LocalizedString { TableReference = "Table", TableEntryReference = $"SKIN_{shopItem.data.name}" };
            var price = shopItem.data.GetPrice();
            if (price == 0)
            {
                _priceText.gameObject.SetActive(false);
                _freeText.gameObject.SetActive(true);
            }
            else
            {
                _freeText.gameObject.SetActive(false);
                _priceText.gameObject.SetActive(true);
                stringEvent = _priceText.GetComponent<LocalizeStringEvent>();
                stringEvent.StringReference = new LocalizedString { TableReference = "Table", TableEntryReference = "UI_SHOP_ITEM_PRICE_TEXT", Arguments = new[] { price.ToString() } };
                _priceText.text = price.ToString();
            }
            var ownedKeys = PlayerController.I.GetOwnedCubeSkinKeys();
            if (ownedKeys.Contains(shopItem.key))
            {
                MaskAsPurchased();
            }
            else if (PlayerController.I.GetGolds() < price)
            {
                MaskAsNotEnoughGold();
            }
            else
            {
                MaskAsDefault();
            }

            _cubeGameObject = Instantiate(shopItem.data.prefab, _standTransform);
            _cubeGameObject.transform.SetPositionAndRotation(_standTransform.position, Quaternion.identity);
            _cubeGameObject.transform.localRotation = Quaternion.identity;
            Debug.Log($"Loading shop item: {_cubeGameObject}");
            Destroy(_cubeGameObject.GetComponent<Cube>());
        }
    }
}
