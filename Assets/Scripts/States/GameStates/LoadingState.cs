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
            IronSource.Agent.shouldTrackNetworkState(true);
            IronSourceRewardedVideoEvents.onAdOpenedEvent += RewardedVideoOnAdOpenedEvent;
            IronSourceRewardedVideoEvents.onAdClosedEvent += RewardedVideoOnAdClosedEvent;
            IronSourceRewardedVideoEvents.onAdAvailableEvent += RewardedVideoOnAdAvailable;
            IronSourceRewardedVideoEvents.onAdUnavailableEvent += RewardedVideoOnAdUnavailable;
            IronSourceRewardedVideoEvents.onAdShowFailedEvent += RewardedVideoOnAdShowFailedEvent;
            IronSourceRewardedVideoEvents.onAdRewardedEvent += RewardedVideoOnAdRewardedEvent;
            IronSourceRewardedVideoEvents.onAdClickedEvent += RewardedVideoOnAdClickedEvent;
            LevelPlayAdFormat[] legacyAdFormats = new[] { LevelPlayAdFormat.REWARDED };
            Debug.Log("LoadingState");
            LevelPlay.OnInitSuccess += (adFormats) =>
            {
                Debug.Log("LevelPlay initialized");
                IronSource.Agent.loadRewardedVideo();
            };
            LevelPlay.OnInitFailed += (error) =>
            {
                Debug.Log("LevelPlay initialization failed");
                Debug.Log(error);
            };
            IronSource.Agent.setMetaData("is_test_suite", "enable");

            IronSourceRewardedVideoEvents.onAdAvailableEvent += (adInfo) =>
            {
                Debug.Log("IronSource rewarded video available");
                Debug.Log(adInfo);
            };
            LevelPlay.Init("2103ecc85", adFormats: legacyAdFormats);
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


        /************* RewardedVideo AdInfo Delegates *************/
        // Indicates that there’s an available ad.
        // The adInfo object includes information about the ad that was loaded successfully
        // This replaces the RewardedVideoAvailabilityChangedEvent(true) event
        void RewardedVideoOnAdAvailable(IronSourceAdInfo adInfo)
        {
            Debug.Log("Rewarded video ad available");
            Debug.Log(adInfo.adUnit);
        }
        // Indicates that no ads are available to be displayed
        // This replaces the RewardedVideoAvailabilityChangedEvent(false) event
        void RewardedVideoOnAdUnavailable()
        {
        }
        // The Rewarded Video ad view has opened. Your activity will loose focus.
        void RewardedVideoOnAdOpenedEvent(IronSourceAdInfo adInfo)
        {
        }
        // The Rewarded Video ad view is about to be closed. Your activity will regain its focus.
        void RewardedVideoOnAdClosedEvent(IronSourceAdInfo adInfo)
        {
        }
        // The user completed to watch the video, and should be rewarded.
        // The placement parameter will include the reward data.
        // When using server-to-server callbacks, you may ignore this event and wait for the ironSource server callback.
        void RewardedVideoOnAdRewardedEvent(IronSourcePlacement placement, IronSourceAdInfo adInfo)
        {
        }
        // The rewarded video ad was failed to show.
        void RewardedVideoOnAdShowFailedEvent(IronSourceError error, IronSourceAdInfo adInfo)
        {
        }
        // Invoked when the video ad was clicked.
        // This callback is not supported by all networks, and we recommend using it only if
        // it’s supported by all networks you included in your build.
        void RewardedVideoOnAdClickedEvent(IronSourcePlacement placement, IronSourceAdInfo adInfo)
        {
        }

    }
}