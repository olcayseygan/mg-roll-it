using System.Collections;
using System.Collections.Generic;
using Assets.Scripts.Patterns.SingletonPattern;
using UnityEngine;

namespace Assets.Scripts {
    public class GoldController : SingletonProvider<GoldController>
    {
        [SerializeField] private GameObject _goldPrefab;
        [SerializeField] private GameObject _goldCollectionEffectPrefab;

        public Gold SpawnGold()
        {
            var gold = Instantiate(_goldPrefab).GetComponent<Gold>();
            return gold;
        }

        public void InstantiateGoldCollectionEffect(Gold gold)
        {
            Instantiate(_goldCollectionEffectPrefab, gold.transform.position, gold.transform.rotation);
        }
    }
}
