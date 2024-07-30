using System;
using UnityEngine;

public class BaseInventory : MonoBehaviour
{
    [SerializeField] private CollectionPoint _collectionPoint;

    private int _resourceCount = 0;

    public event Action<int> ResourceCountChanged;

    private void OnEnable() => _collectionPoint.ResourceDelivered += OnSetResourceCount;

    private void OnDisable() => _collectionPoint.ResourceDelivered -= OnSetResourceCount;

    public void SpendResources(int count)
    {
        _resourceCount -= count;
        ResourceCountChanged?.Invoke(_resourceCount);
    }

    private void OnSetResourceCount(Resource resource)
    {
        _resourceCount++;
        ResourceCountChanged?.Invoke(_resourceCount);
    }
}