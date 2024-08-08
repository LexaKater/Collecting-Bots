using UnityEngine;

[RequireComponent(typeof(BotMover), typeof(TargetTraсker))]
public class ResourceTaker : MonoBehaviour
{
    [SerializeField] private float _distanceToResource;

    private Resource _currentResource;
    private TargetTraсker _targetTraсker;

    private void Awake() => _targetTraсker = GetComponent<TargetTraсker>();

    private void OnEnable() => _targetTraсker.TargetReached += OnTakeResource;

    private void OnDisable() => _targetTraсker.TargetReached -= OnTakeResource;

    public void SetResource(Resource resource) => _currentResource = resource;

    private void OnTakeResource()
    {
        if (_currentResource == null)
            return;

        _currentResource.transform.SetParent(transform);
        _currentResource.transform.localPosition = Vector3.forward * _distanceToResource;

        SetResource(null);
    }
}