using System.Collections;
using System.Collections.Generic;
using Assets.Scripts.Patterns.SingletonPattern;
using Assets.Scripts.Patterns.StatePattern;
using UnityEngine;

namespace Assets.Scripts
{
    public class Cube : SingletonProvider<Cube>, IHasStateProvider<Cube>
    {
        public StateProvider<Cube> StateProvider { get; set; }

        public Transform animationPivotTransform;
        public Transform animationModelTranform;
        public AnimationCurve motionCurve;
        public float motionDuration = 1.0f;

        public Vector3 direction = Vector3.zero;

        protected override void Awake()
        {
            base.Awake();
            StateProvider = new StateProvider<Cube>(this);
            StateProvider.RegisterState(new States.CubeStates.IdleState());
            StateProvider.RegisterState(new States.CubeStates.MotionState());
            StateProvider.SwitchTo<States.CubeStates.MotionState>();
        }

        private void Update()
        {
            StateProvider.Update();
        }
    }
}
