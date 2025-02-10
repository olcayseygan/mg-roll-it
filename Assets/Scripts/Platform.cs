using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts
{
    public class Platform : MonoBehaviour
    {
        public Transform modelTransform;
        public MeshRenderer meshRenderer;

        public bool IsCubeOnMe()
        {
            return Physics.OverlapBox(
                transform.position + Vector3.up * 1f,
                new Vector3(PlatformManager.PLATFORM_SIZE * 0.75f, 2f, PlatformManager.PLATFORM_SIZE * 0.75f) / 2f,
                Quaternion.identity, Cube.Instance.layerMask
            ).Length > 0;
        }
    }
}