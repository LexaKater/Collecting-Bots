using System;
using UnityEngine;

public class CollectionPoint : MonoBehaviour
{
    public event Action<Resource> ResourceDelivered;

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out Resource resource))
        {
            ResourceDelivered?.Invoke(resource);
            resource.Release();
        }
    }
}