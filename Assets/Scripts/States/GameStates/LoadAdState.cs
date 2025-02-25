using System.Collections;
using Assets.Scripts.Patterns.StatePattern;
using GoogleMobileAds.Api;
using UnityEngine;

namespace Assets.Scripts.States.GameStates
{
    public class LoadAdState : State<Game>
    {
        public override StateTransition<Game> OnEnter(Game self)
        {
            // RewardedAd.Load(
            //     "ca-app-pub-2270871235119756/5704405785",
            //     new AdRequest(),
            //     (ad, error) =>
            //     {
            //         if (error != null || ad == null)
            //         {
            //             Debug.LogError("Rewarded ad failed to load an ad " +
            //                          "with error : " + error);
            //             // self.StartCoroutine(LoadAd(self, false, error.GetCode().ToString() + error.GetMessage()));
            //             return;
            //         }

            //         Debug.Log("Rewarded ad loaded with response : "
            //                 + ad.GetResponseInfo());

            //         // self.StartCoroutine(LoadAd(self, true, ""));
            //         self.rewardedAd = ad;
            //     });

            // LevelPlayManager.I.LoadRewardedVideo();

            return self.StateProvider.FindState<PlayingState>(new PlayingStateProperties() { isFreshRun = true });
        }

        private IEnumerator LoadAd(Game self, bool isRewarded, string message)
        {
            yield return null;
            if (isRewarded)
            {
                self.adsSuccessObject.SetActive(true);
            }
            else
            {
                self.adsFailObject.SetActive(true);
                self.adsFailText.text = message;
            }
        }
    }
}