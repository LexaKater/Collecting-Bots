using UnityEngine;
using Zenject;

public class CollectionHandler : MonoBehaviour
{
    [SerializeField] private CollectionPoint _collectionPoint;
    [SerializeField] private BotStorage _botStorage;

    private ResourcesStorage _resourcesStorage;

    [Inject]
    private void Construct(ResourcesStorage resourcesStorage) => _resourcesStorage = resourcesStorage;

    private void OnEnable()
    {
        _resourcesStorage.ResourceAdded += OnResourceAdded;
        _collectionPoint.ResourceDelivered += OnDelivered;
    }

    private void OnDisable()
    {
        _resourcesStorage.ResourceAdded -= OnResourceAdded;
        _collectionPoint.ResourceDelivered -= OnDelivered;
    }

    private void OnDelivered(Resource resource) => _resourcesStorage.RemoveResource(resource);

    private void OnResourceAdded() => TrySendToCollect();

    private void TrySendToCollect()
    {
        if (_botStorage.TryGetFreeBot(out Bot freeBot) == false)
            return;

        if (_resourcesStorage.TryGetResource(out Resource resource) == false)
            return;

        freeBot.GoToCollect(resource, _collectionPoint.transform.position);
    }
}