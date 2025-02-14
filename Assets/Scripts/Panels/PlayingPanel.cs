using System.Collections;
using System.Collections.Generic;
using Assets.Scripts.Patterns.SingletonPattern;
using UnityEngine;

namespace Assets.Scripts.Panels
{
    public class PlayingPanel : Panel
    {
        [SerializeField] private TMPro.TMP_Text _scoreText;
        [SerializeField] private TMPro.TMP_Text _highScoreText;

        public void SetScoreText(int score)
        {
            _scoreText.text = score.ToString();
        }

        public void SetHighScoreText(int highScore)
        {
            _highScoreText.text = highScore.ToString();
        }
    }
}
