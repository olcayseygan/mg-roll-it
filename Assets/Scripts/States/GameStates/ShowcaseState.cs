using Assets.Scripts.Patterns.StatePattern;
using Assets.Scripts.StateViews;
using UnityEngine;

namespace Assets.Scripts.States.GameStates
{
    public class ShowcaseState : State<Game>
    {
        public override StateTransition<Game> OnEnter(Game self)
        {
            self.cameraTargetOffset = new Vector3(-5.5f, 5f, -5f);
            self.cameraTargetRotation = Quaternion.Euler(30f, 45f, 0f);
            self.cameraTargetOrthographicSize = 5f;
            self.AdjustCameraAndSpotlight();
            self.puppy.transform.position = new Vector3(0f, 0f, 0f);
            self.StateViewHandler.SwitchTo<MainMenuPanel>();
            return base.OnEnter(self);
        }

        public override void OnExit(Game self)
        {
            base.OnExit(self);
            self.cameraTargetOffset = new Vector3(-5f, 15f, -5f);
            self.cameraTargetRotation = Quaternion.Euler(45f, 45f, 0f);
            self.cameraTargetOrthographicSize = 15f;
            self.camera.transform.position = self.cameraTargetOffset;
            Game.I.StateViewHandler.SwitchTo<PlayingPanel>();
        }
    }
}