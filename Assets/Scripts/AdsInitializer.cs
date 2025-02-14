using System.Collections;
using System.Collections.Generic;
using Assets.Scripts.Patterns.SingletonPattern;
using UnityEngine;
using GoogleMobileAds.Api;

namespace Assets.Scripts
{
    public class AdsInitializer : SingletonProvider<AdsInitializer>
    {
        [SerializeField] private string _androidGameId;
        [SerializeField] private string _iOSGameId;
        [SerializeField] private bool _testMode = true;
        private string _gameId;

        private string _appId = "ca-app-pub-2270871235119756~8522140819";
        private RewardedAd _rewardedAd;

        private string appKey = "2103ecc85";

        public bool isInitialized = false;

        [SerializeField] private GameObject _adsFailedPanelGameObject;
        [SerializeField] private TMPro.TMP_Text _adsFailedText;
        [SerializeField] private GameObject _adsSucceededPanelGameObject;
        [SerializeField] private GameObject _adsInitPanelGameObject;
        [SerializeField] private GameObject _adsInit2PanelGameObject;

        protected override void Awake()
        {
            base.Awake();
            // #if UNITY_IOS
            //             _gameId = _iOSGameId;
            // #elif UNITY_ANDROID
            //             _gameId = _androidGameId;
            // #elif UNITY_EDITOR
            //             _gameId = _androidGameId;
            // #endif

            // LevelPlay.Init(appKey, adFormats: new[] { LevelPlayAdFormat.REWARDED });
            // LevelPlay.OnInitSuccess += SdkInitializationCompletedEvent;
            // LevelPlay.OnInitFailed += SdkInitializationFailedEvent;
            RewardedAd.Load("ca-app-pub-2270871235119756/5704405785", new AdRequest(), (RewardedAd ad, LoadAdError error) =>
            {
                if (error != null)
                {
                    Debug.Log("Rewarded ad failed to load: " + error);
                    _adsFailedPanelGameObject.SetActive(true);
                    return;
                }
                _rewardedAd = ad;
                _adsSucceededPanelGameObject.SetActive(true);
                Debug.Log("Rewarded ad loaded");
            });
            _adsInitPanelGameObject.SetActive(true);
        }

        // public void OnInitializationComplete()
        // {
        //     Debug.Log("Unity Ads initialization complete.");
        //     isInitialized = true;
        //     _adsSucceededPanelGameObject.SetActive(true);
        // }

        // public void OnInitializationFailed(UnityAdsInitializationError error, string message)
        // {
        //     Debug.Log($"Unity Ads Initialization Failed: {error.ToString()} - {message}");
        //     isInitialized = false;
        //     _adsFailedPanelGameObject.SetActive(true);
        //     _adsFailedText.text = message;
        // }
    }
}
