using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Pathway : MonoBehaviour
{
    [SerializeField] private List<PathwayChecker> _pathwayCheckers;
    private bool _isReached;

    public bool IsReached => _isReached;

    private void Start()
    {
        _isReached = false;
    }

    public void FindPathToWaypoint(Waypoint waypointEnd, List<List<Pathway>> temporalPathToNextWaypoint, List<Pathway> pathways = null)
    {
        if (pathways == null)
        {
            pathways = new List<Pathway>();
        }

        pathways.Add(this);

        List<Pathway> pathwaysNear = new List<Pathway>();

        foreach (PathwayChecker pathwayChecker in _pathwayCheckers)
        {
            if (pathwayChecker.CheckPathway())
            {
                if (pathwayChecker.FindedPathway.IsReached == false)
                {
                    pathwaysNear.Add(pathwayChecker.FindedPathway);
                }
            }
        }

        foreach (Pathway pathway in pathwaysNear)
        {
            if (pathway.TryGetComponent<Waypoint>(out Waypoint waypoint) && waypoint == waypointEnd)
            {
                temporalPathToNextWaypoint.Add(pathways);
                MakePathwaysUnreached(pathways, 1);
                return;
            }

            _isReached = true;
            pathway.FindPathToWaypoint(waypointEnd, temporalPathToNextWaypoint, pathways.ToList());
        }

        _isReached = false;
    }

    private void MakePathwaysUnreached(List<Pathway> pathways, int from)
    {
        int fromToIndexOffset = -1;
        int indexFrom = from + fromToIndexOffset;

        for (int i = indexFrom; i < pathways.Count; i++)
        {
            pathways[i]._isReached = false;
        }
    }
}
