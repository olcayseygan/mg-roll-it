using System.Collections;
using System.Collections.Generic;
using Assets.Scripts.Patterns.SingletonPattern;
using UnityEngine;
using TMPro;
using Assets.Scripts.States;

namespace Assets.Scripts.Panels
{
    public class GameOverPanel : Panel
    {
        [SerializeField] private TMP_Text _scoreText;
        [SerializeField] private TMP_Text _highScoreText;
        [SerializeField] private GameObject _watchAdButtonGameObject;
        [SerializeField] private GameObject _continueButtonGameObject;

        public void SetScoreText(int score)
        {
            _scoreText.text = score.ToString();
        }

        public void SetHighScoreText(int highScore)
        {
            _highScoreText.text = highScore.ToString();
        }

        public void ShowWatchAdButton()
        {
            if (!Game.I.isContinuationEnabled)
            {
                return;
            }

            _watchAdButtonGameObject.SetActive(true);
        }

        public void HideWatchAdButton()
        {
            _watchAdButtonGameObject.SetActive(false);
        }

        public void ShowContinueButton()
        {
            if (!Game.I.isContinuationEnabled)
            {
                return;
            }

            _continueButtonGameObject.SetActive(true);
        }

        public void HideContinueButton()
        {
            _continueButtonGameObject.SetActive(false);
        }

        public void WatchAd()
        {
            Game.I.ShowRewardedAd();
            HideWatchAdButton();
        }

        public void Continue()
        {
            Game.I.ContinueGame();
        }

        public void Retry()
        {
            Game.I.StateProvider.SwitchTo<States.GameStates.CleaningState>(new States.GameStates.CleaningStateProperties() { canSkipToPlaying = true });
        }

        public void MainMenu()
        {
            Game.I.StateProvider.SwitchTo<States.GameStates.CleaningState>(new States.GameStates.CleaningStateProperties() { canSkipToPlaying = false });
        }
    }
}
