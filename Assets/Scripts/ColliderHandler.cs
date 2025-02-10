using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Assets.Scripts
{
    public class ColliderHandler : MonoBehaviour
    {
        [SerializeField] private UnityEvent<Collider> _onTriggerEnter;
        [SerializeField] private UnityEvent<Collider> _onTriggerExit;


        private void OnTriggerEnter(Collider other)
        {
            _onTriggerEnter.Invoke(other);
        }

        private void OnTriggerExit(Collider other)
        {
            _onTriggerExit.Invoke(other);
        }
    }
}