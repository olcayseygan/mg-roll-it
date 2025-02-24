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
        [SerializeField] private RadioButtonGroup _maxFpsGroup;

        [SerializeField] private GameObject _muteButtonGameObject;
        [SerializeField] private GameObject _unmuteButtonGameObject;

        public override void Show()
        {
            _graphicsQualityGroup.SetSelectedIndex(PlayerController.I.GetQualityLevelIndex());
            _maxFpsGroup.SetSelectedIndex(PlayerController.I.GetMaxFPSIndex());
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
    }
}
