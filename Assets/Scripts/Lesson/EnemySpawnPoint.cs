using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PathCalculator))]
public class EnemySpawnPoint : MonoBehaviour
{
    [SerializeField] private Enemy _enemyTemplate;
    [SerializeField] private Vector3 _spawnPositionOffset;

    PathCalculator _pathCalculator;
    List<Pathway> _path;

    private void Awake()
    {
        _pathCalculator = GetComponent<PathCalculator>();
    }

    public void Spawn()
    {
        Enemy enemy = Instantiate(_enemyTemplate, transform.position + _spawnPositionOffset, Quaternion.identity);
        Movement enemyMovement = enemy.gameObject.GetComponent<Movement>();
        enemyMovement.SetPath(_path);
    }

    public void CalculatePath()
    {
        _path = _pathCalculator.Calculate();
    }
}
