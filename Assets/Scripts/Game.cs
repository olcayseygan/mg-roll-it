using System.Collections;
using System.Collections.Generic;
using Assets.Scripts.Patterns.SingletonPattern;
using Assets.Scripts.Patterns.StatePattern;
using UnityEngine;

namespace Assets.Scripts
{
    public class Game : SingletonProvider<Game>, IHasStateProvider<Game>
    {
        public StateProvider<Game> StateProvider { get; set; }

        public GameObject playerPrefab;

        public Transform cameraTransform;
        public Vector3 cameraOffset;



        protected override void Awake()
        {
            base.Awake();
            StateProvider = new StateProvider<Game>(this);
            StateProvider.RegisterState(new States.GameStates.InitializationState());
            StateProvider.RegisterState(new States.GameStates.PlayingState());
            StateProvider.RegisterState(new States.GameStates.GameOverState());
            StateProvider.RegisterState(new States.GameStates.RestartState());
            StateProvider.SwitchTo<States.GameStates.InitializationState>();
        }

        private void Update()
        {
            StateProvider.Update();
        }
    }
}
