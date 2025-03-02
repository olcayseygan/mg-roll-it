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
        [SerializeField] private int _numberOfColors;
        [SerializeField] private bool _hasAnimation;

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

            price = 56;
            if (_numberOfColors > 0) {
                price = (int)(price * Mathf.Pow(1.79f, _numberOfColors));
            }

            if (_hasAnimation)
            {
                price = (int)(price * 2.465f);
            }
        }
    }
}