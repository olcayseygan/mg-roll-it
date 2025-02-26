using System.Collections;
using System.Collections.Generic;
using Assets.Scripts.Patterns.FactoryPattern;
using Assets.Scripts.Patterns.SingletonPattern;
using UnityEngine;

namespace Assets.Scripts
{
    public class PlatformManager : SingletonProvider<PlatformManager>
    {
        private const float DISMANTLE_DISTANCE_THRESHOLD = 10f;
        private const float SPAWN_DISTANCE_THRESHOLD = 30.0f;
        public const float PLATFORM_SIZE = 3.0f;

        [SerializeField] private GameObject _prefab;
        private FactoryProvider<Platform> _platformFactory = new();

        private Vector3 _lastSpawnPosition;

        private float h = 0f;

        private List<int> _last2Directions = new(); // 1 means forward, 0 means right

        public void UpdatePlatforms()
        {
            var platforms = GetPlatforms();
            if (platforms.Count == 0)
            {
                return;
            }

            if (Cube.I == null)
            {
                return;
            }

            var lastPlatform = platforms[platforms.Count - 1];
            var distance = Vector3.Distance(
                new Vector3(Cube.I.transform.position.x, 0.0f, Cube.I.transform.position.z),
                new Vector3(lastPlatform.transform.position.x, 0.0f, lastPlatform.transform.position.z));
            if (distance < SPAWN_DISTANCE_THRESHOLD)
            {
                SpawnPlatform();
            }

            var firstPlatform = platforms[0];
            distance = Vector3.Distance(
                new Vector3(Cube.I.transform.position.x, 0.0f, Cube.I.transform.position.z),
                new Vector3(firstPlatform.transform.position.x, 0.0f, firstPlatform.transform.position.z));
            if (distance > DISMANTLE_DISTANCE_THRESHOLD)
            {
                firstPlatform.StateProvider.SwitchTo<States.PlatformStates.DestroyState>();
                _platformFactory.Dismantle(firstPlatform, 0.1f);
            }
        }

        public void PickRandomColor()
        {
            h = Random.Range(0f, 360f);
        }

        public void SetColor()
        {
            Color mainColor = Color.HSVToRGB(h / 360f, 0.12f, 1f);
            foreach (Platform platform in GetPlatforms())
            {
                platform.meshRenderer.material.color = mainColor;
            }
        }

        public void SetColor(Platform platform)
        {
            Color mainColor = Color.HSVToRGB(h / 360f, 0.12f, 1f);
            platform.meshRenderer.material.color = mainColor;
        }

        public void SpawnPlatform(float x, float z)
        {
            var platform = _platformFactory.Create(_prefab);
            platform.transform.position = new Vector3(x, 0.0f, z);
            platform.modelTransform.localScale = new Vector3(PLATFORM_SIZE, 20.0f, PLATFORM_SIZE);
            SetColor(platform);
            _lastSpawnPosition = new Vector3(x, 0.0f, z);
            platform.StateProvider.SwitchTo<States.PlatformStates.IdleState>();
        }

        public void SpawnPlatform()
        {
            var platform = _platformFactory.Create(_prefab);
            var direction = Random.Range(0, 2);
            if (_last2Directions.Count == 3)
            {
                _last2Directions.RemoveAt(0);
            }

            if (_last2Directions.Count == 2)
            {
                var allSameDirection = _last2Directions.TrueForAll(d => d == direction);
                if (allSameDirection)
                {
                    direction = direction == 1 ? 0 : 1;
                }
            }

            _last2Directions.Add(direction);
            platform.transform.position = _lastSpawnPosition + (direction == 0 ?
                new Vector3(PLATFORM_SIZE, 0.0f, 0.0f) :
                new Vector3(0.0f, 0.0f, PLATFORM_SIZE)
            );
            platform.modelTransform.localScale = new Vector3(PLATFORM_SIZE, 20.0f, PLATFORM_SIZE);
            platform.TrySpawnGoldByChance();
            SetColor(platform);
            _lastSpawnPosition = platform.transform.position;
            platform.StateProvider.SwitchTo<States.PlatformStates.StartState>();
        }

        public List<Platform> GetPlatforms()
        {
            return _platformFactory.Instances;
        }

        public void ClearPlatforms()
        {
            List<Platform> list = GetPlatforms();
            for (int i = list.Count - 1; i >= 0; i--)
            {
                Platform platform = list[i];
                _platformFactory.Dismantle(platform);
            }
        }
    }
}