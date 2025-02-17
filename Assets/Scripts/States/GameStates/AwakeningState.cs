using System.Collections;
using Assets.Scripts.Patterns.StatePattern;
using GoogleMobileAds.Api;
using UnityEngine;

namespace Assets.Scripts.States.GameStates
{
    public class AwakeningState : State<Game>
    {
        public override StateTransition<Game> OnEnter(Game self)
        {
            QualitySettings.vSyncCount = 0;
            Application.targetFrameRate = 60;
            return self.StateProvider.FindState<StartingState>();
        }
    }
}