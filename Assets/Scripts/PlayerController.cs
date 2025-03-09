using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Assets.Scripts.Patterns.SingletonPattern;
using UnityEngine;

namespace Assets.Scripts
{
    public class PlayerController : SingletonProvider<PlayerController>
    {
        private readonly float[] _initialSpeeds = new float[] { 0.175f, 0.125f, 0.075f };

        public string GetPlayerName() => PlayerPrefs.GetString("PLAYER_NAME", "Unknown Player");
        public void SetPlayerName(string name) => PlayerPrefs.SetString("PLAYER_NAME", name);

        public bool GetFirstTime() => PlayerPrefs.GetInt("PLAYER_FIRST_TIME", 1) == 1;
        public void SetFirstTime(bool isFirstTime) => PlayerPrefs.SetInt("PLAYER_FIRST_TIME", isFirstTime ? 1 : 0);

        public void SetGolds(int amount) => PlayerPrefs.SetInt("PLAYER_GOLDS", amount);
        public int GetGolds() => PlayerPrefs.GetInt("PLAYER_GOLDS", 0);
        public void AddGold(int amount) => SetGolds(GetGolds() + amount);
        public void RemoveGold(int amount) => SetGolds(GetGolds() - amount);

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
            if (ownedSkins.Contains(key)) return;
            ownedSkins.Add(key);
            PlayerPrefs.SetString("PLAYER_OWNED_CUBE_SKINS", string.Join(",", ownedSkins));
        }
        public void OwnCubeSkins(List<string> keys)
        {
            var ownedSkins = GetOwnedCubeSkinKeys();
            ownedSkins.AddRange(keys.Where(key => !ownedSkins.Contains(key)));
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

        public List<string> GetAllSkinKeys() => SkinManager.I.skinCollection.collection.Select(pair => pair.key).ToList();

        public void SetQualityLevel(int index)
        {
            PlayerPrefs.SetInt("PLAYER_QUALITY_LEVEL", index);
            QualitySettings.SetQualityLevel(index);
        }

        public int GetQualityLevelIndex() => PlayerPrefs.GetInt("PLAYER_QUALITY_LEVEL", 2);

        public void SetSFXToggle(bool isOn) => PlayerPrefs.SetInt("PLAYER_SFX_TOGGLE", isOn ? 1 : 0);
        public bool GetSFXToggle() => PlayerPrefs.GetInt("PLAYER_SFX_TOGGLE", 1) == 1;

        public void SetInitialSpeedIndex(int index) => PlayerPrefs.SetInt("PLAYER_INITIAL_SPEED", index);
        public int GetInitialSpeedIndex() => PlayerPrefs.GetInt("PLAYER_INITIAL_SPEED", 0);
        public float GetInitialSpeed() => _initialSpeeds[GetInitialSpeedIndex()];

    }
}
