using UnityEngine;
using TMPro;
using Assets.Scripts.Patterns.StatePattern.Plugins;

namespace Assets.Scripts.StateViews
{
    public class PlayingPanel : StateViewPanel
    {
        [SerializeField] private TMP_Text _goldsText;
        [SerializeField] private TMP_Text _scoreText;

        public void SetGoldText(int golds, int currentGolds)
        {
            if (currentGolds > 0)
            {
                _goldsText.text = $"{golds}+{currentGolds}({golds + currentGolds})";
            }
            else
            {
                _goldsText.text = golds.ToString();
            }
        }

        public void SetScoreText(int score)
        {
            _scoreText.text = score.ToString();
        }
    }
}
