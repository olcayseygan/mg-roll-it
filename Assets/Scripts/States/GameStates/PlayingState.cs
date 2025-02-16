using Assets.Scripts.Patterns.StatePattern;
using UnityEngine;

namespace Assets.Scripts.States.GameStates
{
    public class PlayingState : State<Game>
    {
        public override StateTransition<Game> OnEnter(Game self)
        {
            self.isPaused = false;
            self.cube.highScore = Game.I.GetHighScore();
            self.inputList.Clear();
            GameUI.I.playingPanel.SetHighScoreText(self.cube.highScore);
            if (!self.cube.StateProvider.IsInState<CubeStates.WaitForActionState>())
            {
                self.cube.StateProvider.SwitchTo<CubeStates.IdleState>();
            }

            return base.OnEnter(self);
        }

        public override void OnExit(Game self)
        {
            base.OnExit(self);
            self.isPaused = true;
        }

        public override StateTransition<Game> Update(Game self)
        {
            if (Input.GetMouseButtonDown(0))
            {
                if (self.cube.StateProvider.IsInState<CubeStates.WaitForActionState>()) {
                    GameUI.I.StateProvider.SwitchTo<GameUIStates.PlayingState>();
                    self.cube.StateProvider.SwitchTo<CubeStates.IdleState>();

                    return base.Update(self);
                }

                AudioManager.I.PlaySFX(AudioManager.I.screenTapClip);
                self.inputList.Add(0);
            }

            PlatformManager.I.UpdatePlatforms();
            return base.Update(self);
        }
    }
}