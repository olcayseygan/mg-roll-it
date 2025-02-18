using System.Collections.Generic;
using Assets.Scripts.Patterns.SingletonPattern;
using Assets.Scripts.Patterns.StatePattern;
using GoogleMobileAds.Api;
using UnityEngine;

namespace Assets.Scripts
{
    public class Game : SingletonProvider<Game>
    {
        private const float LERP_SPEED = 5f;

        public StateProvider<Game> StateProvider { get; set; }

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

        public List<byte> inputList = new();

        public GameObject adsSuccessObject;
        public GameObject adsFailObject;
        public TMPro.TMP_Text adsFailText;



        protected override void Awake()
        {
            base.Awake();
            StateProvider = new StateProvider<Game>(this);
            StateProvider.RegisterState(new States.GameStates.AwakeningState());
            StateProvider.RegisterState(new States.GameStates.CleaningState());
            StateProvider.RegisterState(new States.GameStates.ContinueState());
            StateProvider.RegisterState(new States.GameStates.GameOverState());
            StateProvider.RegisterState(new States.GameStates.InstantiationState());
            StateProvider.RegisterState(new States.GameStates.LoadAdState());
            StateProvider.RegisterState(new States.GameStates.PlayingState());
            StateProvider.RegisterState(new States.GameStates.ShowcaseState());
            StateProvider.RegisterState(new States.GameStates.StartingState());
            StateProvider.SwitchTo<States.GameStates.AwakeningState>();
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
                puppy.transform.position = Vector3.Lerp(puppy.transform.position, smoothPosition, Time.deltaTime * LERP_SPEED);
            }

            StateProvider.Update();
        }


        public void AdjustCameraAndSpotlight()
        {
            cameraOffset = cameraTargetOffset;
            camera.transform.SetPositionAndRotation(cameraTargetOffset, cameraTargetRotation);
            camera.orthographicSize = cameraTargetOrthographicSize;
            spotlightTransform.position = spotlightOffset;
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
        public void AddCurrentRunScore(int score) => _currentRunScore += score;

        private int _currentRunHighScore = 0;
        public int GetCurrentRunHighScore() => _currentRunHighScore;
        public void SetCurrentRunHighScore(int highScore) => _currentRunHighScore = highScore;

        private int _currentRunCoins = 0;
        public int GetCurrentRunCoins() => _currentRunCoins;
        public void AddCurrentRunCoins(int coins) => _currentRunCoins += coins;

        public void ResetCurrentRun()
        {
            _currentRunScore = 0;
            _currentRunHighScore = PlayerController.I.GetHighScore();
            _currentRunCoins = 0;
        }
        #endregion
    }
}
