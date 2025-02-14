using System.Collections;
using System.Collections.Generic;
using Assets.Scripts.Patterns.SingletonPattern;
using UnityEngine;
using TMPro;

namespace Assets.Scripts.Panels
{
    public class MainMenuPanel : Panel
    {
        [SerializeField] private TMP_Text _highScoreText;
        [SerializeField] private TMP_Text _bgmText;
        [SerializeField] private TMP_Text _sfxText;

        public void SetHighScoreText(int highScore)
        {
            _highScoreText.text = highScore.ToString();
        }

        public void Play()
        {
            GameUI.I.StateProvider.SwitchTo<States.GameUIStates.PlayingState>();
            Game.I.StateProvider.SwitchTo<States.GameStates.PlayingState>();
        }

        public void ToggleBGM()
        {
            AudioManager.I.ToggleBGM();
            SetTexts();
        }

        public void ToggleSFX()
        {
            AudioManager.I.ToggleSFX();
            SetTexts();
        }

        public void SetTexts()
        {
            _bgmText.text = AudioManager.I.IsBGMOn() ? "M" : "m";
            _sfxText.text = AudioManager.I.IsSFXOn() ? "S" : "s";
        }
    }
}
