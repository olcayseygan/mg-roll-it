using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts
{
    [System.Serializable]
    public class KeyPrefabPair
    {
        public string key;
        public SkinDataSO data;
    }

    [CreateAssetMenu(fileName = "Skin Collection", menuName = "ScriptableObjects/Skin Collection", order = 1)]
    public class SkinCollectionSO : ScriptableObject
    {
        public List<KeyPrefabPair> collection = new();
    }
}
