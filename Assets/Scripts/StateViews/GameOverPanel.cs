using System;
using Assets.Scripts.Patterns.StatePattern.Plugins;
using TMPro;
using UnityEngine;
using UnityEngine.Localization;
using UnityEngine.Localization.Components;

namespace Assets.Scripts.StateViews
{
    public class GameOverPanel : StateViewPanel
    {
        [SerializeField] private TMP_Text _goldsText;
        [SerializeField] private TMP_Text _scoreText;
        [SerializeField] private TMP_Text _highScoreText;
        [SerializeField] private GameObject _continueWithGoldButtonGameObject;
        [SerializeField] private GameObject _continueButtonGameObject;
        [SerializeField] private GameObject _doubleGoldButtonGameObject;
        [SerializeField] private TMP_Text _doubleGoldButtonText;

        public override void Show()
        {
            _continueButtonGameObject.SetActive(Game.I.isContinuationEnabled && PlayerController.I.GetGolds() >= 20);
            _goldsText.text = PlayerController.I.GetGolds().ToString();
            _scoreText.text = Game.I.GetCurrentRunScore().ToString();
            _highScoreText.text = PlayerController.I.GetHighScore().ToString();
            var stringEvent = _doubleGoldButtonText.GetComponent<LocalizeStringEvent>();
            stringEvent.StringReference = new LocalizedString { TableReference = "Table", TableEntryReference = "UI_GAME_OVER_DOUBLE_GOLD_BUTTON_TEXT", Arguments = new[] { Game.I.GetCurrentRunGolds().ToString() } };
            base.Show();
        }

        public void ShowContinueButton()
        {
            _continueButtonGameObject.SetActive(true);
        }

        public void HideContinueButton()
        {
            _continueButtonGameObject.SetActive(false);
        }

        public void ShowDoubleGoldButton()
        {
            _doubleGoldButtonGameObject.SetActive(true);
        }

        public void HideDoubleGoldButton()
        {
            _doubleGoldButtonGameObject.SetActive(false);
        }

        public void SetGoldsText(int gold)
        {
            _goldsText.text = gold.ToString();
        }

        public void ContinueWithGoldButton_Click()
        {
            _continueWithGoldButtonGameObject.SetActive(false);
            PlayerController.I.RemoveGold(20);
            Game.I.isContinuationEnabled = false;
            Game.I.StateViewHandler.Get<PlayingPanel>().SetGoldText(PlayerController.I.GetGolds(), Game.I.GetCurrentRunGolds());
            Game.I.StateProvider.SwitchTo<States.GameStates.ContinueState>();
        }

        public void ContinueButton_Click()
        {
            HideContinueButton();
            LevelPlayManager.I.ShowRewardedVideo("Game_Over__Continue");
        }

        public void DoubleGoldButton_Click()
        {
            HideDoubleGoldButton();
            LevelPlayManager.I.ShowRewardedVideo("Game_Over__Double_Gold");
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
