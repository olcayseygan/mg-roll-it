using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts
{
    public class Gold : MonoBehaviour
    {
        private float _turnSpeed = 100f;

        private void Start()
        {
            _turnSpeed = Random.Range(50f, 150f);
        }

        private void Update()
        {
            transform.Rotate(Vector3.up, _turnSpeed * Time.deltaTime);
        }

        public void OnTriggerEnter(Collider collider)
        {
            Cube.I.OnGoldCollected(1);
            FloatingTextController.I.InstantiateFloatingText("+1", 0.5f, Color.black, transform.position + Vector3.up, Vector3.up * 10f);
            Destroy(gameObject);
        }
    }
}
