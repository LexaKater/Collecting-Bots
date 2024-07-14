using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Bot))]
public class BotMover : MonoBehaviour
{
    private const int ResourceIndex = 1;
    private const int CollectionIndex = 2;

    [SerializeField] private float _speed;
    [SerializeField, Range(0, 10f)] private float _distanceToTarget;

    private Coroutine _coroutine;
    private List<Vector3> _waypoints;
    private int _currentWaypoint;
    private Bot _bot;

    private void Awake()
    {
        _bot = GetComponent<Bot>();
        _waypoints = new List<Vector3>();
        AddInitialWaypoints();
    }

    public void StartMove(Resource resource, Vector3 collectionPoint)
    {
        AddWaypoints(resource.transform.position, collectionPoint);
        _currentWaypoint = 0;

        if (_coroutine != null)
            StopCoroutine(Move());

        StartCoroutine(Move());
    }

    private IEnumerator Move()
    {
        while (_currentWaypoint != _waypoints.Count)
        {
            MoveToPoint(_waypoints[_currentWaypoint]);

            yield return null;
        }

        _bot.SetFreeStatus();
    }

    private void MoveToPoint(Vector3 nextPosition)
    {
        transform.position = Vector3.MoveTowards(transform.position, nextPosition, _speed);
        transform.LookAt(nextPosition);

        if (transform.position.IsEnoughClose(nextPosition, _distanceToTarget))
            _currentWaypoint++;
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
        _waypoints.Add(CreateStartPoint());
        _waypoints.Add(transform.position);
    }

    private void AddWaypoints(Vector3 resourcePoint, Vector3 collectionPoint)
    {
        int countStartPoint = 2;
        
        if (_waypoints.Count > countStartPoint)
        {
            _waypoints.RemoveAt(CollectionIndex);
            _waypoints.RemoveAt(ResourceIndex);
        }

        _waypoints.Insert(ResourceIndex, resourcePoint);
        _waypoints.Insert(CollectionIndex, collectionPoint);
    }
}