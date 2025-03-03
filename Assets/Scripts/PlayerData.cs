using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts
{
    [System.Serializable]
    public class PlayerData
    {
        public int playerGolds;
        public int playerHighScore;
        public int playerPlayedGames;
        public string playerEquippedCubeSkinKey;
        public List<string> playerOwnedCubeSkinKeys;
        public int playerQualityLevel;
        public int playerSFXToggle;
        public int playerInitialSpeed;

        public static string Serialize()
        {
            var playerData = new PlayerData() {
                playerGolds = PlayerController.I.GetGolds(),
                playerHighScore = PlayerController.I.GetHighScore(),
                playerPlayedGames = PlayerController.I.GetPlayedGames(),
                playerEquippedCubeSkinKey = PlayerController.I.GetEquippedCubeSkinKey(),
                playerOwnedCubeSkinKeys = PlayerController.I.GetOwnedCubeSkinKeys(),
                playerQualityLevel = PlayerController.I.GetQualityLevelIndex(),
                playerSFXToggle = PlayerController.I.GetSFXToggle() ? 1 : 0,
                playerInitialSpeed = PlayerController.I.GetInitialSpeedIndex()
            };
            return JsonUtility.ToJson(playerData);
        }

        public static PlayerData Deserialize(string json)
        {
            return JsonUtility.FromJson<PlayerData>(json);
        }
    }
}