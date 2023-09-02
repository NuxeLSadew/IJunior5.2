using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class TargetFinder : MonoBehaviour
{
    [SerializeField] private TargetChecker _checkerTemplate;

    private List<TargetChecker> _checkers;
    private List<ITargetable> _targetables;
    private float _range;
    private GameObject _checkersParent;

    private void Start()
    {
        string checkersParentName = "CheckersParent";

        _checkers = new List<TargetChecker>();
        _checkersParent = Instantiate(new GameObject(checkersParentName), transform);
        SetRange(4);

    }

    public void SetRange(float range)
    {
        _range = range;
    }

    public ITargetable FindTarget(TargetType targetType, TargetPriority targetPriority)
    {
        throw new NotImplementedException();
    }

    [ContextMenu(nameof(CreateCheckers))]
    private void CreateCheckers()
    {
        int integeredRange = (int)_range;
        int negativeIntegeredRange = -integeredRange;
        float offsetY = -0.05f;

        for (int x = negativeIntegeredRange; x <= integeredRange; x++)
        {
            for (int z = negativeIntegeredRange; z <= integeredRange; z++)
            {
                if (CheckCoordsIsInRange(x, z))
                {
                    TargetChecker checker = Instantiate(
                            _checkerTemplate,
                            new Vector3(x, transform.position.y + offsetY, z),
                            new Quaternion(),
                            _checkersParent.transform
                            );
                    _checkers.Add(checker);
                }
            }
        }
    }

    [ContextMenu(nameof(ClearCheckers))]
    private void ClearCheckers()
    {
        foreach (TargetChecker checker in _checkers)
        {
            Destroy(checker.gameObject);
        }
    }

    private bool CheckCoordsIsInRange(int firstCoord, int secondCoord)
    {
        return Mathf.Abs(firstCoord) + Mathf.Abs(secondCoord) <= _range;
    }

    private void CollectTargetables()
    {
        foreach (TargetChecker checker in _checkers)
        {
            _targetables.AddRange(checker.Targetables);
        }
    }
}
