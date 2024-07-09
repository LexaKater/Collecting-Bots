using UnityEngine;

[RequireComponent(typeof(BotMover))]
public class Bot : MonoBehaviour
{
    private BotMover _mover;

    public bool IsFree { get; private set; } = true;

    private void Awake() => _mover = GetComponent<BotMover>();

    public void Collect(Resource resource, Vector3 collectionPoint)
    {
        IsFree = false;

        _mover.StartMove(resource, collectionPoint);
    }

    public void SetFreeStatus() => IsFree = true;
}