using System.Collections;
using System.Collections.Generic;
using Assets.Scripts.Patterns.SingletonPattern;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

namespace Assets.Scripts.Panels
{
    public class MainMenuPanel : Panel
    {
        [SerializeField] private TMP_Text _goldsText;
        [SerializeField] private TMP_Text _highScoreText;
        [SerializeField] private TMP_Text _gamesPlayedText;

        [SerializeField] private TMP_Text _sfxText;

        public void SetGoldsText(int golds)
        {
            _goldsText.text = golds.ToString();
        }

        public void SetHighScoreText(int highScore)
        {
            _highScoreText.text = highScore.ToString();
        }

        public void SetGamesPlayedText(int gamesPlayed)
        {
            _gamesPlayedText.text = gamesPlayed.ToString();
        }

        public void PlayButton_Click()
        {
            Game.I.StateProvider.SwitchTo<States.GameStates.LoadAdState>();
        }

        public void ShopButton_Click()
        {
            GameUI.I.StateProvider.SwitchTo<States.GameUIStates.ShopState>();
        }

        public void InventoryButton_Click()
        {
            GameUI.I.StateProvider.SwitchTo<States.GameUIStates.InventoryState>();
        }

        public void SettingsButton_Click()
        {
            GameUI.I.StateProvider.SwitchTo<States.GameUIStates.SettingsState>();
        }
    }
}
