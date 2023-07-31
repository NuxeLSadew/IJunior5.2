using UnityEngine;

public class EnemySpawnSystem : MonoBehaviour
{
    [SerializeField] private float _timeToSpawnEnemy;

    private float _timer;
    private EnemySpawnPoint[] _spawners;
    private int _spawnerIndex;
    private EnemySpawnPoint ChoisedEnemySpawner;

    private void Awake()
    {
        _spawners = GetComponentsInChildren<EnemySpawnPoint>();
        ChoisedEnemySpawner = _spawners[_spawnerIndex];
    }

    private void Update()
    {
        _timer += Time.deltaTime;

        if (_timer >= _timeToSpawnEnemy)
        {
            SpawnEnemyFromChoisedSpawner();
            ChoiseNextSpawner();
            _timer = 0;
        }
    }

    private void ChoiseNextSpawner()
    {
        _spawnerIndex++;

        if (_spawnerIndex >= _spawners.Length)
        {
            _spawnerIndex = 0;
        }

        ChoisedEnemySpawner = _spawners[_spawnerIndex];
    }

    private void SpawnEnemyFromChoisedSpawner()
    {
        ChoisedEnemySpawner.Spawn();
    }
}
