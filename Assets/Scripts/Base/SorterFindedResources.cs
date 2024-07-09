using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class SorterFindedResources : MonoBehaviour
{
    [SerializeField] private Scaner _scaner;
    [SerializeField] private CollectionPoint _collectionPoint;

    private List<Resource> _findedResources;
    private List<Resource> _busyResources;

    private void Awake()
    {
        _findedResources = new List<Resource>();
        _busyResources = new List<Resource>();
    }

    private void OnEnable()
    {
        _scaner.ResourceFinded += AddResource;
        _collectionPoint.ResourceDelivered += RemoveResource;
    }

    private void OnDisable()
    {
        _scaner.ResourceFinded -= AddResource;
        _collectionPoint.ResourceDelivered -= RemoveResource;
    }

    public Resource GetResource()
    {
        if (_findedResources.Count > 0)
        {
            int randomIndex = Random.Range(0, _findedResources.Count);

            Resource resource = _findedResources[randomIndex];

            if (TryFindResource(resource, _busyResources))
                return null;

            _busyResources.Add(resource);

            return resource;
        }

        return null;
    }

    private void RemoveResource(Resource resource)
    {
        if (!TryFindResource(resource, _findedResources))
            return;

        if (!TryFindResource(resource, _busyResources))
            return;

        _findedResources.Remove(resource);
        _busyResources.Remove(resource);
    }

    private void AddResource(Resource resource)
    {
        if (TryFindResource(resource, _findedResources))
            return;

        _findedResources.Add(resource);
    }

    private bool TryFindResource(Resource resource, List<Resource> _resources) => _resources.Contains(resource);
}