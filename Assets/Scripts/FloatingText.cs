using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts
{
    public class FloatingText : MonoBehaviour
    {
        public float lifetime = 1f;
        public Vector3 speed;

        [SerializeField] private TMPro.TMP_Text _text;

        private void Start()
        {
            Destroy(gameObject, lifetime);
            var direction = Camera.main.transform.forward;
            transform.LookAt(transform.position + direction);
        }

        private void Update()
        {
            transform.position += speed * Time.deltaTime;
        }

        public void SetText(string text, Color color)
        {
            _text.text = text;
            _text.color = color;
        }
    }
}
