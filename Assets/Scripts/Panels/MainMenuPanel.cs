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
        [SerializeField] private TMP_Text _coinsText;
        [SerializeField] private TMP_Text _highScoreText;
        [SerializeField] private TMP_Text _gamesPlayedText;

        [SerializeField] private TMP_Text _sfxText;

        public void SetCoinsText(int coins)
        {
            _coinsText.text = coins.ToString();
        }

        public void SetHighScoreText(int highScore)
        {
            _highScoreText.text = highScore.ToString();
        }

        public void SetGamesPlayedText(int gamesPlayed)
        {
            _gamesPlayedText.text = gamesPlayed.ToString();
        }

        public void Play()
        {
            GameUI.I.StateProvider.SwitchTo<States.GameUIStates.PlayingState>();
            Game.I.StateProvider.SwitchTo<States.GameStates.LoadAdState>();
        }

        public void ToggleSFX()
        {
            AudioManager.I.ToggleSFX();
            RefreshAudioToggleColors();
        }

        public void RefreshAudioToggleColors()
        {
            _sfxText.text = AudioManager.I.IsSFXOn() ? "UNMUTE" : "MUTE";
        }
    }
}
