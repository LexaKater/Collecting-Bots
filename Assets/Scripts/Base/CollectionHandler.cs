using UnityEngine;

public class CollectionHandler : MonoBehaviour
{
    [SerializeField] private SorterFindedResources _resourceSorter;
    [SerializeField] private BotSorter _botSorter;
    [SerializeField] private CollectionPoint _collectionPoint;

    private void Update() => StartCollection();

    private void StartCollection()
    {
        Bot freeBot = _botSorter.GetFreeBot();

        if (freeBot == null)
            return;

        Resource resource = _resourceSorter.GetResource();

        if (resource == null)
            return;

        freeBot.GoToCollect(resource, _collectionPoint.transform.position);
    }
}