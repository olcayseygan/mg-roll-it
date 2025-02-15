using Assets.Scripts.Patterns.StatePattern;
using UnityEngine;

namespace Assets.Scripts.States.GameStates
{
    public class PlayingState : State<Game>
    {
        public override StateTransition<Game> OnEnter(Game self)
        {
            self.isPaused = false;
            AudioManager.I.PlaySFX(AudioManager.I.gamePlayingClip);
            // RewardedAdsManager.I.LoadAd();
            self.cube.highScore = Game.I.GetHighScore();
            GameUI.I.playingPanel.SetHighScoreText(self.cube.highScore);
            self.cube.StateProvider.SwitchTo<CubeStates.IdleState>();
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
                Cube.I.ChangeDirection();
            }

            PlatformManager.I.UpdatePlatforms();
            return base.Update(self);
        }
    }
}