using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Skins.CUBE_RAINBOW
{
    public class RainbowMaterial : MonoBehaviour
    {
        private float h = 0f;
        [SerializeField] private float _speed = 1.0f;
        [SerializeField] private MeshRenderer _meshRenderer;

        private void Update()
        {
            h += _speed * Time.deltaTime;
            if (h >= 360f)
            {
                h = 0f;
            }

            _meshRenderer.material.color = Color.HSVToRGB(h / 360f, 1f, 1f);
        }
    }
}
