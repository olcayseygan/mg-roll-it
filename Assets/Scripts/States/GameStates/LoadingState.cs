using System.Collections;
using Assets.Scripts.Patterns.StatePattern;
using UnityEngine;
using UnityEngine.Localization.Settings;
using Assets.Scripts.StateViews;
using GooglePlayGames;
using GooglePlayGames.BasicApi;
using GooglePlayGames.BasicApi.SavedGame;

namespace Assets.Scripts.States.GameStates
{
    public class LoadingState : State<Game>
    {
        public override StateTransition<Game> OnEnter(Game self)
        {
            Game.I.StateViewHandler.SwitchTo<LoadingPanel>();
            if (PlayerController.I.GetFirstTime())
            {
                PlayerController.I.SetFirstTime(false);
                var uuid = System.Guid.NewGuid().ToString();
                PlayerController.I.SetPlayerName(uuid);
            }
            AudioManager.I.LoadAudioSettings();
            LevelPlayManager.I.Initialize();

            PlayGamesPlatform.Activate();
            PlayGamesPlatform.Instance.Authenticate((status) =>
            {
                Debug.Log("PlayGamesPlatform authentication");
                Debug.Log(status.ToString());
                byte[] bytes = System.Text.Encoding.UTF8.GetBytes("Hello, world!");
                SavedGameMetadataUpdate update = new SavedGameMetadataUpdate.Builder().WithUpdatedDescription("Saved game at " + System.DateTime.Now).Build();
            });

            self.StartCoroutine(OnEnterCoroutine(self));
            return base.OnEnter(self);
        }

        private IEnumerator OnEnterCoroutine(Game self)
        {
            yield return LocalizationSettings.InitializationOperation;
            yield return new WaitForSeconds(1.5f);
            self.StateProvider.SwitchTo<InstantiationState>(new InstantiationStateProperties() { canSkipToPlaying = false });
        }
    }
}