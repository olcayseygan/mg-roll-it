using System.Collections;
using System.Collections.Generic;
using Assets.Scripts.Patterns.SingletonPattern;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using Assets.Scripts.Patterns.StatePattern.Plugins;

namespace Assets.Scripts.StateViews
{
    public class MainMenuPanel : StateViewPanel
    {
        [SerializeField] private TMP_Text _goldsText;
        [SerializeField] private TMP_Text _highScoreText;
        [SerializeField] private TMP_Text _gamesPlayedText;

        [SerializeField] private Button _gainGoldButton;

        public override void Show()
        {
            _goldsText.text = PlayerController.I.GetGolds().ToString();
            _highScoreText.text = PlayerController.I.GetHighScore().ToString();
            _gamesPlayedText.text = PlayerController.I.GetPlayedGames().ToString();
            _gainGoldButton.interactable = LevelPlayManager.I.CheckRewardedVideoAvailability("Home_Screen__Gain_Gold");
            base.Show();
        }

        public void SetGoldsText(int gold)
        {
            _goldsText.text = gold.ToString();
        }

        public void PlayButton_Click()
        {
            Game.I.StateProvider.SwitchTo<States.GameStates.LoadAdState>();
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
