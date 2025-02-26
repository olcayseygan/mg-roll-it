using System.Collections;
using Assets.Scripts.Patterns.StatePattern;
using UnityEngine;
using UnityEngine.Localization.Settings;
using Assets.Scripts.StateViews;
using GooglePlayGames;

namespace Assets.Scripts.States.GameStates
{
    public class LoadingState : State<Game>
    {
        public override StateTransition<Game> OnEnter(Game self)
        {
            Game.I.StateViewHandler.SwitchTo<LoadingPanel>();

            LevelPlayManager.I.Initialize();

            AudioManager.I.LoadAudioSettings();
            PlayGamesPlatform.Activate();
            PlayGamesPlatform.Instance.Authenticate((status) =>
            {
                Debug.Log("PlayGamesPlatform authentication");
                Debug.Log(status.ToString());
            });

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