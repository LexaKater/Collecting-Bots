using System;
using UnityEngine;

public class BaseInventory : MonoBehaviour
{
    [SerializeField] private CollectionPoint _collectionPoint;

    private int _resourceCount;

    public event Action<int> ResourceCountChanged;

    private void OnEnable() => _collectionPoint.ResourceDelivered += OnSetResourceCount;

    private void OnDisable() => _collectionPoint.ResourceDelivered -= OnSetResourceCount;

    private void OnSetResourceCount(Resource resource)
    {
        _resourceCount++;
        ResourceCountChanged?.Invoke(_resourceCount);
    }
}