using Assets.Scripts.Patterns.SingletonPattern;
using Assets.Scripts.Patterns.StatePattern;
using Assets.Scripts.Panels;

namespace Assets.Scripts
{
    public class GameUI : SingletonProvider<GameUI>
    {
        public StateProvider<GameUI> StateProvider;

        public GameOverPanel gameOverPanel;
        public InventoryPanel inventoryPanel;
        public MainMenuPanel mainMenuPanel;
        public PlayingPanel playingPanel;
        public SettingsPanel settingsPanel;
        public ShopPanel shopPanel;
        public WaitForActionPanel waitForActionPanel;
        public LoadingPanel loadingPanel;

        public TMPro.TMP_Text debugText;

        protected override void Awake()
        {
            base.Awake();
            StateProvider = new StateProvider<GameUI>(this);
            StateProvider.RegisterState(new States.GameUIStates.GameOverState());
            StateProvider.RegisterState(new States.GameUIStates.InventoryState());
            StateProvider.RegisterState(new States.GameUIStates.MainMenuState());
            StateProvider.RegisterState(new States.GameUIStates.PlayingState());
            StateProvider.RegisterState(new States.GameUIStates.SettingsState());
            StateProvider.RegisterState(new States.GameUIStates.ShopState());
            StateProvider.RegisterState(new States.GameUIStates.WaitForActionState());
            StateProvider.RegisterState(new States.GameUIStates.LoadingState());
        }
    }
}