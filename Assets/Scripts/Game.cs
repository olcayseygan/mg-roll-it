using System.Collections;
using System.Collections.Generic;
using Assets.Scripts.Patterns.SingletonPattern;
using Assets.Scripts.Patterns.StatePattern;
using Assets.Scripts.States.GameStates;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

namespace Assets.Scripts
{
    public class Game : SingletonProvider<Game>, IHasStateProvider<Game>
    {
        public StateProvider<Game> StateProvider { get; set; }

        public GameObject playerPrefab;

        public Transform cameraTransform;
        public Vector3 cameraOffset;
        public Transform spotlightTransform;
        public Vector3 spotlightOffset;
        public PostProcessVolume postProcessingVolume;

        public bool isPaused = false;

        protected override void Awake()
        {
            base.Awake();
            StateProvider = new StateProvider<Game>(this);
            StateProvider.RegisterState(new InitializationState());
            StateProvider.RegisterState(new PlayingState());
            StateProvider.RegisterState(new GameOverState());
            StateProvider.RegisterState(new RestartState());
            StateProvider.RegisterState(new ContinueState());
            StateProvider.RegisterState(new MainMenuState());
            StateProvider.SwitchTo<InitializationState>(new InitializationStateProperties() { isQuickPlayActive = false });
        }

        private void Update()
        {
            if (Cube.Instance == null) {
                return;
            }

            cameraTransform.position = new Vector3(Cube.Instance.modelTransform.position.x, 0f, Cube.Instance.modelTransform.position.z) + cameraOffset;
            spotlightTransform.position = new Vector3(Cube.Instance.modelTransform.position.x, 0f, Cube.Instance.modelTransform.position.z) + spotlightOffset;
            StateProvider.Update();
        }
    }
}
