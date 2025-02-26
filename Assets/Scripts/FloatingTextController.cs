using System.Collections;
using System.Collections.Generic;
using Assets.Scripts.Patterns.FactoryPattern;
using Assets.Scripts.Patterns.SingletonPattern;
using UnityEngine;

namespace Assets.Scripts
{

    public class FloatingTextController : SingletonProvider<FloatingTextController>
    {
        [SerializeField] private GameObject _prefab;
        private FactoryProvider<FloatingText> _floatingTextFactory = new();

        public void InstantiateFloatingText(string text, float lifetime, Color color, Vector3 position, Vector3 speed)
        {
            var floatingText = _floatingTextFactory.Create(_prefab);
            floatingText.transform.position = position;
            floatingText.SetText(text, color);
            floatingText.lifetime = lifetime;
            floatingText.speed = speed;
        }
    }
}
