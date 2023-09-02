using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[RequireComponent(typeof(Pathway))]
public class PathCalculator : MonoBehaviour
{
    [SerializeField] List<Waypoint> _waypoints;

    private List<List<Pathway>> _temporalPathsToNextWaypoint;

    private void Start()
    {
        _temporalPathsToNextWaypoint = new List<List<Pathway>>();
    }

    public List<Pathway> Calculate()
    {
        List<Pathway> calculatedPath = new List<Pathway>();

        for (int i = 0; i < _waypoints.Count - 1; i++)
        {
            Waypoint waypointFrom = _waypoints[i];
            Waypoint waypointTo = _waypoints[i + 1];

            Pathway waypointFromPathwayComponent = waypointFrom.gameObject.GetComponent<Pathway>();
            waypointFromPathwayComponent.FindPathToWaypoint(waypointTo, _temporalPathsToNextWaypoint);

            List<List<Pathway>> orderedList = _temporalPathsToNextWaypoint.OrderBy(path => path.Count).ToList();
            List<Pathway> shortestPath = orderedList.First();

            calculatedPath.AddRange(shortestPath);

            _temporalPathsToNextWaypoint.Clear();
        }

        Pathway lastPathway = _waypoints.Last().gameObject.GetComponent<Pathway>();

        calculatedPath.Add(lastPathway);

        return calculatedPath;
    }
}
