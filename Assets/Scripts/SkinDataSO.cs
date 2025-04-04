using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts
{

    [CreateAssetMenu(fileName = "Scriptable Object", menuName = "ScriptableObjects/Skin Data", order = 1)]
    public class SkinDataSO : ScriptableObject
    {
        public new string name;
        public GameObject prefab;
        public Material material;
        public int price;

        [Header("Price Modifiers")]
        [SerializeField] private bool _isFree;
        [SerializeField] private int _numberOfColors;
        [SerializeField] private bool _isMetallic;
        [SerializeField] private bool _hasAnimation;

        public int GetPrice()
        {
            if (_isFree)
            {
                return 0;
            }

            var price = 259.8f;
            if (_numberOfColors > 0)
            {
                price *= 1.69f * _numberOfColors;
            }

            if (_isMetallic)
            {
                price *= 1.89f;
            }

            if (_hasAnimation)
            {
                price *= 6.42f;
            }

            return (int)price;
        }

        private void OnValidate()
        {
            if (prefab == null)
            {
                Debug.LogError("Prefab is null");
            }

            if (material == null)
            {
                Debug.LogError("Material is null");
            }

            price = GetPrice();
        }
    }
}