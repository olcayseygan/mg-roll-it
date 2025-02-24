using Assets.Scripts.Patterns.StatePattern.Plugins;
using TMPro;
using UnityEngine;
using UnityEngine.Localization;
using UnityEngine.Localization.Components;

namespace Assets.Scripts.StateViews
{
    public class GameOverPanel : StateViewPanel
    {
        [SerializeField] private TMP_Text _goldsText;
        [SerializeField] private TMP_Text _scoreText;
        [SerializeField] private TMP_Text _highScoreText;
        [SerializeField] private GameObject _watchAdButtonGameObject;
        [SerializeField] private GameObject _continueButtonGameObject;
        [SerializeField] private GameObject _doubleGoldButtonGameObject;
        [SerializeField] private TMP_Text _doubleGoldButtonText;

        public override void Show()
        {
            _goldsText.text = PlayerController.I.GetGolds().ToString();
            _scoreText.text = Game.I.GetCurrentRunScore().ToString();
            _highScoreText.text = PlayerController.I.GetHighScore().ToString();
            var stringEvent = _doubleGoldButtonText.GetComponent<LocalizeStringEvent>();
            stringEvent.StringReference = new LocalizedString { TableReference = "Table", TableEntryReference = "UI_GAME_OVER_DOUBLE_GOLD_BUTTON_TEXT", Arguments = new[] { Game.I.GetCurrentRunGolds().ToString() } };
            base.Show();
        }

        public void ShowContinueButton()
        {
            _continueButtonGameObject.SetActive(true);
        }

        public void HideContinueButton()
        {
            _continueButtonGameObject.SetActive(false);
        }

        public void ShowDoubleGoldButton()
        {
            _doubleGoldButtonGameObject.SetActive(true);
        }

        public void HideDoubleGoldButton()
        {
            _doubleGoldButtonGameObject.SetActive(false);
        }

        public void ContinueButton_Click()
        {
            Game.I.StateProvider.SwitchTo<States.GameStates.ContinueState>();
        }

        public void DoubleGoldButton_Click()
        {
            PlayerController.I.AddGold(Game.I.GetCurrentRunGolds());
            _goldsText.text = PlayerController.I.GetGolds().ToString();
            HideDoubleGoldButton();
        }


        public void RetryButton_Click()
        {
            Game.I.StateProvider.SwitchTo<States.GameStates.CleaningState>(new States.GameStates.CleaningStateProperties() { canSkipToPlaying = true });
        }

        public void MainMenu_Click()
        {
            Game.I.StateProvider.SwitchTo<States.GameStates.CleaningState>(new States.GameStates.CleaningStateProperties() { canSkipToPlaying = false });
        }
    }
}
