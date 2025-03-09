using System.Collections;
using System.Collections.Generic;
using Assets.Scripts.Patterns.SingletonPattern;
using Assets.Scripts.StateViews;
using UnityEngine;

namespace Assets.Scripts
{
    public class LevelPlayManager : SingletonProvider<LevelPlayManager>
    {
        protected override void Awake()
        {
            base.Awake();
            RegisterRewardedVideoEvents();
            RegisterSDKEvents();
        }

        public void Initialize()
        {
            // IronSource.Agent.setMetaData("is_test_suite", "enable");
            // IronSource.Agent.setAdaptersDebug(true);
            IronSource.Agent.shouldTrackNetworkState(true);
            IronSource.Agent.validateIntegration();
            IronSource.Agent.init("2103ecc85", PlayerController.I.GetPlayerName(), IronSourceAdUnits.REWARDED_VIDEO);
        }

        private void RegisterRewardedVideoEvents()
        {
            IronSourceRewardedVideoEvents.onAdOpenedEvent += RewardedVideoOnAdOpenedEvent;
            IronSourceRewardedVideoEvents.onAdClosedEvent += RewardedVideoOnAdClosedEvent;
            IronSourceRewardedVideoEvents.onAdAvailableEvent += RewardedVideoOnAdAvailable;
            IronSourceRewardedVideoEvents.onAdUnavailableEvent += RewardedVideoOnAdUnavailable;
            IronSourceRewardedVideoEvents.onAdShowFailedEvent += RewardedVideoOnAdShowFailedEvent;
            IronSourceRewardedVideoEvents.onAdRewardedEvent += RewardedVideoOnAdRewardedEvent;
            IronSourceRewardedVideoEvents.onAdClickedEvent += RewardedVideoOnAdClickedEvent;
        }

        private void RegisterSDKEvents() {
            IronSourceEvents.onSdkInitializationCompletedEvent += SdkInitializationCompletedEvent;
        }

        public void LoadRewardedVideo()
        {
            IronSource.Agent.loadRewardedVideo();
        }

        public void ShowRewardedVideo(string placementName)
        {
            if (!CheckRewardedVideoAvailability(placementName))
            {
                return;
            }

            IronSource.Agent.showRewardedVideo(placementName);
        }

        private void RewardedVideoOnAdAvailable(IronSourceAdInfo adInfo)
        {
            if (adInfo.adUnit == IronSourceAdUnits.REWARDED_VIDEO)
            {
                Game.I.StateViewHandler.Get<MainMenuPanel>().UpdateGaingGoldButtonInteractable();
            }
        }

        private void RewardedVideoOnAdUnavailable()
        {
            Debug.Log("No rewarded video ad available");
        }

        private void RewardedVideoOnAdOpenedEvent(IronSourceAdInfo adInfo)
        {
            Debug.Log("Rewarded video ad opened");
        }

        private void RewardedVideoOnAdClosedEvent(IronSourceAdInfo adInfo)
        {
            Debug.Log("Rewarded video ad closed");
        }

        private void RewardedVideoOnAdRewardedEvent(IronSourcePlacement placement, IronSourceAdInfo adInfo)
        {
            switch (placement.getPlacementName())
            {
                case "Game_Over__Continue":
                    Game.I.StateProvider.SwitchTo<States.GameStates.ContinueState>();
                    break;
                case "Game_Over__Double_Gold":
                    PlayerController.I.AddGold(Game.I.GetCurrentRunGolds());
                    Game.I.StateViewHandler.Get<GameOverPanel>().SetGoldsText(PlayerController.I.GetGolds());
                    break;

                case "Home_Screen__Gain_Gold":
                    PlayerController.I.AddGold(42);
                    Game.I.StateViewHandler.Get<MainMenuPanel>().SetGoldsText(PlayerController.I.GetGolds());
                    Game.I.StateViewHandler.Get<MainMenuPanel>().UpdateGaingGoldButtonInteractable();
                    break;
            }
        }

        private void RewardedVideoOnAdShowFailedEvent(IronSourceError error, IronSourceAdInfo adInfo)
        {
            Debug.Log("Rewarded video failed to show: " + error.getDescription());
        }

        private void RewardedVideoOnAdClickedEvent(IronSourcePlacement placement, IronSourceAdInfo adInfo)
        {
            Debug.Log("Rewarded video ad clicked");
        }

        private void SdkInitializationCompletedEvent()
        {
            Debug.Log("IronSource SDK initialized");
            IronSource.Agent.launchTestSuite();
        }

        public bool CheckRewardedVideoAvailability(string placementName)
        {
            return IronSource.Agent.isRewardedVideoAvailable() &&
                   IronSource.Agent.getPlacementInfo(placementName) != null &&
                   !IronSource.Agent.isRewardedVideoPlacementCapped(placementName);
        }
    }
}
