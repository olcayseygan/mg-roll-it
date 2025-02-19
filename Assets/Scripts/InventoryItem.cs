using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

namespace Assets.Scripts
{
    public class InventoryItem : MonoBehaviour
    {
        public string key;
        public SkinDataSO data;

        [SerializeField] private TMP_Text _nameText;
        [SerializeField] private GameObject _equippedTextGameObject;
        [SerializeField] private Image _image;

        public void SetNameText(string name)
        {
            _nameText.text = name;
        }

        public TMP_Text GetNameText()
        {
            return _nameText;
        }

        public void MarkAsEquipped()
        {
            _image.color = InventoryManager.I.equippedColor;
            _equippedTextGameObject.SetActive(true);
        }

        public void MarkAsUnequipped()
        {
            _image.color = InventoryManager.I.unequippedColor;
            _equippedTextGameObject.SetActive(false);
        }

        public void Equip()
        {
            PlayerController.I.SetEquippedCubeSkinKey(key);
            InventoryManager.I.MarkAsEquipped(this);
        }
    }
}
