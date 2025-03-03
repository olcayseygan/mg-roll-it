using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Skins
{
    public class RandomColorCycleModifier : MonoBehaviour
    {
        public List<Color> colors = new();
        [SerializeField] private float _lerpSpeed = 1.0f;

        private int _currentIndex = 0;
        private int _nextIndex;
        private float _t = 0;
        [SerializeField] private MeshRenderer _meshRenderer;

        private void Start()
        {
            _t = _lerpSpeed;
            if (colors.Count > 1)
            {
                _nextIndex = Random.Range(0, colors.Count);
            }
        }

        private void Update()
        {
            if (colors.Count < 2) return;

            _t = Mathf.Max(0f, _t - Time.deltaTime);
            Color lerpedColor = Color.Lerp(colors[_currentIndex], colors[_nextIndex], 1f - _t / _lerpSpeed);
            _meshRenderer.material.SetColor("_Color", lerpedColor);
            _meshRenderer.material.SetColor("_EmissionColor", lerpedColor * 0.5f);

            if (_t <= 0f)
            {
                _t = _lerpSpeed;
                _currentIndex = _nextIndex;
                _nextIndex = Random.Range(0, colors.Count);
            }
        }
    }
}
