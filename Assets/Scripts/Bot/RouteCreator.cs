using UnityEngine;

public class RouteCreator : MonoBehaviour
{
    private const int StartIndex = 0;
    private const int EndPointIndex = 1;
    private const int CountInitialPoints = 2;

    private Vector3[] _startPoints;

    private void Awake()
    {
        _startPoints = new Vector3[CountInitialPoints];
        AddInitialWaypoints();
    }

    public Vector3[] Create(params Vector3[] waypoints)
    {
        int countWaypoints = waypoints.Length + _startPoints.Length;
        Vector3[] newRoute = new Vector3[countWaypoints];

        newRoute[StartIndex] = _startPoints[StartIndex];
        newRoute[^1] = _startPoints[EndPointIndex];

        for (int i = 0; i < waypoints.Length; i++)
        {
            int indexForNewRoute = i + 1;
            newRoute[indexForNewRoute] = waypoints[i];
        }

        return newRoute;
    }

    private Vector3 CreateStartPoint()
    {
        float startPointZ = -20;
        Vector3 startPoint = transform.position;

        startPoint.z += startPointZ;

        return startPoint;
    }

    private void AddInitialWaypoints()
    {
        _startPoints[StartIndex] = CreateStartPoint();
        _startPoints[EndPointIndex] = transform.position;
    }
}