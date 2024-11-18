using Blocks;
using Cars;
using Score;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Enemy
{
    public class Spawner : MonoBehaviour
    {
        private readonly List<EnemyMovement> _enemies = new List<EnemyMovement>();
        private readonly List<ParticleSystem> _crashParticles = new List<ParticleSystem>();
        private readonly WaitForSeconds _waitStartSpawn = new WaitForSeconds(4f);
        private readonly WaitForSeconds _waitSpawnEnemies = new WaitForSeconds(2.5f);

        [SerializeField] private Camera _camera;
        [SerializeField] private GameObject _enemy;
        [SerializeField] private GameObject _container;
        [SerializeField] private ScoreCounter _scoreCounter;
        [SerializeField] private Transform[] _spawnPosition;
        [SerializeField] private ParticleSystem _crashParticle;
        [SerializeField] private EnemiesFactory.EnemiesFactory _enemyFactory;

        private int _maxEnemies = 10;
        private float _minPosition = -10f;
        private float _maxPosition = 10f;
        private float _zPosition = 14f;
        private float _radius = 0.5f;
        private Coroutine _startTimeSpawnCoroutine;
        private bool _canPlay = true;
        private CarMovement _movement;

        public void Init(CarMovement movement)
        {
            _movement = movement;
        }

        public void StartGame()
        {
            _canPlay = true;
            _startTimeSpawnCoroutine = StartCoroutine(TimeSpawn());
        }

        public void ResetEnemy()
        {
            DestroyEnemy();
        }

        public void EndGame()
        {
            _canPlay = false;
            StopCoroutine(_startTimeSpawnCoroutine);
            DestroyEnemy();
        }

        public void SpawnCar()
        {
            Vector3 positionInGlobalSpace = Vector3.zero;

            var screenEdges = new Vector3[]
            {
        new Vector3(-_minPosition, -_minPosition, _zPosition),
        new Vector3(_minPosition, -_minPosition, _zPosition),

        new Vector3(-_minPosition, _maxPosition, _zPosition),
        new Vector3(_maxPosition, _maxPosition, _zPosition),

        new Vector3(_maxPosition, -_minPosition, _zPosition),
        new Vector3(-_minPosition, -_minPosition, _zPosition),
            };

            int random = UnityEngine.Random.Range(1, screenEdges.Length);

            positionInGlobalSpace = _camera.ViewportToWorldPoint(screenEdges[random]);
            positionInGlobalSpace = new Vector3(positionInGlobalSpace.x, 0, positionInGlobalSpace.z);

            if (CheckCollision(positionInGlobalSpace))
            {
                Spawn(positionInGlobalSpace, screenEdges, random);
            }
            else
            {
                int random2 = UnityEngine.Random.Range(0, _spawnPosition.Length);
                positionInGlobalSpace = _spawnPosition[random2].position;
                positionInGlobalSpace = new Vector3(positionInGlobalSpace.x, 0, positionInGlobalSpace.z);

                Spawn(positionInGlobalSpace, screenEdges, random);
            }

            if (_enemies.Count > _maxEnemies)
                DestroyEnemyInGame();
        }

        private void Spawn(Vector3 positionInGlobalSpace, Vector3[] screenEdges, int random)
        {
            var enemy = _enemyFactory.CreateEnemy();

            enemy.GetComponent<EnemyMovement>().ChangeCarMovement(_movement);
            enemy.transform.SetParent(_container.transform);
            enemy.transform.position = positionInGlobalSpace;
            enemy.name = screenEdges[random].ToString();
            EnemyMovement enemy1 = enemy.GetComponent<EnemyMovement>();
            _enemies.Add(enemy1);

            enemy1.Crashing += SpawnCrashParticle;
            _scoreCounter.AddEnemies(_enemies);
        }

        public void DestroyEnemy()
        {
            for (int i = 0; i < _enemies.Count; i++)
            {
                _enemies[i].Crashing -= SpawnCrashParticle;
                Destroy(_enemies[i].gameObject);
            }

            for (int i = 0; i < _crashParticles.Count; i++)
            {
                Destroy(_crashParticles[i].gameObject);
            }

            _crashParticles.Clear();
            _enemies.Clear();
        }

        private void DestroyEnemyInGame()
        {
            for (int i = 0; i < _enemies.Count; i++)
            {
                if (_enemies[i].gameObject.activeSelf == false)
                {
                    _enemies[i].Crashing -= SpawnCrashParticle;
                    Destroy(_enemies[i].gameObject);
                    _enemies.Remove(_enemies[i]);
                }
            }
        }

        private void SpawnCrashParticle(Transform transform)
        {
            var particle = Instantiate(_crashParticle, transform);
            particle.transform.SetParent(_container.transform);
            particle.Play();
            _crashParticles.Add(particle);
        }

        private bool CheckCollision(Vector3 position)
        {
            Collider[] hitColliders = Physics.OverlapSphere(position, _radius);
            foreach (var hitCollider in hitColliders)
            {
                if (hitCollider.gameObject.GetComponent<Block>())
                {
                    return true;
                }

                if (hitCollider.gameObject.GetComponent<WaterBlock>())
                {
                    return false;
                }
            }

            return false;
        }

        private IEnumerator TimeSpawn()
        {
            yield return _waitStartSpawn;

            while (_canPlay)
            {
                SpawnCar();
                yield return _waitSpawnEnemies;
            }

            yield return null;
        }
    }
}