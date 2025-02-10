using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts
{
    public class FollowTransform : MonoBehaviour
    {
        [SerializeField] private Transform _transform;
        [SerializeField] private Vector3 _offset = new(0, 0, 0);

        private void Update()
        {
            transform.position = new Vector3(_transform.position.x, 0f, _transform.position.z) + _offset;
        }
    }
}