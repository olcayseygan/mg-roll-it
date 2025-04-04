using Assets.Scripts.Patterns.StatePattern;
using Assets.Scripts.StateViews;
using UnityEngine;

namespace Assets.Scripts.States.GameStates
{
    public class InstantiationStateProperties : StateProperties
    {
        public bool canSkipToPlaying;
    }

    public class InstantiationState : State<Game, InstantiationStateProperties>
    {
        private const int PLATFORM_SIZE = 5;
        private const int PLATFORM_LENGTH = 15;

        public override StateTransition<Game> OnEnter(Game self, InstantiationStateProperties properties)
        {
            PlatformManager.I.PickRandomColor();
            InstantiatePlatforms();
            InstantiateCube(self);
            Game.I.StateViewHandler.Get<PlayingPanel>().SetScoreText(0);
            Game.I.StateViewHandler.Get<PlayingPanel>().SetGoldText(PlayerController.I.GetGolds(), 0);
            self.isContinuationEnabled = true;
            self.hasUsedDoubleGold = false;
            self.ResetCurrentRun();
            if (properties.canSkipToPlaying)
            {
                return self.StateProvider.FindState<PlayingState>(new PlayingStateProperties() { isFreshRun = true });
            }

            return self.StateProvider.FindState<ShowcaseState>();
        }

        public void InstantiatePlatforms()
        {
            var platformManager = PlatformManager.I;
            var startX = 0f;
            var startZ = 0f;
            for (int i = PLATFORM_SIZE - 1; i >= 1; i--)
            {
                for (int x = 0; x < i; x++)
                {
                    var deltaX = startX + x * PlatformManager.PLATFORM_SIZE;
                    for (int z = 0; z < i; z++)
                    {
                        var deltaZ = startZ + z * PlatformManager.PLATFORM_SIZE;
                        if (platformManager.GetPlatforms().Find(platform => platform.transform.position.x == deltaX && platform.transform.position.z == deltaZ) != null)
                            continue;
                        platformManager.SpawnPlatform(deltaX, deltaZ);
                    }
                }
                startX += (i - 1) * PlatformManager.PLATFORM_SIZE;
                startZ += (i - 1) * PlatformManager.PLATFORM_SIZE;
            }

            for (int i = 0; i < PLATFORM_LENGTH; i++)
            {
                platformManager.SpawnPlatform();
            }
        }

        public void InstantiateCube(Game self)
        {
            self.CreateCube();
        }
    }
}