using System.Collections.Generic;
using UnityEngine;

public class ResourcePool : MonoBehaviour
{
    [SerializeField] private Resource _resource;

    private Queue<Resource> _resourcesPool;

    private void Awake() => _resourcesPool = new Queue<Resource>();

    public Resource GetResource()
    {
        if (_resourcesPool.Count == 0)
            return Instantiate(_resource);

        return _resourcesPool.Dequeue();
    }

    public void PutResource(Resource resource)
    {
        resource.gameObject.SetActive(false);
        _resourcesPool.Enqueue(resource);
    }
}