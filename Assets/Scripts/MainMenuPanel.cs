using System.Collections;
using System.Collections.Generic;
using Assets.Scripts.Patterns.SingletonPattern;
using UnityEngine;
using TMPro;

namespace Assets.Scripts
{
    public class MainMenuPanel : SingletonProvider<MainMenuPanel>
    {
        [SerializeField] private GameObject _panel;

        public TMP_Text highScoreText;

        public void Show()
        {
            _panel.gameObject.SetActive(true);
        }

        public void Hide()
        {
            _panel.gameObject.SetActive(false);
        }

        public void Play()
        {
            Game.Instance.StateProvider.SwitchTo<States.GameStates.PlayingState>();
        }
    }
}
