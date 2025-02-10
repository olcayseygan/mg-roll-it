using System.Collections;
using System.Collections.Generic;
using Assets.Scripts.Patterns.SingletonPattern;
using Assets.Scripts.Patterns.StatePattern;
using UnityEngine;
using TMPro;

namespace Assets.Scripts
{
    public class Cube : SingletonProvider<Cube>, IHasStateProvider<Cube>
    {
        public StateProvider<Cube> StateProvider { get; set; }

        public LayerMask layerMask;

        public AnimationCurve motionCurve;

        public Vector3 direction = Vector3.zero;
        public Vector3 currentPosition = Vector3.zero;

        public Transform modelTransform;
        public Rigidbody modelRigidbody;

        public Vector3 deathPosition;

        public Platform lastPlatform;

        protected override void Awake()
        {
            base.Awake();
            StateProvider = new StateProvider<Cube>(this);
            StateProvider.RegisterState(new States.CubeStates.IdleState());
            StateProvider.RegisterState(new States.CubeStates.MotionState());
            StateProvider.RegisterState(new States.CubeStates.DeathState());
            StateProvider.RegisterState(new States.CubeStates.RevivalState());
            StateProvider.SwitchTo<States.CubeStates.IdleState>();
        }

        private void Update()
        {
            StateProvider.Update();
        }

        public void HandleCollisionWithWall()
        {
            Debug.Log("Collision with wall");
        }

        public void ChangeDirection()
        {
            if (direction.z == 1f) {
                direction.x = 1f;
                direction.z = 0f;
            } else {
                direction.x = 0f;
                direction.z = 1f;
            }
        }
    }
}
