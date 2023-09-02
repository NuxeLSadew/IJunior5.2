using System.Collections;
using UnityEngine;
using DG.Tweening;
using DG.Tweening.Core;
using DG.Tweening.Plugins.Options;
using System.Collections.Generic;

public class Movement : MonoBehaviour
{
    private const int SpeedNeedsForMoveOnNextCell = 6;
    private const float TimeToMove = 2;

    /// <summary>
    /// Скорость движения. Указывается в целых числах, перед каждым передвижением высчитывается шанс
    /// для перехода на следующую клетку. Шанс равен speed/6.
    /// Например, если скорость 4/6, то в 4-х случаях из 6-ти 
    /// передвигаемый объект передвинется на следующую клетку. 
    /// В случаях, если скорость больше 6, объект гарантированно двинется на следующую клетку, 
    /// затем будет расчитываться шанс на передвижение на следующую клетку.
    /// </summary>
    [SerializeField] private int _speed;
    [SerializeField] private bool _isLoggingMoveSuccess;

    private float _timer;
    private int _minSpeed = 0;
    private int _maxSpeed = 18;
    private float _portionOfTimeForMovementAnimation = 0.1f;
    private float _timeForMovementAnimation;
    private Coroutine _movementCoroutine;
    private TweenerCore<Vector3, Vector3, VectorOptions> _movementTweener;
    private List<Pathway> _path;
    private int _currentPathwayIndex;

    private void OnValidate()
    {
        if (_speed < _minSpeed)
        {
            _speed = _minSpeed;
        }
        if (_speed > _maxSpeed)
        {
            _speed = _maxSpeed;
        }
    }

    private void Start()
    {
        _timeForMovementAnimation = TimeToMove * _portionOfTimeForMovementAnimation;
    }

    private void Update()
    {
        _timer += Time.deltaTime;

        if (_timer >= TimeToMove)
        {
            _timer = 0;
            _movementCoroutine = StartCoroutine(Move());
        }
    }

    public void SetPath(List<Pathway> path)
    {
        if (_path == null)
        {
            _path = path;
        }
        else
        {
            throw new System.Exception("Нельзя поменять путь");
        }
    }

    private IEnumerator Move()
    {
        int cellsToMoveCount = CalculateCellsCountToMove(_speed);
        WaitForSeconds wait = new WaitForSeconds(_timeForMovementAnimation);

        for (int i = 0; i < cellsToMoveCount; i++)
        {
            MoveOnNextCell();

            yield return wait;
        }
    }

    private void MoveOnNextCell()
    {
        Vector3 nextPosition = _path[_currentPathwayIndex + 1].transform.position;
        _currentPathwayIndex++;

        nextPosition = CalculateNextPositionToMove(nextPosition);
        
        _movementTweener = transform.DOMove(nextPosition, _timeForMovementAnimation);
    }

    private int CalculateCellsCountToMove(int speed)
    {
        int minRandomValue = 1;
        int count = speed / SpeedNeedsForMoveOnNextCell;
        speed %= SpeedNeedsForMoveOnNextCell;

        int randomedValue = Random.Range(minRandomValue, SpeedNeedsForMoveOnNextCell + 1);
        bool isMoveAdditionalCellSuccess = speed >= randomedValue;

        if (speed > 0 && isMoveAdditionalCellSuccess)
        {
            count++;
        }

        if (_isLoggingMoveSuccess)
        {
            string successResultTranslated = isMoveAdditionalCellSuccess ? "Успех" : "Неудача";

            Debug.Log($"({name}): " +
                $"Шанс передвинуться на одну клетку дальше: ({speed}/{SpeedNeedsForMoveOnNextCell}) - " +
                $"{successResultTranslated}");
        }

        return count;
    }

    private Vector3 CalculateNextPositionToMove(Vector3 position)
    {
        float offsetY = 0.375f;
        position.y += offsetY;

        return position;
    }

    private void OnDestroy()
    {
        _movementTweener.Kill();
        StopCoroutine(_movementCoroutine);
    }
}
