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
        [SerializeField] private TMP_Text _highScoreText;

        [SerializeField] private Image _bgmImage;
        [SerializeField] private Image _sfxImage;

        [SerializeField] private Color _onColor;
        [SerializeField] private Color _offColor;

        public void SetHighScoreText(int highScore)
        {
            _highScoreText.text = highScore.ToString();
        }

        public void Play()
        {
            GameUI.I.StateProvider.SwitchTo<States.GameUIStates.PlayingState>();
            Game.I.StateProvider.SwitchTo<States.GameStates.LoadAdState>();
        }

        public void ToggleBGM()
        {
            AudioManager.I.ToggleBGM();
            RefreshAudioToggleColors();
        }

        public void ToggleSFX()
        {
            AudioManager.I.ToggleSFX();
            RefreshAudioToggleColors();
        }

        public void RefreshAudioToggleColors()
        {
            _bgmImage.color = AudioManager.I.IsBGMOn() ? _onColor : _offColor;
            _sfxImage.color = AudioManager.I.IsSFXOn() ? _onColor : _offColor;
        }
    }
}
