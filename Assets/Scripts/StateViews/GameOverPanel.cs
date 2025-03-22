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
            _continueWithGoldButtonGameObject.SetActive(Game.I.isContinuationEnabled && PlayerController.I.GetGolds() >= 20);
            _continueButtonGameObject.SetActive(Game.I.isContinuationEnabled && LevelPlayManager.I.CheckRewardedVideoAvailability("Game_Over__Continue"));
            _doubleGoldButtonGameObject.SetActive(Game.I.GetCurrentRunGolds() > 0 && !Game.I.hasUsedDoubleGold && LevelPlayManager.I.CheckRewardedVideoAvailability("Game_Over__Double_Gold"));
            _goldsText.text = PlayerController.I.GetGolds().ToString();
            _scoreText.text = Game.I.GetCurrentRunScore().ToString();
            _highScoreText.text = PlayerController.I.GetHighScore().ToString();
            LocalizationController.I.SetStringReference(_doubleGoldButtonText.gameObject, "UI_GAME_OVER_DOUBLE_GOLD_BUTTON_TEXT", Game.I.GetCurrentRunGolds().ToString());
            base.Show();
        }

        public void SetGoldsText(int gold)
        {
            _goldsText.text = gold.ToString();
        }

        public void ContinueWithGoldButton_Click()
        {
            _continueWithGoldButtonGameObject.SetActive(false);
            _continueButtonGameObject.SetActive(false);
            PlayerController.I.RemoveGold(20);
            Game.I.isContinuationEnabled = false;
            Game.I.StateViewHandler.Get<PlayingPanel>().SetGoldText(PlayerController.I.GetGolds(), Game.I.GetCurrentRunGolds());
            Game.I.StateProvider.SwitchTo<States.GameStates.ContinueState>();
        }

        public void ContinueButton_Click()
        {
            _continueWithGoldButtonGameObject.SetActive(false);
            _continueButtonGameObject.SetActive(false);
            LevelPlayManager.I.ShowRewardedVideo("Game_Over__Continue");
        }

        public void DoubleGoldButton_Click()
        {
            _doubleGoldButtonGameObject.SetActive(false);
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
