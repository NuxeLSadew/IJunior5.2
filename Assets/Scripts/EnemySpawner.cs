using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private GameObject _enemyPrefab;
    [SerializeField] private Vector3 _spawnPositionOffset;

    public void Spawn()
    {
        Instantiate(_enemyPrefab, transform.position + _spawnPositionOffset, Quaternion.identity);
    }
}
