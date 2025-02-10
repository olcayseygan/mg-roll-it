using Assets.Scripts.Patterns.StatePattern;
using UnityEngine;

namespace Assets.Scripts.States.GameStates
{
    public class InitializationState : State<Game>
    {
        public override StateTransition<Game> OnEnter(Game self)
        {
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

            var player = Object.Instantiate(self.playerPrefab);
            Debug.Log("Initializing game");
            return self.StateProvider.FindState<PlayingState>();
        }
    }
}