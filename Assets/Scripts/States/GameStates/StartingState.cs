using System.Collections;
using Assets.Scripts.Patterns.StatePattern;
using GoogleMobileAds.Api;
using UnityEngine;

namespace Assets.Scripts.States.GameStates
{
    public class StartingState : State<Game>
    {
        public override StateTransition<Game> OnEnter(Game self)
        {
            AudioManager.I.LoadAudioSettings();
            MobileAds.Initialize(initStatus =>
            {
                Debug.Log("AdMob initialized");
            });
            return self.StateProvider.FindState<InstantiationState>(new InstantiationStateProperties() { canSkipToPlaying = false });
        }

    }
}