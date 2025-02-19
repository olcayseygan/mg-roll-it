using Assets.Scripts.Patterns.StatePattern;
using UnityEngine;

namespace Assets.Scripts.States.GameStates
{
    public class PlayingStateProperties : StateProperties
    {
        public bool isFreshRun;
    }

    public class PlayingState : State<Game, PlayingStateProperties>
    {
        public override StateTransition<Game> OnEnter(Game self, PlayingStateProperties properties)
        {
            self.inputList.Clear();
            GameUI.I.StateProvider.SwitchTo<GameUIStates.WaitForActionState>();
            GameUI.I.playingPanel.SetCoinsText(PlayerController.I.GetCoins(), self.GetCurrentRunCoins());
            Cube.I.StateProvider.SwitchTo<CubeStates.WaitForActionState>();
            if (properties.isFreshRun)
            {
                PlayerController.I.AddPlayedGames();
            }

            return base.OnEnter(self);
        }

        public override StateTransition<Game> Update(Game self)
        {
            if (Cube.I.StateProvider.IsInState<CubeStates.FellOffState>())
            {
                return self.StateProvider.FindState<GameOverState>();
            }

            if (Input.GetMouseButtonDown(0))
            {
                if (Cube.I.StateProvider.IsInState<CubeStates.WaitForActionState>())
                {
                    GameUI.I.StateProvider.SwitchTo<GameUIStates.PlayingState>();
                    Cube.I.StateProvider.SwitchTo<CubeStates.IdleState>();
                    return base.Update(self);
                }

                AudioManager.I.PlaySFX(AudioManager.I.screenTapClip);
                self.inputList.Add(0);
            }

            PlatformManager.I.UpdatePlatforms();
            PlatformManager.I.UpdateColors();
            return base.Update(self);
        }
    }
}