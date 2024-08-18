using UnityEngine;

[RequireComponent(typeof(BotMover))]
public class ResourceTaker : MonoBehaviour
{
    [SerializeField] private float _distanceToResource;

    private Resource _currentResource;

    public void SetResource(Resource resource) => _currentResource = resource;

    private void Update() => TryTakeResource();

    private void TryTakeResource()
    {
        if (_currentResource == null)
            return;

        if (transform.position.IsEnoughClose(_currentResource.transform.position, _distanceToResource))
        {
            _currentResource.transform.SetParent(transform);
            _currentResource.transform.localPosition = Vector3.forward * _distanceToResource;

            SetResource(null);
        }
    }
}