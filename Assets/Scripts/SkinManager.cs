using System.Collections;
using System.Collections.Generic;
using Assets.Scripts.Patterns.SingletonPattern;
using UnityEngine;

namespace Assets.Scripts
{
    public class SkinManager : SingletonProvider<SkinManager>
    {
        public SkinCollectionSO skinCollection;

        private readonly Dictionary<string, SkinDataSO> _skinDictionary = new();

        protected override void Awake()
        {
            base.Awake();
            foreach (var pair in skinCollection.collection)
            {
                _skinDictionary.Add(pair.key, pair.data);
            }
        }

        public SkinDataSO GetSkinData(string key)
        {
            return _skinDictionary[key];
        }
    }
}
