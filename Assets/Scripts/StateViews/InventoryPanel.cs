using System.Collections;
using Assets.Scripts.Patterns.StatePattern.Plugins;

namespace Assets.Scripts.StateViews
{
    public class InventoryPanel : StateViewPanel
    {
        public override void Show()
        {
            InventoryManager.I.LoadOwnedCubeSkins();
            base.Show();
        }

        public override void Hide()
        {
            InventoryManager.I.ClearList();
            Game.I.StartCoroutine(ChangeCubeSkin());
        }

        private IEnumerator ChangeCubeSkin()
        {
            Game.I.DestoryCube();
            yield return null;
            Game.I.CreateCube();
            base.Hide();
        }

        public void BackButton_Click()
        {
            Game.I.StateViewHandler.SwitchTo<MainMenuPanel>();
        }
    }
}
