using Assets.Scripts.Patterns.SingletonPattern;
using Assets.Scripts.Patterns.StatePattern;
using UnityEngine;

namespace Assets.Scripts
{
    public class GameUI : SingletonProvider<GameUI>
    {
        public StateProvider<GameUI> StateProvider;

        public GameObject debugPanel;
        public TMPro.TMP_Text debugText;

        public StateViews.MainMenuPanel mainMenuPanel;
        public StateViews.SettingsPanel settingsPanel;
        public StateViews.PlayingPanel playingPanel;
        public StateViews.WaitForActionPanel waitForActionPanel;
        public StateViews.LoadingPanel loadingPanel;
        public StateViews.InventoryPanel inventoryPanel;
        public StateViews.ShopPanel shopPanel;
        public StateViews.GameOverPanel gameOverPanel;
    }
}