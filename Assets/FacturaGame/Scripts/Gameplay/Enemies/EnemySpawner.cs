using System.Collections;
using FacturaGame.Scripts.Gameplay.Services.StoperService;
using FacturaGame.Scripts.Utils;
using UnityEngine;
using Zenject;
using Random = UnityEngine.Random;

namespace FacturaGame.Scripts.Gameplay.Enemies
{
    public class EnemySpawner : MonoBehaviour, IStopable
    {
        [SerializeField] private Transform _roadStartPoint;
        [SerializeField] private Transform _roadEndPoint;
        [SerializeField] private float _roadWidth;
        [SerializeField] private float _spawnDistance;
        [SerializeField] private AnimationCurve _spawnDensity;
        [SerializeField] private float _densityMultiplier = 15f;
        [SerializeField] private Transform _spawnContainer;
        
        private EnemyFactory _enemyFactory;
        private Transform _spawnTarget;
        private float _lastSpawnPosition;
        private Coroutine _spawnCoroutine;

        [Inject]
        public void Construct(EnemyFactory enemyFactory, SystemsStopService systemsStopService)
        {
            _enemyFactory = enemyFactory;
            systemsStopService.AddStopable(this);
        }
        
        public void SetSpawnTarget(Transform spawnTarget)
        {
            _spawnTarget = spawnTarget;
        }

        public void RunSpawn()
        {
            _spawnCoroutine = CoroutineRunner.RunCoroutine(SpawnCoroutine());
        }

        public void StopSpawn()
        {
            CoroutineRunner.CancelCoroutine(_spawnCoroutine);
        }
        
        private IEnumerator SpawnCoroutine()
        {
            _lastSpawnPosition = _roadStartPoint.position.z + _spawnDistance;
            SpawnEnemiesAlongRoad(new Vector2(_roadStartPoint.position.z, _lastSpawnPosition));

            while (true)
            {
                float targetPositionZ = _spawnTarget.position.z;
                
                if (targetPositionZ + _spawnDistance >= _lastSpawnPosition)
                {
                    float spanwIntervalEnd = _lastSpawnPosition + _spawnDistance >= _roadEndPoint.position.z
                        ? _roadEndPoint.position.z
                        : _lastSpawnPosition + _spawnDistance;
                    
                    Vector2 spawnInterval = new Vector2(_lastSpawnPosition, spanwIntervalEnd);
                    SpawnEnemiesAlongRoad(spawnInterval);
                    
                    _lastSpawnPosition = spanwIntervalEnd;

                    if (_lastSpawnPosition >= _roadEndPoint.position.z)
                    {
                        break;
                    }
                }
                
                yield return null;
            }
        }

        public void OnStop()
        {
            StopSpawn();
            _lastSpawnPosition = 0;
        }

        private void SpawnEnemiesAlongRoad(Vector2 spawnInterval)
        {
            float roadLenght = (_roadEndPoint.position.z - _roadStartPoint.position.z);
            float density = _spawnDensity.Evaluate((1 / roadLenght) * _lastSpawnPosition);
            int enemiesToSpawn = Mathf.CeilToInt(density * _densityMultiplier);

            for (int i = 0; i < enemiesToSpawn; i++)
            {
                float spawnX = Random.Range(-_roadWidth / 2f, _roadWidth / 2f); 
                float spawnZ = Random.Range(spawnInterval.x, spawnInterval.y); 
                Vector3 spawnPosition = new Vector3(spawnX, 0, spawnZ);
                
                _enemyFactory.
                    CreateEnemy(spawnPosition, _spawnContainer).
                    EnemyBehaviour.
                    InitialiseBehavior().
                    StartBehavior();
            }
        }
        
#if UNITY_EDITOR
        private void OnDrawGizmos()
        {
            Gizmos.color = Color.blue;
            Gizmos.DrawLine(_roadStartPoint.position + (Vector3.left * (_roadWidth / 2)), _roadEndPoint.position + (Vector3.left * (_roadWidth / 2)));
            Gizmos.DrawLine(_roadStartPoint.position + (Vector3.right * (_roadWidth / 2)), _roadEndPoint.position + (Vector3.right * (_roadWidth / 2)));
            Gizmos.DrawLine(_roadStartPoint.position + (Vector3.left * (_roadWidth / 2)), _roadStartPoint.position + (Vector3.right * (_roadWidth / 2)));
            Gizmos.DrawLine(_roadEndPoint.position + (Vector3.left * (_roadWidth / 2)), _roadEndPoint.position + (Vector3.right * (_roadWidth / 2)));
        }
#endif
    }
}