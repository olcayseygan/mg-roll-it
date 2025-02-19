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

        public void BackButton_Click()
        {
            GameUI.I.StateProvider.SwitchTo<States.GameUIStates.MainMenuState>();
        }
    }
}
