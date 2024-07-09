using UnityEngine;

[RequireComponent(typeof(BotMover))]
public class ResourceTaker : MonoBehaviour
{
    [SerializeField] private float _distanceToTarget = 0.1f;

    private BotMover _mover;

    private void Awake() => _mover = GetComponent<BotMover>();

    private void OnEnable() => _mover.ResourceReached += TakeResource;

    private void OnDisable() => _mover.ResourceReached -= TakeResource;

    private void TakeResource(Resource resource)
    {
        resource.transform.SetParent(transform);
        resource.transform.localPosition = Vector3.forward * _distanceToTarget;
    }
}