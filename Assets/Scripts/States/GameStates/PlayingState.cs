using Assets.Scripts.Patterns.StatePattern;
using Assets.Scripts.StateViews;
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
            if (properties.isFreshRun)
            {
                self.speed = Game.MAX_SPEED;
                self.puppy.transform.position = Vector3.zero;
                self.AdjustCameraAndSpotlight();
                PlayerController.I.AddPlayedGames();
            }

            Cube.I.StateProvider.SwitchTo<CubeStates.WaitForActionState>();
            self.StateViewHandler.SwitchTo<WaitForActionPanel>();
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
                    Game.I.StateViewHandler.SwitchTo<PlayingPanel>();
                    Cube.I.StateProvider.SwitchTo<CubeStates.IdleState>();
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