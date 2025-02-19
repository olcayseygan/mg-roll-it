using System.Collections;
using System.Collections.Generic;
using Assets.Scripts.Patterns.SingletonPattern;
using UnityEngine;

namespace Assets.Scripts.Panels
{
    public class SettingsPanel : Panel
    {
        public void BackButton_Click()
        {
            GameUI.I.StateProvider.SwitchTo<States.GameUIStates.MainMenuState>();
        }
    }
}
