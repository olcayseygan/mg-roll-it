using System.Collections;
using System.Collections.Generic;
using Assets.Scripts.Patterns.SingletonPattern;
using Assets.Scripts.Patterns.StatePattern;
using Assets.Scripts.Panels;
using UnityEngine;

namespace Assets.Scripts
{
    public class GameUI : SingletonProvider<GameUI>
    {
        public StateProvider<GameUI> StateProvider;

        public MainMenuPanel mainMenuPanel;
        public PlayingPanel playingPanel;
        public GameOverPanel gameOverPanel;

        protected override void Awake()
        {
            base.Awake();
            StateProvider = new StateProvider<GameUI>(this);
            StateProvider.RegisterState(new States.GameUIStates.MainMenuState());
            StateProvider.RegisterState(new States.GameUIStates.PlayingState());
            StateProvider.RegisterState(new States.GameUIStates.GameOverState());
            StateProvider.SwitchTo<States.GameUIStates.MainMenuState>();
        }
    }
}