using UnityEngine;

public class BaseInventory : MonoBehaviour
{
    [SerializeField] private ResourcesView _resourcesView;
    [SerializeField] private CollisionHandler _collisionHandler;

    private InventoryData _inventoryData;

    private void OnEnable() => _collisionHandler.ResourceDelivered += OnGet;

    private void OnDisable() => _collisionHandler.ResourceDelivered -= OnGet;

    private void OnGet(Resources.Type type, int count)
    {
        _inventoryData.TakeData(type, count);
        _resourcesView.ShowResourcesCount(type, _inventoryData.GetCount(type));
    }
}