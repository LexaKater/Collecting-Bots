using System.Collections;
using UnityEngine;

public class BotMover : MonoBehaviour
{
    private const int StartIndexPosition = 1;
    private const int ResourceIndexPosition = 2;
    private const int CollectionIndexPosition = 3;
    private const int EndIndexPosition = 4;

    [SerializeField] private Transform _startPoint;
    [SerializeField] private Transform _collectionPoint;
    [SerializeField] private Transform _endPoint;
    [SerializeField] private float _speed = 5;
    [SerializeField, Range(0f, 10f)] private float _delay = 0.5f;
    [SerializeField] private ResourcesTaker _taker;

    private Coroutine _coroutine;
    private int _currentIndexPosition = 1;
    private float _distanceTotarget = 0.5f;

    public bool IsFree { get; private set; } = true;

    public void StartMove(Resources resource)
    {
        if (_coroutine != null)
            StopCoroutine(Move(resource));

        StartCoroutine(Move(resource));
    }

    private IEnumerator Move(Resources resource)
    {
        WaitForSeconds wait = new WaitForSeconds(_delay);

        IsFree = false;

        _currentIndexPosition = 1;

        while (_currentIndexPosition != 5)
        {
            switch (_currentIndexPosition)
            {
                case StartIndexPosition:
                    MoveToPoint(_startPoint.position);

                    yield return wait;
                    break;

                case ResourceIndexPosition:
                    MoveToPoint(resource.transform.position);
                    _taker.TakeResource(resource);

                    yield return wait;
                    break;

                case CollectionIndexPosition:
                    MoveToPoint(_collectionPoint.position);

                    yield return wait;
                    break;

                case EndIndexPosition:
                    MoveToPoint(_endPoint.position);

                    yield return wait;
                    break;
            }

            if (transform.position.IsEnoughClose(_endPoint.position, _distanceTotarget))
                _currentIndexPosition = 5;
        }

        IsFree = true;
    }

    private void MoveToPoint(Vector3 nextPosition)
    {
        transform.position = Vector3.MoveTowards(transform.position, nextPosition, _speed);
        transform.LookAt(nextPosition);

        if (transform.position.IsEnoughClose(nextPosition, _distanceTotarget))
            _currentIndexPosition++;
    }
}