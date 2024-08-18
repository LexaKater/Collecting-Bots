using System;
using UnityEngine;

[RequireComponent(typeof(BotMover), typeof(ResourceTaker), typeof(RouteCreator))]
[RequireComponent(typeof(TargetTraсker))]
public class Bot : MonoBehaviour
{
    private BotMover _mover;
    private ResourceTaker _resourceTaker;
    private RouteCreator _routeCreator;
    private TargetTraсker _targetTraсker;

    public bool IsFree { get; private set; } = true;

    private void Awake()
    {
        _mover = GetComponent<BotMover>();
        _resourceTaker = GetComponent<ResourceTaker>();
        _routeCreator = GetComponent<RouteCreator>();
        _targetTraсker = GetComponent<TargetTraсker>();
    }

    private void OnEnable() => _mover.RouteEnded += OnRouteEnded;

    private void OnDisable() => _mover.RouteEnded -= OnRouteEnded;

    public void GoToCollect(Resource resource, Vector3 collectionPoint)
    {
        IsFree = false;

        _mover.StartMove(_routeCreator.Create(resource.transform.position, collectionPoint));
        _resourceTaker.SetResource(resource);
    }

    public void GoToBuild(Vector3 constructionPoint)
    {
        IsFree = false;

        _mover.StartMove(_routeCreator.Create(constructionPoint));
        _targetTraсker.StartTrackingPosition(constructionPoint);
    }

    private void OnRouteEnded() => IsFree = true;
}