using System.Collections;
using System.Collections.Generic;
using Assets.Scripts.Patterns.SingletonPattern;
using UnityEngine;

namespace Assets.Scripts
{
    public class PlayingPanel : SingletonProvider<PlayingPanel>
    {
        [SerializeField] private GameObject _panel;

        [SerializeField] private TMPro.TMP_Text _scoreText;

        public void SetScore(int score)
        {
            _scoreText.text = score.ToString();
        }

        public void Show()
        {
            _panel.gameObject.SetActive(true);
        }

        public void Hide()
        {
            _panel.gameObject.SetActive(false);
        }
    }
}
