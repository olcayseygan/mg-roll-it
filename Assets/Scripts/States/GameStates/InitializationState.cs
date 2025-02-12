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
            self.postProcessingVolume.profile.GetSetting<UnityEngine.Rendering.PostProcessing.DepthOfField>().active = true;
            var platformManager = PlatformManager.Instance;
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

            ColorController.Instance.SetColor();
            ColorController.Instance.UpdateColor();

            Object.Instantiate(self.playerPrefab);
            if (properties.isQuickPlayActive)
            {
                return self.StateProvider.FindState<PlayingState>();
            }

            return self.StateProvider.FindState<MainMenuState>();
        }
    }
}