using System.Collections;
using System.Collections.Generic;
using Assets.Scripts.Patterns.SingletonPattern;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using Assets.Scripts.Patterns.StatePattern.Plugins;
using Assets.Scripts.UI;

namespace Assets.Scripts.StateViews
{
    public class SettingsPanel : StateViewPanel
    {
        [SerializeField] private RadioButtonGroup _graphicsQualityGroup;
        [SerializeField] private RadioButtonGroup _initialSpeedGroup;

        [SerializeField] private GameObject _muteButtonGameObject;
        [SerializeField] private GameObject _unmuteButtonGameObject;

        [SerializeField] private Button _couponCodeButton;
        [SerializeField] private TMP_InputField _couponCodeInputField;

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                BackButton_Click();
            }
        }

        public override void Show()
        {
            _graphicsQualityGroup.SetSelectedIndex(PlayerController.I.GetQualityLevelIndex());
            _initialSpeedGroup.SetSelectedIndex(PlayerController.I.GetInitialSpeedIndex());
            _muteButtonGameObject.SetActive(PlayerController.I.GetSFXToggle());
            _unmuteButtonGameObject.SetActive(!PlayerController.I.GetSFXToggle());
            base.Show();
        }

        public void BackButton_Click()
        {
            Game.I.StateViewHandler.SwitchTo<MainMenuPanel>();
        }

        public void MuteButton_Click()
        {
            AudioManager.I.MuteSFX();
            _muteButtonGameObject.SetActive(false);
            _unmuteButtonGameObject.SetActive(true);
        }

        public void UnmuteButton_Click()
        {
            AudioManager.I.UnmuteSFX();
            _muteButtonGameObject.SetActive(true);
            _unmuteButtonGameObject.SetActive(false);
        }


        public void CouponCodeButton_Click()
        {
            switch (_couponCodeInputField.text)
            {
                case "unlock-all-cube-skins":
                    Debug.Log("Unlocking all cube skins");
                    PlayerController.I.OwnAllCubeSkins();
                    break;
                default:
                    break;
            }

            _couponCodeInputField.text = "";
        }
    }
}
