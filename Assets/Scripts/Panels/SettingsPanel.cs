using System.Collections;
using System.Collections.Generic;
using Assets.Scripts.Patterns.SingletonPattern;
using Assets.Scripts.UI;
using UnityEngine;

namespace Assets.Scripts.Panels
{
    public class SettingsPanel : Panel
    {
        public RadioButtonGroup graphicsQualityGroup;
        public RadioButtonGroup maxFpsGroup;

        public GameObject muteButtonGameObject;
        public GameObject unmuteButtonGameObject;

        public void BackButton_Click()
        {
            GameUI.I.StateProvider.SwitchTo<States.GameUIStates.MainMenuState>();
        }

        public void MuteButton_Click()
        {
            AudioManager.I.MuteSFX();
            muteButtonGameObject.SetActive(false);
            unmuteButtonGameObject.SetActive(true);
        }

        public void UnmuteButton_Click()
        {
            AudioManager.I.UnmuteSFX();
            muteButtonGameObject.SetActive(true);
            unmuteButtonGameObject.SetActive(false);
        }
    }
}
