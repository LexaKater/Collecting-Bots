using UnityEngine;

[RequireComponent(typeof(BotMover), typeof(ResourceTaker))]
public class Bot : MonoBehaviour
{
    private BotMover _mover;
    private ResourceTaker _taker;

    public bool IsFree { get; private set; } = true;

    private void Awake()
    {
        _mover = GetComponent<BotMover>();
        _taker = GetComponent<ResourceTaker>();
    }

    public void GoToCollect(Resource resource, Vector3 collectionPoint)
    {
        IsFree = false;

        _mover.StartMove(resource, collectionPoint);
        _taker.SetResource(resource);
    }

    public void SetFreeStatus() => IsFree = true;
}