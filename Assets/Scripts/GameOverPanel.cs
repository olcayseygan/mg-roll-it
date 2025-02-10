using System.Collections;
using System.Collections.Generic;
using Assets.Scripts.Patterns.SingletonPattern;
using TMPro;
using UnityEngine;

namespace Assets.Scripts
{
    public class GameOverPanel : SingletonProvider<GameOverPanel>
    {
        [SerializeField] private GameObject _panel;
        [SerializeField] private TMP_Text _scoreText;
        [SerializeField] private TMP_Text _highScoreText;

        public void Show(int score, int highScore)
        {
            _highScoreText.text = highScore.ToString();
            _scoreText.text = score.ToString();
            _panel.gameObject.SetActive(true);
        }

        public void Hide()
        {
            _panel.gameObject.SetActive(false);
        }
    }
}
