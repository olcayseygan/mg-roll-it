using System.Collections;
using System.Collections.Generic;
using Assets.Scripts.Patterns.SingletonPattern;
using UnityEngine;

namespace Assets.Scripts {
    public class CoinController : SingletonProvider<CoinController>
    {
        [SerializeField] private GameObject _coinPrefab;

        public Coin SpawnCoin()
        {
            var coin = Instantiate(_coinPrefab).GetComponent<Coin>();
            return coin;
        }
    }
}
