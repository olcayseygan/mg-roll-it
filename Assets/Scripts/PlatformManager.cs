using System.Collections;
using System.Collections.Generic;
using Assets.Scripts.Patterns.FactoryPattern;
using Assets.Scripts.Patterns.SingletonPattern;
using UnityEngine;

namespace Assets.Scripts
{
    public class PlatformManager : SingletonProvider<PlatformManager>
    {
        private const float DISMANTLE_DISTANCE_THRESHOLD = 10.0f;
        public const float PLATFORM_SIZE = 3.0f;

        [SerializeField] private GameObject _prefab;
        private FactoryProvider<Platform> _platformFactory = new();

        private Vector3 _lastSpawnPosition;

        [SerializeField] private float _speed = 5f;
        private float h = 0f;

        private List<int> _last2Directions = new(); // 1 means forward, 0 means right

        public void UpdatePlatforms()
        {
            if (GetPlatforms().Count == 0)
            {
                return;
            }

            if (Cube.I == null)
            {
                return;
            }

            var firstPlatform = GetPlatforms()[0];
            var distance = Vector3.Distance(Cube.I.transform.position, firstPlatform.transform.position);
            if (distance > DISMANTLE_DISTANCE_THRESHOLD)
            {
                firstPlatform.StateProvider.SwitchTo<States.PlatformStates.DestroyState>();
                _platformFactory.Dismantle(firstPlatform, 0.1f);
                SpawnPlatform();
            }
        }

        public void SetColor()
        {
            h = Random.Range(0f, 360f);
        }

        public void UpdateColors()
        {
            h += _speed * Time.deltaTime;
            if (h >= 360f)
            {
                h = 0f;
            }

            Color mainColor = Color.HSVToRGB(h / 360f, 0.12f, 1f);
            foreach (Platform platform in GetPlatforms())
            {
                platform.meshRenderer.material.color = mainColor;
            }
        }

        public void SpawnPlatform(float x, float z)
        {
            var platform = _platformFactory.Create(_prefab);
            platform.transform.position = new Vector3(x, 0.0f, z);
            platform.modelTransform.localScale = new Vector3(PLATFORM_SIZE, 20.0f, PLATFORM_SIZE);
            _lastSpawnPosition = new Vector3(x, 0.0f, z);
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
            platform.TrySpawnCoinByChance();
            _lastSpawnPosition = platform.transform.position;
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