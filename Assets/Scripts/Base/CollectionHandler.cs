using UnityEngine;

public class CollectionHandler : MonoBehaviour
{
    [SerializeField] private SorterFindedResources _resourceSorter;
    [SerializeField] private BotSorter _botSorter;
    [SerializeField] private CollectionPoint _collectionPoint;

    private void Update() => StartCollection();

    private void StartCollection()
    {
        Bot freeBot = _botSorter.GetRandomBot();

        if (freeBot == null)
            return;

        Resource resource = _resourceSorter.GetResource();

        if (resource == null)
            return;

        freeBot.Collect(resource, _collectionPoint.transform.position);
    }
}