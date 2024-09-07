using System;
using UnityEngine;

public class BaseInventory : MonoBehaviour
{
    [SerializeField] private CollectionPoint _collectionPoint;

    private int _resourceCount = 0;

    public event Action<int> ResourceCountChanged;

    private void Start() => ResourceCountChanged?.Invoke(_resourceCount);

    private void OnEnable() => _collectionPoint.ResourceDelivered += OnSetResourcesCount;

    private void OnDisable() => _collectionPoint.ResourceDelivered -= OnSetResourcesCount;

    public void SpendResources(int count)
    {
        _resourceCount -= count;
        ResourceCountChanged?.Invoke(_resourceCount);
    }

    private void OnSetResourcesCount(Resource resource)
    {
        _resourceCount++;
        ResourceCountChanged?.Invoke(_resourceCount);
    }
}