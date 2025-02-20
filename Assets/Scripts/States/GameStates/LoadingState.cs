using System.Collections;
using Assets.Scripts.Patterns.StatePattern;
using GoogleMobileAds.Api;
using UnityEngine;
using UnityEngine.Localization;
using UnityEngine.Localization.Settings;
using GooglePlayGames;

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