using System.Collections;
using UnityEngine;

public class EnemySpawnSystem : MonoBehaviour
{
    [SerializeField] private float _delay;
    [SerializeField] private int _enemiesCount;

    private EnemySpawnPoint[] _spawners;
    private int _spawnerIndex;
    private EnemySpawnPoint _choisedEnemySpawner;
    private Coroutine _spawnProcedureCoroutine;

    private void Awake()
    {
        _spawners = GetComponentsInChildren<EnemySpawnPoint>();
        _choisedEnemySpawner = _spawners[_spawnerIndex];
    }

    private void Start()
    {
        _spawnProcedureCoroutine = StartCoroutine(RunSpawnProcedure());
    }

    private IEnumerator RunSpawnProcedure()
    {
        while (_enemiesCount > 0)
        {
            SpawnEnemyFromChoisedSpawner();
            ChoiseNextSpawner();
            _enemiesCount--;

            yield return new WaitForSeconds(_delay);
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
}
