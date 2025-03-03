using System.Collections;
using System.Collections.Generic;
using Assets.Scripts.Patterns.StatePattern;
using UnityEngine;

namespace Assets.Scripts
{
    public class Platform : MonoBehaviour
    {
        public StateProvider<Platform> StateProvider { get; private set; }

        public Transform modelTransform;
        public MeshRenderer meshRenderer;

        private void Awake()
        {
            StateProvider = new StateProvider<Platform>(this);
            StateProvider.RegisterState(new States.PlatformStates.IdleState());
            StateProvider.RegisterState(new States.PlatformStates.DestroyState());
            StateProvider.SwitchTo<States.PlatformStates.IdleState>();
        }

        private void Update()
        {
            StateProvider.Update();
        }

        public bool IsCubeOnMe()
        {
            return Physics.OverlapBox(
                transform.position + Vector3.up * 1f,
                new Vector3(modelTransform.localScale.x * 0.75f, 2f, modelTransform.localScale.z * 0.75f) / 2f,
                Quaternion.identity, Cube.I.layerMask
            ).Length > 0;
        }


        public void TrySpawnGoldByChance()
        {
            if (Random.Range(0, 100) < 10)
            {
                var gold = GoldController.I.SpawnGold();
                gold.transform.SetParent(transform);
                gold.transform.localPosition = new Vector3(Random.Range(transform.localScale.x / 2f, -transform.localScale.x / 2f), 0f, Random.Range(transform.localScale.z / 2f, -transform.localScale.z / 2f));
            }
        }
    }
}