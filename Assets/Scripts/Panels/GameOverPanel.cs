using System.Collections;
using System.Collections.Generic;
using Assets.Scripts.Patterns.SingletonPattern;
using UnityEngine;
using TMPro;
using Assets.Scripts.States;
using UnityEngine.Localization.Components;
using UnityEngine.Localization;

namespace Assets.Scripts.Panels
{
    public class GameOverPanel : Panel
    {
        [SerializeField] private TMP_Text _goldsText;
        [SerializeField] private TMP_Text _scoreText;
        [SerializeField] private TMP_Text _highScoreText;
        [SerializeField] private GameObject _watchAdButtonGameObject;
        [SerializeField] private GameObject _continueButtonGameObject;
        [SerializeField] private GameObject _doubleGoldButtonGameObject;
        [SerializeField] private TMP_Text _doubleGoldButtonText;

        public void SetGoldsText(int golds)
        {
            _goldsText.text = golds.ToString();
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

        public void SetDoubleGoldButtonText(int golds)
        {
            var stringEvent = _doubleGoldButtonText.GetComponent<LocalizeStringEvent>();
            stringEvent.StringReference = new LocalizedString { TableReference = "Table", TableEntryReference = "UI_GAME_OVER_DOUBLE_GOLD_BUTTON_TEXT", Arguments = new[] { golds.ToString() } };
        }

        public void ShowDoubleGoldButton()
        {
            _doubleGoldButtonGameObject.SetActive(true);
        }

        public void HideDoubleGoldButton()
        {
            _doubleGoldButtonGameObject.SetActive(false);
        }

        public void ContinueButton_Click()
        {
            Game.I.StateProvider.SwitchTo<States.GameStates.ContinueState>();
        }

        public void DoubleGoldButton_Click()
        {
            PlayerController.I.AddGold(Game.I.GetCurrentRunGolds());
            SetGoldsText(PlayerController.I.GetGolds());
            HideDoubleGoldButton();
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
