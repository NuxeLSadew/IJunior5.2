using System.Collections;
using UnityEngine;

public class Movement : MonoBehaviour
{
    const int SpeedNeedsForMoveOnNextCell = 6;

    /// <summary>
    /// Скорость движения. Указывается в целых числах, перед каждым передвижением высчитывается шанс
    /// для перехода на следующую клетку. Шанс равен speed/6.
    /// Например, если скорость 4/6, то в 4-х случаях из 6-ти 
    /// передвигаемый объект передвинется на следующую клетку. 
    /// В случаях, если скорость больше 6, объект гарантированно двинется на следующую клетку, 
    /// затем будет расчитываться шанс на передвижение на следующую клетку.
    /// </summary>
    [SerializeField] private int _speed;
    [SerializeField] private float _timeToMove;
    [SerializeField] private bool _isLoggingMoveSuccess;

    private float _timer;
    private WayChecker _wayChecker;
    private int _minSpeed = 0;
    private int _maxSpeed = 18;
    private float _portionOfTimeForMovementAnimation = 0.1f;
    private float _timeForMovementAnimation;
    private Coroutine _movementCoroutine;
    private Coroutine _movementToNextCellCoroutine;

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
        _timeForMovementAnimation = _timeToMove * _portionOfTimeForMovementAnimation;
        _wayChecker = GetComponentInChildren<WayChecker>();
    }

    private void Update()
    {
        _timer += Time.deltaTime;

        if (_timer >= _timeToMove)
        {
            _timer = 0;
            _movementCoroutine = StartCoroutine(Move());
        }
    }

    private IEnumerator Move()
    {
        int cellsToMoveCount = CalculateCellsCountToMove(_speed);

        for (int i = 0; i < cellsToMoveCount; i++)
        {
            _movementToNextCellCoroutine = StartCoroutine(MoveOnNextCell());

            yield return new WaitForSeconds(_timeForMovementAnimation);
        }
    }

    private IEnumerator MoveOnNextCell()
    {
        if (_wayChecker.TryGetNextCellPosition(out Vector3 nextPosition))
        {
            Vector3 startPosition = transform.position;
            nextPosition = CalculateNextPositionToMove(nextPosition);
            
            float animationTimer = 0;

            while (animationTimer < 1)
            {
                animationTimer += Time.deltaTime / _timeForMovementAnimation;
                transform.position = Vector3.Lerp(startPosition, nextPosition, animationTimer);

                yield return null;
            }
        }
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
}
