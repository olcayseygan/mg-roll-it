using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Assets.Scripts
{
    public class ColliderHandler : MonoBehaviour
    {
        [SerializeField] private UnityEvent<Collision> _onCollisionEnter;
        [SerializeField] private UnityEvent<Collision> _onCollisionExit;
        [SerializeField] private UnityEvent<Collision> _onCollisionStay;

        [SerializeField] private UnityEvent<Collider> _onTriggerEnter;
        [SerializeField] private UnityEvent<Collider> _onTriggerExit;
        [SerializeField] private UnityEvent<Collider> _onTriggerStay;

        private void OnCollisionEnter(Collision collision) => _onCollisionEnter.Invoke(collision);
        private void OnCollisionExit(Collision collision) => _onCollisionExit.Invoke(collision);
        private void OnCollisionStay(Collision collision) => _onCollisionStay.Invoke(collision);

        private void OnTriggerEnter(Collider collider) => _onTriggerEnter.Invoke(collider);
        private void OnTriggerExit(Collider collider) => _onTriggerExit.Invoke(collider);
        private void OnTriggerStay(Collider collider) => _onTriggerStay.Invoke(collider);
    }
}
