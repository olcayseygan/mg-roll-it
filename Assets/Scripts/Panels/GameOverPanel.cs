using System.Collections;
using System.Collections.Generic;
using Assets.Scripts.Patterns.SingletonPattern;
using UnityEngine;
using TMPro;

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
            if (!Game.I.canContinue)
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
            if (!Game.I.canContinue)
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
            // RewardedAdsManager.I.ShowAd();
            HideWatchAdButton();
        }

        public void Continue()
        {
            Game.I.ContinueGame();
        }

        public void Retry()
        {
            Game.I.RestartGame(true);
        }

        public void MainMenu()
        {
            Game.I.RestartGame(false);
        }
    }
}
