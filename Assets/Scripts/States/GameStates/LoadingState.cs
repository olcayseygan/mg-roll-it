using System.Collections;
using Assets.Scripts.Patterns.StatePattern;
using GoogleMobileAds.Api;
using UnityEngine;
using UnityEngine.Localization;
using UnityEngine.Localization.Settings;
using GooglePlayGames;
using Assets.Scripts.StateViews;
using com.unity3d.mediation;

namespace Assets.Scripts.States.GameStates
{
    public class LoadingState : State<Game>
    {
        public override StateTransition<Game> OnEnter(Game self)
        {
            Game.I.StateViewHandler.SwitchTo<LoadingPanel>();
            QualitySettings.vSyncCount = 0;
            Application.targetFrameRate = PlayerController.I.GetMaxFPS();

            LevelPlayAdFormat[] legacyAdFormats = new[] { LevelPlayAdFormat.REWARDED };
            Debug.Log("LoadingState");
            LevelPlay.OnInitSuccess += (adFormats) =>
            {
                Debug.Log("LevelPlay initialized");
            };
            LevelPlay.OnInitFailed += (error) =>
            {
                Debug.Log("LevelPlay initialization failed");
                Debug.Log(error);
            };
            IronSource.Agent.setMetaData("is_test_suite", "enable");
            LevelPlay.Init("2103ecc85", adFormats: legacyAdFormats);

            IronSourceEvents.onSdkInitializationCompletedEvent += () =>
            {
                Debug.Log("IronSource initialized");
                IronSource.Agent.launchTestSuite();
            };
            AudioManager.I.LoadAudioSettings();
            // MobileAds.Initialize(initStatus =>
            // {
            //     Debug.Log("AdMob initialized");
            // });
            // PlayerController.I.SetHighScore(0);
            // PlayerController.I.OwnAllCubeSkins();
            // PlayerPrefs.DeleteAll();
            // PlayGamesPlatform.DebugLogEnabled = true;
            // PlayGamesPlatform.Activate();
            // PlayGamesPlatform.Instance.Authenticate((status) =>
            // {
            //     Debug.Log("PlayGamesPlatform authentication");
            //     Debug.Log(status.ToString());
            // });

            self.StartCoroutine(OnEnterCoroutine(self));
            return base.OnEnter(self);
        }

        private IEnumerator OnEnterCoroutine(Game self)
        {
            yield return LocalizationSettings.InitializationOperation;
            yield return new WaitForSeconds(1f);
            self.StateProvider.SwitchTo<InstantiationState>(new InstantiationStateProperties() { canSkipToPlaying = false });
        }
    }
}