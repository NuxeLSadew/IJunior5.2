using System.Collections;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private float _delay;
    [SerializeField] private int _enemiesCount;

    private EnemySpawnPoint[] _spawners;
    private int _spawnerIndex;
    private EnemySpawnPoint _choisedEnemySpawner;
    private Coroutine _spawnProcedureCoroutine;

    private void Start()
    {
        _spawners = GetComponentsInChildren<EnemySpawnPoint>();
        InitializeSpawnPointsPaths();
        _choisedEnemySpawner = _spawners[_spawnerIndex];
        _spawnProcedureCoroutine = StartCoroutine(RunSpawnProcedure());
    }

    private IEnumerator RunSpawnProcedure()
    {
        WaitForSeconds waitForSeconds = new WaitForSeconds(_delay);

        for (int i = 0; i < _enemiesCount; i++)
        {
            yield return waitForSeconds;

            SpawnEnemyFromChoisedSpawner();
            ChoiseNextSpawner();
        }
    }

    private void ChoiseNextSpawner()
    {
        _spawnerIndex++;

        if (_spawnerIndex >= _spawners.Length)
        {
            _spawnerIndex = 0;
        }

        _choisedEnemySpawner = _spawners[_spawnerIndex];
    }

    private void SpawnEnemyFromChoisedSpawner()
    {
        _choisedEnemySpawner.Spawn();
    }

    private void InitializeSpawnPointsPaths()
    {
        foreach (EnemySpawnPoint enemySpawnPoint in _spawners)
        {
            enemySpawnPoint.CalculatePath();
        }
    }
}
