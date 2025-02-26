using System.Collections.Generic;
using Assets.Scripts.Patterns.SingletonPattern;
using Assets.Scripts.Patterns.StatePattern;
using Assets.Scripts.Patterns.StatePattern.Plugins;
using GoogleMobileAds.Api;
using UnityEngine;

namespace Assets.Scripts
{
    public class Game : SingletonProvider<Game>
    {
        private const float LERP_SPEED = 2.5f;
        private const float PUPPY_SPEED = 7.5f;

        public StateProvider<Game> StateProvider { get; private set; }
        public StateViewHandler<Game> StateViewHandler { get; private set; }

        public GameObject cubePrefab;

        public new Camera camera;
        public Vector3 cameraOffset;
        public Vector3 cameraTargetOffset;
        public Quaternion cameraTargetRotation;
        public float cameraTargetOrthographicSize;
        public Transform spotlightTransform;
        public Vector3 spotlightOffset;

        public bool isContinuationEnabled = true;


        public GameObject puppy;

        public RewardedAd rewardedAd;

        public bool playerHasInteracted = false;

        public GameObject adsSuccessObject;
        public GameObject adsFailObject;
        public TMPro.TMP_Text adsFailText;


        public float maxSpeed = 0.175f;
        private const float MIN_SPEED = 0.075f;
        private const float SPEED_DECREASING_RATE = 0.001f;
        [HideInInspector] public float speed;

        protected override void Awake()
        {
            if (false)
            {
                Application.logMessageReceivedThreaded += HandleLog;
                GameUI.I.debugPanel.SetActive(true);
            }

            base.Awake();
            StateProvider = new StateProvider<Game>(this);
            StateProvider.RegisterState(new States.GameStates.CleaningState());
            StateProvider.RegisterState(new States.GameStates.ContinueState());
            StateProvider.RegisterState(new States.GameStates.GameOverState());
            StateProvider.RegisterState(new States.GameStates.InstantiationState());
            StateProvider.RegisterState(new States.GameStates.PlayingState());
            StateProvider.RegisterState(new States.GameStates.ShowcaseState());
            StateProvider.RegisterState(new States.GameStates.LoadingState());

            StateViewHandler = new StateViewHandler<Game>();
            StateViewHandler.RegisterStateViewPanel(GameUI.I.mainMenuPanel);
            StateViewHandler.RegisterStateViewPanel(GameUI.I.settingsPanel);
            StateViewHandler.RegisterStateViewPanel(GameUI.I.playingPanel);
            StateViewHandler.RegisterStateViewPanel(GameUI.I.waitForActionPanel);
            StateViewHandler.RegisterStateViewPanel(GameUI.I.loadingPanel);
            StateViewHandler.RegisterStateViewPanel(GameUI.I.inventoryPanel);
            StateViewHandler.RegisterStateViewPanel(GameUI.I.shopPanel);
            StateViewHandler.RegisterStateViewPanel(GameUI.I.gameOverPanel);

            StateProvider.SwitchTo<States.GameStates.LoadingState>();
        }

        private void HandleLog(string logString, string stackTrace, LogType type)
        {
            string colorTag = type == LogType.Error ? "<color=red>" :
                type == LogType.Warning ? "<color=yellow>" :
                "<color=black>";
            string formattedMessage = $"{colorTag}{logString}{stackTrace}</color>\n";
            GameUI.I.debugText.text += formattedMessage;
        }

        private void Update()
        {
            if (Input.GetMouseButtonDown(0))
            {
                AudioManager.I.PlaySFX(AudioManager.I.screenTapClip);
            }

            if (Cube.I != null)
            {
                var smoothPosition = Cube.I.GetSmoothPosition();
                cameraOffset = Vector3.Lerp(cameraOffset, cameraTargetOffset, Time.deltaTime * LERP_SPEED);
                camera.orthographicSize = Mathf.Lerp(camera.orthographicSize, cameraTargetOrthographicSize, Time.deltaTime * LERP_SPEED);
                camera.transform.SetPositionAndRotation(
                    puppy.transform.position + cameraOffset,
                    Quaternion.Lerp(camera.transform.rotation, cameraTargetRotation, Time.deltaTime * LERP_SPEED)
                );
                spotlightTransform.position = Vector3.Lerp(spotlightTransform.position, smoothPosition + spotlightOffset, Time.deltaTime * LERP_SPEED);
                puppy.transform.position = Vector3.Lerp(puppy.transform.position, smoothPosition, Time.deltaTime * PUPPY_SPEED);

                speed = Mathf.Clamp(speed - SPEED_DECREASING_RATE * Time.deltaTime, MIN_SPEED, maxSpeed);
            }

            StateProvider.Update();
        }


        public void AdjustCameraAndSpotlight()
        {
            cameraOffset = cameraTargetOffset;
            camera.transform.SetPositionAndRotation(cameraTargetOffset, cameraTargetRotation);
            camera.orthographicSize = cameraTargetOrthographicSize;
            spotlightTransform.position = spotlightOffset;
            puppy.transform.position = Vector3.zero;
        }


        public void CreateCube()
        {
            var cubeSkin = SkinManager.I.GetSkinData(PlayerController.I.GetEquippedCubeSkinKey());
            Instantiate(cubeSkin.prefab).GetComponent<Cube>();
        }
        public void DestoryCube()
        {
            Destroy(Cube.I.gameObject);
        }

        #region Current Run
        private int _currentRunScore = 0;
        public int GetCurrentRunScore() => _currentRunScore;
        public void AddCurrentRunScore(int amount) => _currentRunScore += amount;

        private int _currentRunHighScore = 0;
        public int GetCurrentRunHighScore() => _currentRunHighScore;
        public void SetCurrentRunHighScore(int highScore) => _currentRunHighScore = highScore;

        private int _currentRunGolds = 0;
        public int GetCurrentRunGolds() => _currentRunGolds;
        public void AddCurrentRunGolds(int amount) => _currentRunGolds += amount;

        public void ResetCurrentRun()
        {
            _currentRunScore = 0;
            _currentRunHighScore = PlayerController.I.GetHighScore();
            _currentRunGolds = 0;
        }
        #endregion

        void OnApplicationPause(bool isPaused)
        {
            IronSource.Agent.onApplicationPause(isPaused);
        }
    }
}
