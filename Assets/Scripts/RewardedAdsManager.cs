using System.Collections;
using System.Collections.Generic;
using Assets.Scripts.Patterns.SingletonPattern;
using UnityEngine;
using UnityEngine.Advertisements;
using UnityEngine.Events;

namespace Assets.Scripts
{
    public class RewardedAdsManager : SingletonProvider<RewardedAdsManager>, IUnityAdsLoadListener, IUnityAdsShowListener
    {
        [SerializeField] private string _androidAdUnitId = "Rewarded_Android";
        [SerializeField] private string _iOSAdUnitId = "Rewarded_iOS";
        private string _adUnitId = null;

        public bool isAdLoaded = false;

        [SerializeField] private UnityEvent _onAdsShowComplete;
        [SerializeField] private UnityEvent _onAdsShowFailed;

        protected override void Awake()
        {
            base.Awake();
#if UNITY_IOS
            _adUnitId = _iOSAdUnitId;
#elif UNITY_ANDROID
            _adUnitId = _androidAdUnitId;
#elif UNITY_EDITOR
            _adUnitId = _androidAdUnitId;
#endif
        }

        public void LoadAd()
        {
            Debug.Log("Loading Ad: " + _adUnitId);
            Advertisement.Load(_adUnitId, this);
        }

        public void OnUnityAdsAdLoaded(string placementId)
        {
            Debug.Log("Ad Loaded: " + placementId);
            if (placementId.Equals(_adUnitId))
            {
                isAdLoaded = true;
            }
        }

        public void OnUnityAdsFailedToLoad(string placementId, UnityAdsLoadError error, string message)
        {
            isAdLoaded = false;
        }

        public void OnUnityAdsShowFailure(string placementId, UnityAdsShowError error, string message)
        {
            isAdLoaded = false;
        }

        public void OnUnityAdsShowStart(string placementId)
        {
            throw new System.NotImplementedException();
        }

        public void OnUnityAdsShowClick(string placementId)
        {
            throw new System.NotImplementedException();
        }

        public void OnUnityAdsShowComplete(string placementId, UnityAdsShowCompletionState showCompletionState)
        {
            throw new System.NotImplementedException();
        }
    }
}
