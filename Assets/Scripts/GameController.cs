using System.Collections;
using System.Collections.Generic;
using Assets.Scripts.Patterns.SingletonPattern;
using UnityEngine;
using TMPro;
using Assets.Scripts.States.GameStates;

namespace Assets.Scripts
{

    public class GameController : SingletonProvider<GameController>
    {
        public float speed = 1.0f;
        public int score = 0;

        public int GetHighScore()
        {
            return PlayerPrefs.GetInt("HighScore", 0);
        }

        public void SetHighScore(int value)
        {
            PlayerPrefs.SetInt("HighScore", value);
        }

        public void RestartGame()
        {
            Game.Instance.StateProvider.SwitchTo<States.GameStates.RestartState>(new RestartStateProperties() { isQuickPlayActive = true });
        }

        public void ContinueGame()
        {
            Game.Instance.StateProvider.SwitchTo<States.GameStates.ContinueState>();
        }

        public void MainMenu()
        {
            Game.Instance.StateProvider.SwitchTo<States.GameStates.RestartState>(new RestartStateProperties() { isQuickPlayActive = false });
        }
    }
}
