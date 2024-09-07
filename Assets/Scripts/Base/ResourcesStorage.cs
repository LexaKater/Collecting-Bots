using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class ResourcesStorage : MonoBehaviour
{
    [SerializeField] private Scaner _scaner;

    private List<Resource> _findedResources;
    private List<Resource> _busyResources;

    public event Action ResourceAdded;

    private void Awake()
    {
        _findedResources = new List<Resource>();
        _busyResources = new List<Resource>();
    }

    private void OnEnable() => _scaner.ResourceFinded += AddResource;

    private void OnDisable() => _scaner.ResourceFinded -= AddResource;

    public bool TryGetResource(out Resource resource)
    {
        resource = null;

        if (_findedResources.Count > 0)
        {
            int randomIndex = Random.Range(0, _findedResources.Count);
            resource = _findedResources[randomIndex];

            _findedResources.Remove(resource);
            _busyResources.Add(resource);

            return true;
        }

        return false;
    }

    public void RemoveResource(Resource resource)
    {
        if (_busyResources.Contains(resource) == false)
            return;

        _busyResources.Remove(resource);
    }

    private void AddResource(Resource resource)
    {
        if (_busyResources.Contains(resource))
            return;

        if (_findedResources.Contains(resource))
            return;

        _findedResources.Add(resource);

        ResourceAdded?.Invoke();
    }
}