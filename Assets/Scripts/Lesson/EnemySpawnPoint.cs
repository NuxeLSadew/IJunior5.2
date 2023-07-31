using UnityEngine;

public class EnemySpawnPoint : MonoBehaviour
{
    [SerializeField] private Enemy _enemyPrefab;
    [SerializeField] private Vector3 _spawnPositionOffset;

    public void Spawn()
    {
        Instantiate(_enemyPrefab, transform.position + _spawnPositionOffset, Quaternion.identity);
    }
}
