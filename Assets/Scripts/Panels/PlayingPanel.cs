using System.Collections;
using System.Collections.Generic;
using Assets.Scripts.Patterns.SingletonPattern;
using UnityEngine;

namespace Assets.Scripts.Panels
{
    public class PlayingPanel : Panel
    {
        [SerializeField] private TMPro.TMP_Text _goldsText;
        [SerializeField] private TMPro.TMP_Text _scoreText;

        public void SetGoldText(int golds, int currentGolds)
        {
            if (currentGolds > 0) {
                _goldsText.text = $"{golds}+{currentGolds}({golds + currentGolds})";
            } else {
                _goldsText.text = golds.ToString();
            }
        }

        public void SetScoreText(int score)
        {
            _scoreText.text = score.ToString();
        }
    }
}
