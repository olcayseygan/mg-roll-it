using Assets.Scripts.Patterns.StatePattern.Plugins;

namespace Assets.Scripts.StateViews
{
    public class ShopPanel : StateViewPanel
    {
        public override void Show()
        {
            ShopManager.I.LoadCubeSkins();
            base.Show();
        }

        public void BackButton_Click()
        {
            Game.I.StateViewHandler.SwitchTo<MainMenuPanel>();
        }
    }
}
