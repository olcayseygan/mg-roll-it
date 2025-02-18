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
        [SerializeField] private TMP_Text _coinsText;
        [SerializeField] private TMP_Text _scoreText;
        [SerializeField] private TMP_Text _highScoreText;
        [SerializeField] private GameObject _watchAdButtonGameObject;
        [SerializeField] private GameObject _continueButtonGameObject;
        [SerializeField] private GameObject _doubleCoinsButtonGameObject;
        [SerializeField] private TMP_Text _doubleCoinsButtonText;

        public void SetCoinsText(int coins)
        {
            _coinsText.text = coins.ToString();
        }

        public void SetScoreText(int score)
        {
            _scoreText.text = score.ToString();
        }

        public void SetHighScoreText(int highScore)
        {
            _highScoreText.text = highScore.ToString();
        }

        public void ShowContinueButton()
        {
            _continueButtonGameObject.SetActive(true);
        }

        public void HideContinueButton()
        {
            _continueButtonGameObject.SetActive(false);
        }

        public void SetDoubleCoinsButtonText(int coins)
        {
            _doubleCoinsButtonText.text = $"+{coins} COINS";
        }

        public void ShowDoubleCoinsButton()
        {
            _doubleCoinsButtonGameObject.SetActive(true);
        }

        public void HideDoubleCoinsButton()
        {
            _doubleCoinsButtonGameObject.SetActive(false);
        }

        public void ContinueButton_Click()
        {
            Game.I.StateProvider.SwitchTo<States.GameStates.ContinueState>();
        }

        public void DoubleCoinsButton_Click()
        {
            PlayerController.I.AddCoins(Game.I.GetCurrentRunCoins());
            SetCoinsText(PlayerController.I.GetCoins());
            HideDoubleCoinsButton();
        }


        public void RetryButton_Click()
        {
            Game.I.StateProvider.SwitchTo<States.GameStates.CleaningState>(new States.GameStates.CleaningStateProperties() { canSkipToPlaying = true });
        }

        public void MainMenu_Click()
        {
            Game.I.StateProvider.SwitchTo<States.GameStates.CleaningState>(new States.GameStates.CleaningStateProperties() { canSkipToPlaying = false });
        }
    }
}
