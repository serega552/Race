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

    private Coroutine _startTimeSpawnCoroutine;
    private bool _canPlay = true;
    private CarMovement _movement;
    private List<EnemyMovement> _enemies = new List<EnemyMovement>();
    private WaitForSeconds _waitStartSpawn = new WaitForSeconds(4f);
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
        new Vector3(-10f, -10f, _zPosition),      // left bottom
        new Vector3(10f, -10f, _zPosition),   // middle bottom

        new Vector3(-10f, 10f, _zPosition),      // left top
        new Vector3(10f, 10f, _zPosition),   // middle top
        };

        int random = Random.Range(1, screenEdges.Length);

        positionInGlobalSpace = _camera.ViewportToWorldPoint(screenEdges[random]);
        positionInGlobalSpace = new Vector3(positionInGlobalSpace.x, 0, positionInGlobalSpace.z);
        var enemy = Instantiate(_enemy);

        enemy.GetComponent<EnemyMovement>().GetCarMovement(_movement);
        enemy.transform.SetParent(_container.transform);
        enemy.transform.position = positionInGlobalSpace;
        enemy.name = screenEdges[random].ToString();
        _enemies.Add(enemy.GetComponent<EnemyMovement>());

        _scoreCounter.AddEnemies(_enemies);
    }

    public void DestroyEnemy()
    {
        for(int i = 0; i < _enemies.Count; i++)
        {
            Destroy(_enemies[i]!.gameObject);
        }

        _enemies.Clear();
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
