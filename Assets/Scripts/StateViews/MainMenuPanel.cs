using System.Collections;
using System.Collections.Generic;
using Assets.Scripts.Patterns.SingletonPattern;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using Assets.Scripts.Patterns.StatePattern.Plugins;
using Assets.Scripts.States.GameStates;

namespace Assets.Scripts.StateViews
{
    public class MainMenuPanel : StateViewPanel
    {
        [SerializeField] private TMP_Text _goldsText;
        [SerializeField] private TMP_Text _highScoreText;
        [SerializeField] private TMP_Text _gamesPlayedText;

        [SerializeField] private Button _gainGoldButton;

        private const float DOUBLE_PRESS_INTERVAL = 0.5f;
        private float _lastBackPressTime = 0f;


        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                if (Time.time - _lastBackPressTime < DOUBLE_PRESS_INTERVAL)
                {
                    Application.Quit();
                }
                else
                {
                    _lastBackPressTime = Time.time;
                }
            }
        }

        public override void Show()
        {
            _goldsText.text = PlayerController.I.GetGolds().ToString();
            _highScoreText.text = PlayerController.I.GetHighScore().ToString();
            _gamesPlayedText.text = PlayerController.I.GetPlayedGames().ToString();
            UpdateGaingGoldButtonInteractable();
            base.Show();
        }

        public void SetGoldsText(int gold)
        {
            _goldsText.text = gold.ToString();
        }

        public void UpdateGaingGoldButtonInteractable()
        {
            _gainGoldButton.gameObject.SetActive(LevelPlayManager.I.CheckRewardedVideoAvailability("Home_Screen__Gain_Gold"));
        }

        public void PlayButton_Click()
        {
            Game.I.StateProvider.SwitchTo<PlayingState>(new PlayingStateProperties() { isFreshRun = true });
        }

        public void ShopButton_Click()
        {
            Game.I.StateViewHandler.SwitchTo<ShopPanel>();
        }

        public void InventoryButton_Click()
        {
            Game.I.StateViewHandler.SwitchTo<InventoryPanel>();
        }

        public void SettingsButton_Click()
        {
            Game.I.StateViewHandler.SwitchTo<SettingsPanel>();
        }

        public void GainGoldButton_Click()
        {
            LevelPlayManager.I.ShowRewardedVideo("Home_Screen__Gain_Gold");
        }
    }
}
