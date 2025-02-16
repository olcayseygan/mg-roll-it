using System.Collections;
using Assets.Scripts.Patterns.StatePattern;
using GoogleMobileAds.Api;
using UnityEngine;

namespace Assets.Scripts.States.GameStates
{
    public class AwakeState : State<Game>
    {
        public override StateTransition<Game> OnEnter(Game self)
        {
            QualitySettings.vSyncCount = 0;
            Application.targetFrameRate = 60;
            // NOTE: muzik yapilana kadar kapali.
            // AudioManager.I.PlayBGM();
            MobileAds.Initialize(initStatus => {
                Debug.Log("AdMob initialized");
            });
            return self.StateProvider.FindState<InitializationState>(new InitializationStateProperties() { isQuickPlayActive = false });
        }

    }
}