using UnityEngine;

public class RouteCreator : MonoBehaviour
{
    private const int StartPointIndex = 0;
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

        newRoute[StartPointIndex] = _startPoints[StartPointIndex];
        newRoute[^1] = _startPoints[EndPointIndex];

        for (int i = 0; i < waypoints.Length; i++)
        {
            int indexForNewRoute = i + 1;
            newRoute[indexForNewRoute] = waypoints[i];
        }

        return newRoute;
    }
    
    public void SetEndPoint(Vector3 endPoint) => _startPoints[EndPointIndex] = endPoint;

    public void SetStartPoint(Vector3 startPoint) => _startPoints[StartPointIndex] = startPoint;

    public Vector3 CreateStartPoint(Vector3 startPosition)
    {
        Vector3 startPoint = startPosition;
        float startPointZ = -20;

        startPoint.z += startPointZ;

        return startPoint;
    }
    
    public Vector3 CreateEndPoint(Vector3 endPosition)
    {
        Vector3 endPoint = endPosition;
        float endPointX = +20;

        endPoint.x += endPointX;

        return endPoint;
    }

    private void AddInitialWaypoints()
    {
        _startPoints[StartPointIndex] = CreateStartPoint(transform.position);
        _startPoints[EndPointIndex] = transform.position;
    }
}