using System;
using UnityEngine;

public class CollisionHandler : MonoBehaviour
{
    public event Action<Resources.Type, int> ResourceDelivered;

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out BotInventory inventory))
            ResourceDelivered?.Invoke(inventory.ResourceType,  inventory.ResourceCount);
    }
}