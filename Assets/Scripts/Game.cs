using System.Collections;
using System.Collections.Generic;
using Assets.Scripts.Patterns.SingletonPattern;
using Assets.Scripts.Patterns.StatePattern;
using UnityEngine;

namespace Assets.Scripts
{
    public class Game : SingletonProvider<Game>
    {
        private const float LERP_SPEED = 5f;

        public StateProvider<Game> StateProvider { get; set; }

        public GameObject cubePrefab;

        public new Camera camera;
        public Transform cameraTransform;
        public Vector3 cameraOffset;
        public Vector3 cameraTargetOffset;
        public Quaternion cameraTargetRotation;
        public float cameraTargetOrthographicSize;
        public Transform spotlightTransform;
        public Vector3 spotlightOffset;

        public bool isPaused = true;

        public bool canContinue = false;

        private int _score = 0;

        public GameObject puppy;
        public Cube cube;

        protected override void Awake()
        {
            base.Awake();
            StateProvider = new StateProvider<Game>(this);
            StateProvider.RegisterState(new States.GameStates.InitializationState());
            StateProvider.RegisterState(new States.GameStates.PlayingState());
            StateProvider.RegisterState(new States.GameStates.GameOverState());
            StateProvider.RegisterState(new States.GameStates.RestartState());
            StateProvider.RegisterState(new States.GameStates.ContinueState());
            StateProvider.RegisterState(new States.GameStates.AwakeState());
            StateProvider.RegisterState(new States.GameStates.ShowcaseState());
            StateProvider.SwitchTo<States.GameStates.AwakeState>();
        }

        private void Update()
        {
            if (cube != null)
            {
                var smoothPosition = cube.GetSmoothPosition();
                cameraOffset = Vector3.Lerp(cameraOffset, cameraTargetOffset, Time.deltaTime * LERP_SPEED);
                camera.orthographicSize = Mathf.Lerp(camera.orthographicSize, cameraTargetOrthographicSize, Time.deltaTime * LERP_SPEED);
                camera.transform.SetPositionAndRotation(
                    puppy.transform.position + cameraOffset,
                    Quaternion.Lerp(camera.transform.rotation, cameraTargetRotation, Time.deltaTime * LERP_SPEED)
                );
                spotlightTransform.position = Vector3.Lerp(spotlightTransform.position, smoothPosition + spotlightOffset, Time.deltaTime * LERP_SPEED);
                puppy.transform.position = Vector3.Lerp(puppy.transform.position, smoothPosition, Time.deltaTime * LERP_SPEED);
            }

            StateProvider.Update();
        }

        public List<Platform> GetPlatforms() => PlatformManager.I.GetPlatforms();
        public LayerMask GetCubeLayerMask() => cube.layerMask;
        public int GetScore() => _score;
        public int GetHighScore() => PlayerPrefs.GetInt("HighScore", 0);

        public void SetScore(int score)
        {
            _score = score;
            GameUI.I.playingPanel.SetScoreText(_score);
        }
        public void SetHighScore(int highScore) => PlayerPrefs.SetInt("HighScore", highScore);

        public void AddScore(int score)
        {
            _score += score;
            GameUI.I.playingPanel.SetScoreText(_score);
        }

        public void ContinueGame()
        {
            StateProvider.SwitchTo<States.GameStates.ContinueState>();
        }

        public void RestartGame(bool isQuickPlayActive)
        {
            StateProvider.SwitchTo<States.GameStates.RestartState>(new States.GameStates.RestartStateProperties() { isQuickPlayActive = isQuickPlayActive });
        }

        public void NavigateToMainMenuPanel()
        {
            StateProvider.SwitchTo<States.GameStates.ShowcaseState>();
        }

        public void AdjustCameraAndSpotlight()
        {
            cameraOffset = cameraTargetOffset;
            camera.transform.SetPositionAndRotation(cameraTargetOffset, cameraTargetRotation);
            camera.orthographicSize = cameraTargetOrthographicSize;
            spotlightTransform.position = spotlightOffset;
        }
    }
}
