using System.Collections;
using System.Collections.Generic;
using Assets.Scripts.Patterns.FactoryPattern;
using Assets.Scripts.Patterns.SingletonPattern;
using UnityEngine;

namespace Assets.Scripts
{
    public class PlatformManager : SingletonProvider<PlatformManager>
    {
        private const float DISMANTLE_DISTANCE_THRESHOLD = 100.0f;
        public const float PLATFORM_SIZE = 3.0f;

        [SerializeField] private GameObject _prefab;
        private FactoryProvider<Platform> _platformFactory = new ();

        private Vector3 _lastSpawnPosition;

        public int size = 3;

        public int totalPlatforms = 40;

        public void UpdatePlatforms()
        {
            if (GetPlatforms().Count == 0) {
                return;
            }

            if (Cube.Instance == null)
            {
                return;
            }

            var distance = Vector3.Distance(Cube.Instance.transform.position, _lastSpawnPosition);
            if (distance < DISMANTLE_DISTANCE_THRESHOLD)
            {
                _platformFactory.Dismantle(_platformFactory.Instances[0]);
                SpawnPlatform();
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
            platform.transform.position = _lastSpawnPosition + (Random.Range(0, 2) == 0 ?
                new Vector3(0.0f, 0.0f, PLATFORM_SIZE) :
                new Vector3(PLATFORM_SIZE, 0.0f, 0.0f));
            platform.modelTransform.localScale = new Vector3(PLATFORM_SIZE, 20.0f, PLATFORM_SIZE);
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