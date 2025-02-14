using Assets.Scripts.Patterns.StatePattern;
using UnityEngine;

namespace Assets.Scripts.States.GameStates
{
    public class InitializationStateProperties : StateProperties
    {
        public bool isQuickPlayActive;
    }

    public class InitializationState : State<Game, InitializationStateProperties>
    {
        public override StateTransition<Game> OnEnter(Game self, InitializationStateProperties properties)
        {
            self.isPaused = true;
            self.canContinue = true;
            var platformManager = PlatformManager.I;
            for (int x = 0; x < platformManager.size; x++)
            {
                for (int z = 0; z < platformManager.size; z++)
                {
                    platformManager.SpawnPlatform(x * PlatformManager.PLATFORM_SIZE, z * PlatformManager.PLATFORM_SIZE);
                }
            }

            for (int i = 0; i < platformManager.totalPlatforms; i++)
            {
                platformManager.SpawnPlatform();
            }

            ColorController.I.SetColor();
            ColorController.I.UpdateColor();

            self.cube = Object.Instantiate(self.cubePrefab).GetComponent<Cube>();
            if (properties.isQuickPlayActive)
            {
                self.AdjustCameraAndSpotlight();
                return self.StateProvider.FindState<PlayingState>();
            }

            return self.StateProvider.FindState<ShowcaseState>();
        }
    }
}