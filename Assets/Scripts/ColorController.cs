using System.Collections;
using System.Collections.Generic;
using Assets.Scripts.Patterns.SingletonPattern;
using UnityEngine;

namespace Assets.Scripts
{
    public class ColorController : SingletonProvider<ColorController>
    {
        [SerializeField] private float _speed = 5f;
        private float h = 0f;

        private void Update()
        {
            if (Game.Instance.isPaused)
            {
                return;
            }

            h += _speed * Time.deltaTime;
            if (h >= 360f)
            {
                h = 0f;
            }

            UpdateColor();
        }

        public void SetColor() {
            h = Random.Range(0f, 360f);
        }

        public void UpdateColor()
        {
            Color mainColor = Color.HSVToRGB(h / 360f, 0.12f, 1f);
            foreach (Platform platform in PlatformManager.Instance.GetPlatforms())
            {
                platform.meshRenderer.material.color = mainColor;
            }
        }
    }
}
