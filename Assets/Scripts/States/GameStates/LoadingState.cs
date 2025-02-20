using System.Collections;
using Assets.Scripts.Patterns.StatePattern;
using GoogleMobileAds.Api;
using UnityEngine;
using UnityEngine.Localization;
using UnityEngine.Localization.Settings;

namespace Assets.Scripts.States.GameStates
{
    public class LoadingState : State<Game>
    {
        public override StateTransition<Game> OnEnter(Game self)
        {
            GameUI.I.StateProvider.SwitchTo<GameUIStates.LoadingState>();
            QualitySettings.vSyncCount = 0;
            Application.targetFrameRate = PlayerController.I.GetMaxFPS();

            AudioManager.I.LoadAudioSettings();
            // MobileAds.Initialize(initStatus =>
            // {
            //     Debug.Log("AdMob initialized");
            // });
            // PlayerController.I.SetHighScore(0);
            // PlayerController.I.OwnAllCubeSkins();
            // PlayerPrefs.DeleteAll();
            self.StartCoroutine(OnEnterCoroutine(self));
            return base.OnEnter(self);
        }

        private IEnumerator OnEnterCoroutine(Game self) {
            Debug.Log("LoadingState.OnEnterCoroutine");
            yield return LocalizationSettings.InitializationOperation;
            yield return new WaitForSeconds(1f);
            Debug.Log("LocalizationSettings.InitializationOperation");
            self.StateProvider.SwitchTo<InstantiationState>(new InstantiationStateProperties() { canSkipToPlaying = false });
        }
    }
}