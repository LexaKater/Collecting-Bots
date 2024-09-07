using UnityEngine;
using System;

[RequireComponent(typeof(BotMover), typeof(TargetTraсker), typeof(RouteCreator))]
public class Bot : MonoBehaviour
{
    [SerializeField] private float _distanceToResource;

    private TargetTraсker _targetTraсker;
    private RouteCreator _routeCreator;
    private Resource _currentResource;
    private BotMover _mover;

    public event Action<Bot> TargetReached;

    public bool IsFree { get; private set; } = true;

    private void Awake()
    {
        _mover = GetComponent<BotMover>();
        _routeCreator = GetComponent<RouteCreator>();
        _targetTraсker = GetComponent<TargetTraсker>();
    }

    private void OnEnable()
    {
        _targetTraсker.TargetReached += OnTargetReached;
        _mover.RouteEnded += OnRouteEnded;
    }

    private void OnDisable()
    {
        _targetTraсker.TargetReached -= OnTargetReached;
        _mover.RouteEnded -= OnRouteEnded;
    }

    public void GoToCollect(Resource resource, Vector3 collectionPoint)
    {
        IsFree = false;
        _currentResource = resource;

        _mover.StartMove(_routeCreator.Create(resource.transform.position, collectionPoint));
        _targetTraсker.StartTrackingPosition(resource.transform.position);
    }

    public void GoToBuild(Vector3 constructionPoint)
    {
        IsFree = false;

        _routeCreator.SetEndPoint(_routeCreator.CreateEndPoint(constructionPoint));
        _mover.StartMove(_routeCreator.Create(constructionPoint));
        _routeCreator.SetStartPoint(_routeCreator.CreateStartPoint(constructionPoint));
        _targetTraсker.StartTrackingPosition(constructionPoint);
    }

    private void OnTargetReached()
    {
        TargetReached?.Invoke(this);

        if (TryTakeResource(_currentResource) == false)
            return;

        _currentResource = null;
    }

    private bool TryTakeResource(Resource resource)
    {
        if (resource == null)
            return false;

        if (transform.position.IsEnoughClose(resource.transform.position, _distanceToResource))
        {
            resource.transform.SetParent(transform);
            resource.transform.localPosition = Vector3.forward * _distanceToResource;

            return true;
        }

        return false;
    }

    private void OnRouteEnded() => IsFree = true;
}