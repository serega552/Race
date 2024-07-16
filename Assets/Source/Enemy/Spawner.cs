using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] private Camera _camera;
    [SerializeField] private float _zPosition = 14f;
    [SerializeField] private GameObject _enemy;
    [SerializeField] private GameObject _container;
    [SerializeField] private ScoreCounter _scoreCounter;
    [SerializeField] private Transform[] _spawnPosition;
    [SerializeField] private ParticleSystem _crashParticle;

    private List<ParticleSystem> _crashParticles = new List<ParticleSystem>();
    private Coroutine _startTimeSpawnCoroutine;
    private bool _canPlay = true;
    private CarMovement _movement;
    private List<EnemyMovement> _enemies = new List<EnemyMovement>();
    private WaitForSeconds _waitStartSpawn = new WaitForSeconds(1f);
    private WaitForSeconds _waitSpawnEnemies = new WaitForSeconds(2.5f);

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
        Vector3 positionInGlobalSpace = new Vector3(0, 0, 0);

        var screenEdges = new Vector3[]
        {
        new Vector3(-10f, -10f, _zPosition),
        new Vector3(10f, -10f, _zPosition),

        new Vector3(-10f, 10f, _zPosition),
        new Vector3(10f, 10f, _zPosition),

        new Vector3(10f, -10f, _zPosition),
        new Vector3(-10f, -10f, _zPosition),
        };

        int random = Random.Range(1, screenEdges.Length);

        positionInGlobalSpace = _camera.ViewportToWorldPoint(screenEdges[random]);
        positionInGlobalSpace = new Vector3(positionInGlobalSpace.x, 0, positionInGlobalSpace.z);

        if (CheckCollision(positionInGlobalSpace))
        {
            var enemy = Instantiate(_enemy);

            enemy.GetComponent<EnemyMovement>().GetCarMovement(_movement);
            enemy.transform.SetParent(_container.transform);
            enemy.transform.position = positionInGlobalSpace;
            enemy.name = screenEdges[random].ToString();
            EnemyMovement enemy1 = enemy.GetComponent<EnemyMovement>();
            _enemies.Add(enemy1);

            enemy1.OnCrash += SpawnCrashParticle;
            _scoreCounter.AddEnemies(_enemies);
        }
        else
        {
            int random2 = Random.Range(0, _spawnPosition.Length);
            positionInGlobalSpace = _spawnPosition[random2].position;
            positionInGlobalSpace = new Vector3(positionInGlobalSpace.x, 0, positionInGlobalSpace.z);

            var enemy = Instantiate(_enemy);

            enemy.GetComponent<EnemyMovement>().GetCarMovement(_movement);
            enemy.transform.SetParent(_container.transform);
            enemy.transform.position = positionInGlobalSpace;
            enemy.name = screenEdges[random].ToString();
            EnemyMovement enemy1 = enemy.GetComponent<EnemyMovement>();
            _enemies.Add(enemy1);

            enemy1.OnCrash += SpawnCrashParticle;
            _scoreCounter.AddEnemies(_enemies);
        }
    }

    public void DestroyEnemy()
    {
        for (int i = 0; i < _enemies.Count; i++)
        {
            _enemies[i].OnCrash -= SpawnCrashParticle;
            Destroy(_enemies[i].gameObject);
        }
        
        for (int i = 0; i < _crashParticles.Count; i++)
        {
            Destroy(_crashParticles[i].gameObject);
        }

        _crashParticles.Clear();
        _enemies.Clear();
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
        Collider[] hitColliders = Physics.OverlapSphere(position, 0.5f);
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
