using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Skins
{
    public class ColorCycleModifier : MonoBehaviour
    {
        public List<Color> colors = new();
        [SerializeField] private float _lerpSpeed = 1.0f;

        private int _currentIndex = 0;
        private int _nextIndex = 1;
        private float _t = 0;
        [SerializeField] private MeshRenderer _meshRenderer;

        void Update()
        {
            if (colors.Count < 2) return;

            _t += Time.deltaTime * _lerpSpeed;
            // _meshRenderer.material.color = Color.Lerp(colors[_currentIndex], colors[_nextIndex], _t);
            _meshRenderer.material.SetColor("_Color", Color.Lerp(colors[_currentIndex], colors[_nextIndex], _t));
            _meshRenderer.material.SetColor("_EmissionColor", Color.Lerp(colors[_currentIndex], colors[_nextIndex], _t) * 0.5f);

            if (_t >= 1.0f)
            {
                _t = 0;
                _currentIndex = _nextIndex;
                _nextIndex = (_nextIndex + 1) % colors.Count;
            }
        }
    }
}