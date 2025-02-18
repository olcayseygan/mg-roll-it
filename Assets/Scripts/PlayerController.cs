using System.Collections;
using System.Collections.Generic;
using Assets.Scripts.Patterns.SingletonPattern;
using UnityEngine;

namespace Assets.Scripts
{
    public class PlayerController : SingletonProvider<PlayerController>
    {
        public void SetCoins(int coins) => PlayerPrefs.SetInt("PLAYER_COINS", coins);
        public int GetCoins() => PlayerPrefs.GetInt("PLAYER_COINS", 0);
        public void AddCoins(int amount) => SetCoins(GetCoins() + amount);
        public void RemoveCoins(int amount) => SetCoins(GetCoins() - amount);

        public void SetHighScore(int score) => PlayerPrefs.SetInt("PLAYER_HIGHSCORE", score);
        public void SetHighScoreIfHigher(int score)
        {
            if (score > GetHighScore())
            {
                SetHighScore(score);
            }
        }

        public int GetHighScore() => PlayerPrefs.GetInt("PLAYER_HIGHSCORE", 0);

        public void AddPlayedGames() => PlayerPrefs.SetInt("PLAYER_PLAYED_GAMES", GetPlayedGames() + 1);
        public int GetPlayedGames() => PlayerPrefs.GetInt("PLAYER_PLAYED_GAMES", 0);
    }
}
