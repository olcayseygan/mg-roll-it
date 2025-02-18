using System.Collections;
using System.Collections.Generic;
using System.Linq;
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

        public string GetEquippedCubeSkinKey() => PlayerPrefs.GetString("PLAYER_EQUIPPED_CUBE_SKIN", "0");
        public void SetEquippedCubeSkinKey(string key) => PlayerPrefs.SetString("PLAYER_EQUIPPED_CUBE_SKIN", key);

        public List<string> GetOwnedCubeSkinKeys() => PlayerPrefs.GetString("PLAYER_OWNED_CUBE_SKINS", "0").Split(',').ToList();
        public void OwnCubeSkin(string key)
        {
            var ownedSkins = GetOwnedCubeSkinKeys();
            ownedSkins.Add(key);
            PlayerPrefs.SetString("PLAYER_OWNED_CUBE_SKINS", string.Join(",", ownedSkins));
        }

        public void OwnAllCubeSkins()
        {

            var ownedSkins = new List<string>();
            foreach (var pair in SkinManager.I.skinCollection.collection)
            {
                ownedSkins.Add(pair.key);
            }
            PlayerPrefs.SetString("PLAYER_OWNED_CUBE_SKINS", string.Join(",", ownedSkins));
        }
    }
}
