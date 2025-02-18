using Assets.Scripts.Patterns.SingletonPattern;
using Assets.Scripts.Patterns.StatePattern;
using Assets.Scripts.Panels;

namespace Assets.Scripts
{
    public class GameUI : SingletonProvider<GameUI>
    {
        public StateProvider<GameUI> StateProvider;

        public MainMenuPanel mainMenuPanel;
        public PlayingPanel playingPanel;
        public GameOverPanel gameOverPanel;
        public WaitForActionPanel waitForActionPanel;
        public InventoryPanel inventoryPanel;
        public ShopPanel shopPanel;

        protected override void Awake()
        {
            base.Awake();
            StateProvider = new StateProvider<GameUI>(this);
            StateProvider.RegisterState(new States.GameUIStates.MainMenuState());
            StateProvider.RegisterState(new States.GameUIStates.PlayingState());
            StateProvider.RegisterState(new States.GameUIStates.GameOverState());
            StateProvider.RegisterState(new States.GameUIStates.WaitForActionState());
            StateProvider.RegisterState(new States.GameUIStates.InventoryState());
            StateProvider.RegisterState(new States.GameUIStates.ShopState());
            StateProvider.SwitchTo<States.GameUIStates.MainMenuState>();
        }

        public void NavigateToMainMenu()
        {
            StateProvider.SwitchTo<States.GameUIStates.MainMenuState>();
        }

        public void NavigateToInventory()
        {
            StateProvider.SwitchTo<States.GameUIStates.InventoryState>();
        }

        public void NavigateToShop()
        {
            StateProvider.SwitchTo<States.GameUIStates.ShopState>();
        }
    }
}