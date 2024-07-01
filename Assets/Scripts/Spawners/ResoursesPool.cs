using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class ResoursesPool : MonoBehaviour
{
    [SerializeField] private Resources[] _resources;

    private Queue<Resources> _resourcesPool;

    private void Awake() => _resourcesPool = new Queue<Resources>();

    public Resources GetResource()
    {
        if (_resourcesPool.Count == 0)
        {
            Resources newResours = Instantiate(GetRandomResources());

            return newResours;
        }

        return _resourcesPool.Dequeue();
    }

    public void OnPutResource(Resources resource)
    {
        resource.gameObject.SetActive(false);
        _resourcesPool.Enqueue(resource);

        resource.Released -= OnPutResource;
    }

    private Resources GetRandomResources() => _resources[Random.Range(0, _resources.Length)];
}