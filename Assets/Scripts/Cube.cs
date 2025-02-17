using System.Collections;
using System.Collections.Generic;
using Assets.Scripts.Patterns.SingletonPattern;
using Assets.Scripts.Patterns.StatePattern;
using UnityEngine;
using TMPro;

namespace Assets.Scripts
{
    public class Cube : SingletonProvider<Cube>
    {
        public StateProvider<Cube> StateProvider { get; set; }

        public LayerMask layerMask;

        public AnimationCurve motionCurve;

        public Vector3 direction = Vector3.zero;
        public Vector3 lastKnownPosition = Vector3.zero;

        public Transform modelTransform;

        public Platform lastVisitedPlatform;

        public float speed = 0.15f;
        public int highScore = 0;

        public bool HasFallenOff() => StateProvider.IsInState<States.CubeStates.FellOffState>();
        public Vector3 GetSmoothPosition() => new(modelTransform.position.x, 0f, modelTransform.position.z);

        protected override void Awake()
        {
            base.Awake();
            StateProvider = new StateProvider<Cube>(this);
            StateProvider.RegisterState(new States.CubeStates.IdleState());
            StateProvider.RegisterState(new States.CubeStates.MotionState());
            StateProvider.RegisterState(new States.CubeStates.FellOffState());
            StateProvider.RegisterState(new States.CubeStates.RevivalState());
            StateProvider.RegisterState(new States.CubeStates.ShowcaseState());
            StateProvider.RegisterState(new States.CubeStates.WaitForActionState());
            StateProvider.SwitchTo<States.CubeStates.ShowcaseState>();
        }

        private void Update()
        {
            StateProvider.Update();
        }

        public void ChangeDirection()
        {
            if (direction.z == 1f)
            {
                direction.x = 1f;
                direction.z = 0f;
            }
            else
            {
                direction.x = 0f;
                direction.z = 1f;
            }
        }

    }
}
