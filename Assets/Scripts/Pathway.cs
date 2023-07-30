using UnityEngine;

public class Pathway : MonoBehaviour
{
    public bool IsNextPathwayDefined { get; private set; }
    public Vector3 NextPathwayPosition { get; private set; }

    private PathChecker[] _pathCheckers;
    private Arrow _arrow;

    private void Awake()
    {
        _pathCheckers = GetComponentsInChildren<PathChecker>();
        _arrow = GetComponentInChildren<Arrow>();
    }

    private void FixedUpdate()
    {
        if (IsNextPathwayDefined)
        {
            return;
        }

        foreach (PathChecker pathChecker in _pathCheckers)
        {
            if (pathChecker.IsNextCellDefined)
            {
                IsNextPathwayDefined = true;
                _arrow.RotateY(pathChecker.RotationToNextCell);
                NextPathwayPosition = pathChecker.transform.position;
                break;
            }
        }
    }
}
