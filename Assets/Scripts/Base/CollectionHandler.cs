using UnityEngine;

public class CollectionHandler : MonoBehaviour
{
    [SerializeField] private ResourcesSorter resourceResourcesSorter;
    [SerializeField] private BotSorter _botSorter;
    [SerializeField] private CollectionPoint _collectionPoint;

    public void TrySendToCollect()
    {
        if (_botSorter.TryGetFreeBot(out Bot freeBot) == false)
            return;

        if (resourceResourcesSorter.TryGetResource(out Resource resource) == false)
            return;

        freeBot.GoToCollect(resource, _collectionPoint.transform.position);
    }
}