using System;
using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Bot))]
public class BotMover : MonoBehaviour
{
    [SerializeField, Range(0, 10f)] private float _distanceToTarget;
    [SerializeField] private float _speed;

    private Coroutine _coroutine;
    private int _currentWaypoint;

    public event Action RouteEnded;

    public void StartMove(Vector3[] route)
    {
        _currentWaypoint = 0;

        if (_coroutine != null)
            StopCoroutine(Move(route));

        StartCoroutine(Move(route));
    }

    private IEnumerator Move(Vector3[] route)
    {
        while (_currentWaypoint != route.Length)
        {
            MoveToPoint(route[_currentWaypoint]);

            yield return null;
        }

        RouteEnded?.Invoke();
    }

    private void MoveToPoint(Vector3 nextPosition)
    {
        transform.position = Vector3.MoveTowards(transform.position, nextPosition, _speed);
        transform.LookAt(nextPosition);

        if (transform.position.IsEnoughClose(nextPosition, _distanceToTarget))
            _currentWaypoint++;
    }
}