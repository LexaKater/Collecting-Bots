using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Bot))]
public class BotMover : MonoBehaviour
{
    private const int ResourceIndex = 2;

    [SerializeField] private float _speed;
    [SerializeField, Range(0f, 10f)] private float _delay;
    [SerializeField, Range(0, 10f)] private float _distanceToTarget;

    private Coroutine _coroutine;
    private List<Vector3> _waypoints;
    private Vector3 _startPoint;
    private int _currentWaypoint;
    private Bot _bot;

    public event Action<Resource> ResourceReached;

    private void Awake()
    {
        _bot = GetComponent<Bot>();
        _waypoints = new List<Vector3>();
        CreateStartPoint();
    }

    public void StartMove(Resource resource, Vector3 collectionPoint)
    {
        AddWaypoints(_startPoint, resource.transform.position, collectionPoint, transform.position);
        _currentWaypoint = 0;

        if (_coroutine != null)
            StopCoroutine(Move(resource));

        StartCoroutine(Move(resource));
    }

    private IEnumerator Move(Resource resource)
    {
        WaitForSeconds wait = new WaitForSeconds(_delay);

        while (_currentWaypoint != _waypoints.Count)
        {
            MoveToPoint(_waypoints[_currentWaypoint]);

            if (_currentWaypoint == ResourceIndex)
                ResourceReached?.Invoke(resource);

            yield return wait;
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

    private void CreateStartPoint()
    {
        float startPointY = 4;
        _startPoint = transform.position;

        _startPoint.y += startPointY;
    }

    private void AddWaypoints(Vector3 startPoint, Vector3 resourcePoint, Vector3 collectionPoint, Vector3 endPoint)
    {
        _waypoints.Clear();

        _waypoints.Add(startPoint);
        _waypoints.Add(resourcePoint);
        _waypoints.Add(collectionPoint);
        _waypoints.Add(endPoint);
    }
}